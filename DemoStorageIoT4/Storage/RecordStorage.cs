using Azure.Data.Tables;
using DemoStorageIoT4.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoStorageIoT4.Storage
{
    public class RecordStorage : IRecordsTable
    {
       // private const string TableName = "Item";
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly RecordsEntity updaterecord;
        public RecordStorage(IConfiguration configuration)
        {
            _connectionString = configuration.GetValue<string>("MyTableConfig:StorageConnection");
        }
        public async Task<TableClient> GetTableClient(string TableName)
        {
            var serviceClient = new TableServiceClient(_connectionString);
            var tableClient = serviceClient.GetTableClient(TableName);
            
            await tableClient.CreateIfNotExistsAsync();
            return tableClient;
        }
        public async Task DeleteEntityAsync(string TableName,string category, string id)
        {
            // throw new NotImplementedException();
            var tableClient = await GetTableClient(TableName);
            await tableClient.DeleteEntityAsync(category,id);
        }

        public async Task<RecordsEntity> GetEntityAsync(string TableName,string category, string id)
        {
            //  throw new NotImplementedException();
            var tableClient = await GetTableClient(TableName);
            return await tableClient.GetEntityAsync<RecordsEntity>(category, id);
        }

        public async Task<RecordsEntity> AddEntityAsync(string TableName,RecordsEntity entity)
        {
            // throw new NotImplementedException();
            var tableClient = await GetTableClient(TableName);
            await tableClient.AddEntityAsync(entity);
           // var query = new TableQuery();
          //  TableOperation insertoperation = TableOperation.Insert((Microsoft.WindowsAzure.Storage.Table.ITableEntity)entity);
            
            return entity;
        }

        public async Task<RecordsEntity> UpsertEntityAsync(string TableName, RecordsEntity entity)
        {
            // throw new NotImplementedException();
            var tableClient = await GetTableClient(TableName);
           await tableClient.UpsertEntityAsync(entity);
         //  await tableClient.UpdateEntityAsync(entity,entity.ETag);
            return entity;
        }
    }
}
