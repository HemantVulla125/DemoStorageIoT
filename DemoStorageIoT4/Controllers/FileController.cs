using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoStorageIoT4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {

        private readonly IFileStorage storagefile;
        private readonly string _connectionString;
        private readonly string queuename;

        public FileController(IFileStorage storage, IConfiguration iConfig)
        {
            storagefile = storage;
            _connectionString = iConfig.GetValue<string>("MyTableConfig:StorageConnection");
            queuename = iConfig.GetValue<string>("MyTableConfig:queuename");
        }
        [HttpPost("Create File")]
        public IActionResult CreateFile( string fileShareName, string  directoryname)
        {
            
                storagefile.CreateFileShareAsync(_connectionString, fileShareName, directoryname);
                return Ok();
           
        
            // _storagequeue.CreateQueue(queuename);
            // _storagequeue.InsertMessage(_connectionString, queuename, queueMessage.ToString());
           
        }
        [HttpPost("InsertFile")]
        public IActionResult Create(string sharepath, string  directory,string newFileName,string localpath)
        {



            storagefile.InsertFile(_connectionString, sharepath, directory, newFileName, localpath);
            // _storagequeue.InsertMessage(queuename, queueMessage.ToString());
            return Ok();
        }
        [HttpDelete("DeleteFile")]
        public IActionResult DeleteFile(string sharepath, string fileName)
        {



            storagefile.DeleteFiles(_connectionString, sharepath, fileName);
            // _storagequeue.InsertMessage(queuename, queueMessage.ToString());
            return Ok();
        }
    }
}
