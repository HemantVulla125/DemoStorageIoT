using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DemoStorageIoT4.Controllers
{
    public class BlobController : Controller
    {
        private readonly IBlobStorage _storage;
        private readonly string _connectionString;
        private readonly string _container;

        public BlobController(IBlobStorage storage, IConfiguration iConfig)
        {
            _storage = storage;
            _connectionString = iConfig.GetValue<string>("MyConfig:StorageConnection");
            _container = iConfig.GetValue<string>("MyConfig:ContainerName");
        }

        [HttpGet("ListFiles")]
        public async Task<List<string>> ListFiles(string container)
        {
            return await _storage.GetAllDocuments(_connectionString, container);
        }

        [Route("InsertFile")]
        [HttpPost]
        public async Task<bool> InsertFile( string container,IFormFile asset)
        {
            if (asset != null)
            {
                CloudStorageAccount storageacc = CloudStorageAccount.Parse(_connectionString);

                //Create Reference to Azure Blob
                CloudBlobClient blobClient = storageacc.CreateCloudBlobClient();

                //The next 2 lines create if not exists a container named "democontainer"
                CloudBlobContainer containerCreate = blobClient.GetContainerReference(container);

               await containerCreate.CreateIfNotExistsAsync();


                Stream stream = asset.OpenReadStream();
                await _storage.UploadDocument(_connectionString, container, asset.FileName, stream);
                return true;
            }

            return false;
        }

        [HttpGet("DownloadFile/{fileName}")]
        public async Task<IActionResult> DownloadFile(string container,string fileName)
        {
            var content = await _storage.GetDocument(_connectionString, container, fileName);
            return File(content, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }


        [Route("DeleteFile/{fileName}")]
        [HttpGet]
        public async Task<bool> DeleteFile(string container,string fileName)
        {
            return await _storage.DeleteDocument(_connectionString, container, fileName);
        }


    }
}
