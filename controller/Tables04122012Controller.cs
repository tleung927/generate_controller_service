using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Tables04122012Controller : ControllerBase
    {
        private readonly ITables04122012Service _Tables04122012Service;

        public Tables04122012Controller(ITables04122012Service Tables04122012Service)
        {
            _Tables04122012Service = Tables04122012Service;
        }

        [HttpGet]
        public async Task<IActionResult> GetTables04122012List(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Tables04122012s = await _Tables04122012Service.GetTables04122012ListByValue(offset, limit, val);

            if (Tables04122012s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Tables04122012s in database");
            }

            return StatusCode(StatusCodes.Status200OK, Tables04122012s);
        }

        [HttpGet]
        public async Task<IActionResult> GetTables04122012List(string Tables04122012_name)
        {
            var Tables04122012s = await _Tables04122012Service.GetTables04122012List(Tables04122012_name);

            if (Tables04122012s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Tables04122012 found for uci: {Tables04122012_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Tables04122012s);
        }

        [HttpGet]
        public async Task<IActionResult> GetTables04122012(string Tables04122012_name)
        {
            var Tables04122012s = await _Tables04122012Service.GetTables04122012(Tables04122012_name);

            if (Tables04122012s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Tables04122012 found for uci: {Tables04122012_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Tables04122012s);
        }

        [HttpPost]
        public async Task<ActionResult<Tables04122012>> AddTables04122012(Tables04122012 Tables04122012)
        {
            var dbTables04122012 = await _Tables04122012Service.AddTables04122012(Tables04122012);

            if (dbTables04122012 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Tables04122012.TbTables04122012Name} could not be added."
                );
            }

            return CreatedAtAction("GetTables04122012", new { uci = Tables04122012.TbTables04122012Name }, Tables04122012);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTables04122012(Tables04122012 Tables04122012)
        {           
            Tables04122012 dbTables04122012 = await _Tables04122012Service.UpdateTables04122012(Tables04122012);

            if (dbTables04122012 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Tables04122012.TbTables04122012Name} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTables04122012(Tables04122012 Tables04122012)
        {            
            (bool status, string message) = await _Tables04122012Service.DeleteTables04122012(Tables04122012);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Tables04122012);
        }
    }
}
