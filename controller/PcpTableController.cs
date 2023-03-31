using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PcpTableController : ControllerBase
    {
        private readonly IPcpTableService _PcpTableService;

        public PcpTableController(IPcpTableService PcpTableService)
        {
            _PcpTableService = PcpTableService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPcpTableList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var PcpTables = await _PcpTableService.GetPcpTableListByValue(offset, limit, val);

            if (PcpTables == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No PcpTables in database");
            }

            return StatusCode(StatusCodes.Status200OK, PcpTables);
        }

        [HttpGet]
        public async Task<IActionResult> GetPcpTableList(string PcpTable_name)
        {
            var PcpTables = await _PcpTableService.GetPcpTableList(PcpTable_name);

            if (PcpTables == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No PcpTable found for uci: {PcpTable_name}");
            }

            return StatusCode(StatusCodes.Status200OK, PcpTables);
        }

        [HttpGet]
        public async Task<IActionResult> GetPcpTable(string PcpTable_name)
        {
            var PcpTables = await _PcpTableService.GetPcpTable(PcpTable_name);

            if (PcpTables == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No PcpTable found for uci: {PcpTable_name}");
            }

            return StatusCode(StatusCodes.Status200OK, PcpTables);
        }

        [HttpPost]
        public async Task<ActionResult<PcpTable>> AddPcpTable(PcpTable PcpTable)
        {
            var dbPcpTable = await _PcpTableService.AddPcpTable(PcpTable);

            if (dbPcpTable == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{PcpTable.TbPcpTableName} could not be added."
                );
            }

            return CreatedAtAction("GetPcpTable", new { uci = PcpTable.TbPcpTableName }, PcpTable);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePcpTable(PcpTable PcpTable)
        {           
            PcpTable dbPcpTable = await _PcpTableService.UpdatePcpTable(PcpTable);

            if (dbPcpTable == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{PcpTable.TbPcpTableName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePcpTable(PcpTable PcpTable)
        {            
            (bool status, string message) = await _PcpTableService.DeletePcpTable(PcpTable);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, PcpTable);
        }
    }
}
