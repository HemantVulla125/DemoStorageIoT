using DemoStorageIoT4.Models;
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
    public class RecordsTableController : Controller
    {


        private readonly IRecordsTable _storageTable;
      
        public RecordsTableController(IRecordsTable recordsService, IConfiguration iConfig )
        {
            _storageTable = recordsService;
           // _connectionString= iConfig.GetValue<string>("MyTableConfig:StorageConnection");
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
        [HttpGet("CreateTable")]
        
        public async Task<IActionResult> GetAsync([FromQuery] string category, string id, string tablename)
        {
            return Ok(await _storageTable.GetEntityAsync(tablename,category, id));
        }

        [HttpPost("AddDetails")]
        public async Task<IActionResult> PostAsync([FromForm] RecordsEntity entity, string tblname)
        {
            entity.PartitionKey = entity.Category;

          //  string Id = Guid.NewGuid().ToString();
          //  = entity.Id;
            entity.RowKey = entity.Id;

            var createdEntity = await _storageTable.AddEntityAsync(tblname,entity);
            // return CreatedAtAction(nameof(GetAsync), createdEntity);
            return Ok();
        }

        [HttpPut("UpdateEntity")]
        public async Task<IActionResult> PutAsync([FromForm] RecordsEntity entity, string tblname)
        {
            entity.PartitionKey = entity.Category;
            entity.RowKey = entity.Id;

            await _storageTable.UpsertEntityAsync(tblname,entity);
            return NoContent();
        }

        [HttpDelete("DeleteEntity")]
        public async Task<IActionResult> DeleteAsync([FromQuery] string category, string id, string tblname)
        {
            await _storageTable.DeleteEntityAsync(tblname,category, id);
            return NoContent();
        }
    }
}
