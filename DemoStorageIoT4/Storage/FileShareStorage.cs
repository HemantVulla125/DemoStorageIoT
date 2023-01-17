using Azure;
using Azure.Storage.Files.Shares;
using Azure.Storage.Files.Shares.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DemoStorageIoT4.Storage
{
    public class FileShareStorage : IFileStorage
    {
        public async Task<bool> CreateFileShareAsync(string connectionString, string shareName, string directoryname)
        {
            // throw new NotImplementedException();
            string connectionStringFile = connectionString;

            // Instantiate a ShareClient which will be used to create and manipulate the file share
            ShareClient share = new ShareClient(connectionString, shareName);

            // Create the share if it doesn't already exist


            // Ensure that the share exists
            if (!await share.ExistsAsync())
            {
                await share.CreateIfNotExistsAsync();

                ShareDirectoryClient directory = share.GetDirectoryClient(directoryname);

                if (!await directory.ExistsAsync())
                {
                    // Create the directory if it doesn't already exist
                    await directory.CreateIfNotExistsAsync();
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteFiles(string connectionString, string shareName, string snapshotTime)
        {
            // throw new NotImplementedException();
            string connectionStringDelete = connectionString;

            // Instatiate a ShareServiceClient
            ShareServiceClient shareService = new ShareServiceClient(connectionString);

            // Get a ShareClient
            ShareClient share = shareService.GetShareClient(shareName);
            

           
            try
            {
                
                    // Delete the snapshot
                    await share.DeleteIfExistsAsync();
                    return true;
                //}
               // return true;
            }
            catch (RequestFailedException ex)
            {
                return false;
                Console.WriteLine($"Exception: {ex.Message}");
                Console.WriteLine($"Error code: {ex.Status}\t{ex.ErrorCode}");
            }
        }

        public async Task InsertFile(string connectionString, string shareName, string FolderName, string directoryName, string document)
        {
            //throw new NotImplementedException();
            string connectionStringFile = connectionString;

            // Instantiate a ShareClient which will be used to create and manipulate the file share
            ShareClient share = new ShareClient(connectionString, shareName);
            if (await share.ExistsAsync())
             {
                Console.WriteLine($"Share created: {share.Name}");

                // Get a reference to the sample directory
                ShareDirectoryClient directory = share.GetDirectoryClient(FolderName);

                // Create the directory if it doesn't already exist
                await directory.CreateIfNotExistsAsync();

                // Ensure that the directory exists
                if (await directory.ExistsAsync())
                {
                    // Get a reference to a file object
                    ShareFileClient file = directory.GetFileClient(directoryName);

                    //Upload if File Does not exisit
                    if (!await file.ExistsAsync())
                    {
                        // var file = directory.GetFileClient(fileName);
                        using FileStream stream = File.OpenRead(document);
                        file.Create(stream.Length);
                        file.UploadRange(
                            new HttpRange(0, stream.Length),
                            stream);
                        //Console.WriteLine($"File exists: {file.Name}");
                    }
                    else
                    {
                        // Download the file
                       // ShareFileDownloadInfo download = await file.DownloadAsync();
                    }

                        // Save the data to a local file, overwrite if the file already exists
                        //using (FileStream stream = File.OpenWrite(@"downloadedLog1.txt"))
                        //{
                        //    await download.Content.CopyToAsync(stream);
                        //    await stream.FlushAsync();
                        //    stream.Close();

                        //    // Display where the file was saved
                        //    Console.WriteLine($"File downloaded: {stream.Name}");
                        //}
                    }
                }
            }
        }
    }

