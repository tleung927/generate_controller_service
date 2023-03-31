using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SclicdrController : ControllerBase
    {
        private readonly ISclicdrService _SclicdrService;

        public SclicdrController(ISclicdrService SclicdrService)
        {
            _SclicdrService = SclicdrService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSclicdrList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Sclicdrs = await _SclicdrService.GetSclicdrListByValue(offset, limit, val);

            if (Sclicdrs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Sclicdrs in database");
            }

            return StatusCode(StatusCodes.Status200OK, Sclicdrs);
        }

        [HttpGet]
        public async Task<IActionResult> GetSclicdrList(string Sclicdr_name)
        {
            var Sclicdrs = await _SclicdrService.GetSclicdrList(Sclicdr_name);

            if (Sclicdrs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Sclicdr found for uci: {Sclicdr_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Sclicdrs);
        }

        [HttpGet]
        public async Task<IActionResult> GetSclicdr(string Sclicdr_name)
        {
            var Sclicdrs = await _SclicdrService.GetSclicdr(Sclicdr_name);

            if (Sclicdrs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Sclicdr found for uci: {Sclicdr_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Sclicdrs);
        }

        [HttpPost]
        public async Task<ActionResult<Sclicdr>> AddSclicdr(Sclicdr Sclicdr)
        {
            var dbSclicdr = await _SclicdrService.AddSclicdr(Sclicdr);

            if (dbSclicdr == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Sclicdr.TbSclicdrName} could not be added."
                );
            }

            return CreatedAtAction("GetSclicdr", new { uci = Sclicdr.TbSclicdrName }, Sclicdr);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSclicdr(Sclicdr Sclicdr)
        {           
            Sclicdr dbSclicdr = await _SclicdrService.UpdateSclicdr(Sclicdr);

            if (dbSclicdr == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Sclicdr.TbSclicdrName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSclicdr(Sclicdr Sclicdr)
        {            
            (bool status, string message) = await _SclicdrService.DeleteSclicdr(Sclicdr);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Sclicdr);
        }
    }
}
