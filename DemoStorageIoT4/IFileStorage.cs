using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoStorageIoT4
{
   public interface IFileStorage
    {
        Task<bool> CreateFileShareAsync(string connectionString, string shareName, string directoryname);
     //   Task GetFileShare(string connectionString, string containerName, string fileName, Stream fileContent);
        Task InsertFile(string connectionString, string sharepath, string FolderName, string directoryName,string filepath);
        Task<bool> DeleteFiles(string connectionString, string shareName, string snapshotTime);
    }
}
