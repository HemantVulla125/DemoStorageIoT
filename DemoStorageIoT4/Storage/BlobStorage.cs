using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.WindowsAzure.Storage.Blob;

namespace DemoStorageIoT4.Storage
{
    public class BlobStorage : IBlobStorage
    {
        
        
            public async Task<bool> DeleteDocument(string connectionString, string containerName, string fileName)
            {
                //  throw new NotImplementedException();
                var container = BlobExtensions.GetContainer(connectionString, containerName);
                if (!await container.ExistsAsync())
                {
                    return false;


                }

                var blobClient = container.GetBlobClient(fileName);

                if (await blobClient.ExistsAsync())
                {
                    await blobClient.DeleteIfExistsAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public async Task<List<string>> GetAllDocuments(string connectionString, string containerName)
            {
                //  throw new NotImplementedException();
                var container = BlobExtensions.GetContainer(connectionString, containerName);

                if (!await container.ExistsAsync())
                {
                    return new List<string>();
                }

                List<string> blobs = new List<string>();

                await foreach (BlobItem blobItem in container.GetBlobsAsync())
                {
                    blobs.Add(blobItem.Name);
                }
                return blobs;
            }

            public async Task<Stream> GetDocument(string connectionString, string containerName, string fileName)
            {
                //throw new NotImplementedException();
                var container = BlobExtensions.GetContainer(connectionString, containerName);
                if (await container.ExistsAsync())
                {
                    var blobClient = container.GetBlobClient(fileName);
                    if (blobClient.Exists())
                    {
                        var content = await blobClient.DownloadStreamingAsync();
                        return content.Value.Content;
                    }
                    else
                    {
                        throw new FileNotFoundException();
                    }
                }
                else
                {
                    throw new FileNotFoundException();
                }

            }

            public async Task UploadDocument(string connectionString, string containerName, string fileName, Stream fileContent)
            {
                //  throw new NotImplementedException();
                var container = BlobExtensions.GetContainer(connectionString, containerName);
                if (!await container.ExistsAsync())
                {
                    BlobServiceClient blobServiceClient1 = new BlobServiceClient(connectionString);
                    BlobServiceClient blobServiceClient = blobServiceClient1;
                    await blobServiceClient.CreateBlobContainerAsync(containerName);
                    container = blobServiceClient.GetBlobContainerClient(containerName);
                }

                var bobclient = container.GetBlobClient(fileName);
                if (!bobclient.Exists())
                {
                    fileContent.Position = 0;
                    await container.UploadBlobAsync(fileName, fileContent);
                }
                else
                {
                    fileContent.Position = 0;
                    await bobclient.UploadAsync(fileContent, overwrite: true);
                }
            }
        }

        public static class BlobExtensions
        {
            public static BlobContainerClient GetContainer(string connectionString, string containerName)
            {
                BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
                return blobServiceClient.GetBlobContainerClient(containerName);
            }
        }
    }

