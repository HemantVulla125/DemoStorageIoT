using Azure;
using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoStorageIoT4.Models
{
    public class RecordsEntity : ITableEntity
    {
        public string PartitionKey { get ; set ; }
        public string RowKey { get ; set ; }
        public DateTimeOffset? Timestamp { get ; set ; }
        public ETag ETag { get ; set ; }

        public string Category { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        
        public int Price { get; set; }
    }
}
