using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArL234Controller : ControllerBase
    {
        private readonly IArL234Service _ArL234Service;

        public ArL234Controller(IArL234Service ArL234Service)
        {
            _ArL234Service = ArL234Service;
        }

        [HttpGet]
        public async Task<IActionResult> GetArL234List(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ArL234s = await _ArL234Service.GetArL234ListByValue(offset, limit, val);

            if (ArL234s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ArL234s in database");
            }

            return StatusCode(StatusCodes.Status200OK, ArL234s);
        }

        [HttpGet]
        public async Task<IActionResult> GetArL234List(string ArL234_name)
        {
            var ArL234s = await _ArL234Service.GetArL234List(ArL234_name);

            if (ArL234s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ArL234 found for uci: {ArL234_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ArL234s);
        }

        [HttpGet]
        public async Task<IActionResult> GetArL234(string ArL234_name)
        {
            var ArL234s = await _ArL234Service.GetArL234(ArL234_name);

            if (ArL234s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ArL234 found for uci: {ArL234_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ArL234s);
        }

        [HttpPost]
        public async Task<ActionResult<ArL234>> AddArL234(ArL234 ArL234)
        {
            var dbArL234 = await _ArL234Service.AddArL234(ArL234);

            if (dbArL234 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ArL234.TbArL234Name} could not be added."
                );
            }

            return CreatedAtAction("GetArL234", new { uci = ArL234.TbArL234Name }, ArL234);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateArL234(ArL234 ArL234)
        {           
            ArL234 dbArL234 = await _ArL234Service.UpdateArL234(ArL234);

            if (dbArL234 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ArL234.TbArL234Name} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteArL234(ArL234 ArL234)
        {            
            (bool status, string message) = await _ArL234Service.DeleteArL234(ArL234);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ArL234);
        }
    }
}
