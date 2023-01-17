using Azure.Storage.Queues.Models;
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
    public class QueueController : ControllerBase
    {
        private readonly IQueueStorage _storagequeue;
        private readonly string _connectionString;
        private readonly string queuename;

        public QueueController(IQueueStorage storage, IConfiguration iConfig)
        {
            _storagequeue = storage;
            _connectionString = iConfig.GetValue<string>("MyTableConfig:StorageConnection");
            queuename = iConfig.GetValue<string>("MyTableConfig:queuename");
        }
        [HttpPost("Send Message")]
        public IActionResult Send( string queuename,string queueMessage)
        {
           // _storagequeue.CreateQueue(queuename);
            _storagequeue.InsertMessage(_connectionString, queuename, queueMessage.ToString());
            return Ok();
        }
        [HttpGet("CreateQueue")]
        public IActionResult Create(string queuename)
        {



            _storagequeue.CreateQueue(_connectionString, queuename);
           // _storagequeue.InsertMessage(queuename, queueMessage.ToString());
            return Ok();
        }
        //[HttpGet("ListFiles")]
        //public IActionResult List(string queuename)
        //{
        //  //  _storagequeue.CreateQueue(queuename);
        // //   _storagequeue.InsertMessage(queuename, queueMessage.ToString());
        //    return Ok();
        //}

        [HttpDelete("DeleteMessage")]

        public IActionResult DeleteQueue(string queuename)
        {
           // _storagequeue.CreateQueue(queuename);
            _storagequeue.DeleteQueue(_connectionString,queuename);
            return Ok();
        }
        [HttpPut("UpdateMessage")]
        public IActionResult UpdateMessage(string queuename, string message)
        {
            //_storagequeue.CreateQueue(queuename);
            _storagequeue.UpdateMessage(_connectionString,queuename,message);
            return Ok();
        }

    }
}
