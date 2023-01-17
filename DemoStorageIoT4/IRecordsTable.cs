using DemoStorageIoT4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoStorageIoT4
{
   public interface IRecordsTable
    {
        Task<RecordsEntity> GetEntityAsync(string tblname,string category, string id);
        Task<RecordsEntity> AddEntityAsync(string tblname, RecordsEntity entity);
        Task<RecordsEntity> UpsertEntityAsync(string tblname,RecordsEntity entity);
        Task DeleteEntityAsync(string tblname,string category, string id);
    }
}
