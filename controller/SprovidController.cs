using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SprovidController : ControllerBase
    {
        private readonly ISprovidService _SprovidService;

        public SprovidController(ISprovidService SprovidService)
        {
            _SprovidService = SprovidService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSprovidList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Sprovids = await _SprovidService.GetSprovidListByValue(offset, limit, val);

            if (Sprovids == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Sprovids in database");
            }

            return StatusCode(StatusCodes.Status200OK, Sprovids);
        }

        [HttpGet]
        public async Task<IActionResult> GetSprovidList(string Sprovid_name)
        {
            var Sprovids = await _SprovidService.GetSprovidList(Sprovid_name);

            if (Sprovids == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Sprovid found for uci: {Sprovid_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Sprovids);
        }

        [HttpGet]
        public async Task<IActionResult> GetSprovid(string Sprovid_name)
        {
            var Sprovids = await _SprovidService.GetSprovid(Sprovid_name);

            if (Sprovids == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Sprovid found for uci: {Sprovid_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Sprovids);
        }

        [HttpPost]
        public async Task<ActionResult<Sprovid>> AddSprovid(Sprovid Sprovid)
        {
            var dbSprovid = await _SprovidService.AddSprovid(Sprovid);

            if (dbSprovid == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Sprovid.TbSprovidName} could not be added."
                );
            }

            return CreatedAtAction("GetSprovid", new { uci = Sprovid.TbSprovidName }, Sprovid);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSprovid(Sprovid Sprovid)
        {           
            Sprovid dbSprovid = await _SprovidService.UpdateSprovid(Sprovid);

            if (dbSprovid == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Sprovid.TbSprovidName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSprovid(Sprovid Sprovid)
        {            
            (bool status, string message) = await _SprovidService.DeleteSprovid(Sprovid);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Sprovid);
        }
    }
}
