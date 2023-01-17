
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DemoStorageIoT4
{
   public interface IQueueStorage
    {
        Task CreateQueue(string connectionstring,string queueName);
        void DequeueMessages(string queueName);
        void InsertMessage(string connectionstring,string queueName, string message);
        void DeleteQueue(string connectionstring,string queueName);
        void UpdateMessage(string connectionstring,string queueName,string message);
    }
}
