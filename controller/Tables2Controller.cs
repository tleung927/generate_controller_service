using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Tables2Controller : ControllerBase
    {
        private readonly ITables2Service _Tables2Service;

        public Tables2Controller(ITables2Service Tables2Service)
        {
            _Tables2Service = Tables2Service;
        }

        [HttpGet]
        public async Task<IActionResult> GetTables2List(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Tables2s = await _Tables2Service.GetTables2ListByValue(offset, limit, val);

            if (Tables2s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Tables2s in database");
            }

            return StatusCode(StatusCodes.Status200OK, Tables2s);
        }

        [HttpGet]
        public async Task<IActionResult> GetTables2List(string Tables2_name)
        {
            var Tables2s = await _Tables2Service.GetTables2List(Tables2_name);

            if (Tables2s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Tables2 found for uci: {Tables2_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Tables2s);
        }

        [HttpGet]
        public async Task<IActionResult> GetTables2(string Tables2_name)
        {
            var Tables2s = await _Tables2Service.GetTables2(Tables2_name);

            if (Tables2s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Tables2 found for uci: {Tables2_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Tables2s);
        }

        [HttpPost]
        public async Task<ActionResult<Tables2>> AddTables2(Tables2 Tables2)
        {
            var dbTables2 = await _Tables2Service.AddTables2(Tables2);

            if (dbTables2 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Tables2.TbTables2Name} could not be added."
                );
            }

            return CreatedAtAction("GetTables2", new { uci = Tables2.TbTables2Name }, Tables2);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTables2(Tables2 Tables2)
        {           
            Tables2 dbTables2 = await _Tables2Service.UpdateTables2(Tables2);

            if (dbTables2 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Tables2.TbTables2Name} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTables2(Tables2 Tables2)
        {            
            (bool status, string message) = await _Tables2Service.DeleteTables2(Tables2);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Tables2);
        }
    }
}
