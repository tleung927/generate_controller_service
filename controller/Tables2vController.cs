using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Tables2vController : ControllerBase
    {
        private readonly ITables2vService _Tables2vService;

        public Tables2vController(ITables2vService Tables2vService)
        {
            _Tables2vService = Tables2vService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTables2vList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Tables2vs = await _Tables2vService.GetTables2vListByValue(offset, limit, val);

            if (Tables2vs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Tables2vs in database");
            }

            return StatusCode(StatusCodes.Status200OK, Tables2vs);
        }

        [HttpGet]
        public async Task<IActionResult> GetTables2vList(string Tables2v_name)
        {
            var Tables2vs = await _Tables2vService.GetTables2vList(Tables2v_name);

            if (Tables2vs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Tables2v found for uci: {Tables2v_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Tables2vs);
        }

        [HttpGet]
        public async Task<IActionResult> GetTables2v(string Tables2v_name)
        {
            var Tables2vs = await _Tables2vService.GetTables2v(Tables2v_name);

            if (Tables2vs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Tables2v found for uci: {Tables2v_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Tables2vs);
        }

        [HttpPost]
        public async Task<ActionResult<Tables2v>> AddTables2v(Tables2v Tables2v)
        {
            var dbTables2v = await _Tables2vService.AddTables2v(Tables2v);

            if (dbTables2v == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Tables2v.TbTables2vName} could not be added."
                );
            }

            return CreatedAtAction("GetTables2v", new { uci = Tables2v.TbTables2vName }, Tables2v);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTables2v(Tables2v Tables2v)
        {           
            Tables2v dbTables2v = await _Tables2vService.UpdateTables2v(Tables2v);

            if (dbTables2v == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Tables2v.TbTables2vName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTables2v(Tables2v Tables2v)
        {            
            (bool status, string message) = await _Tables2vService.DeleteTables2v(Tables2v);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Tables2v);
        }
    }
}
