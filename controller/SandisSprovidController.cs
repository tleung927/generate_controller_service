using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SandisSprovidController : ControllerBase
    {
        private readonly ISandisSprovidService _SandisSprovidService;

        public SandisSprovidController(ISandisSprovidService SandisSprovidService)
        {
            _SandisSprovidService = SandisSprovidService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSandisSprovidList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var SandisSprovids = await _SandisSprovidService.GetSandisSprovidListByValue(offset, limit, val);

            if (SandisSprovids == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No SandisSprovids in database");
            }

            return StatusCode(StatusCodes.Status200OK, SandisSprovids);
        }

        [HttpGet]
        public async Task<IActionResult> GetSandisSprovidList(string SandisSprovid_name)
        {
            var SandisSprovids = await _SandisSprovidService.GetSandisSprovidList(SandisSprovid_name);

            if (SandisSprovids == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No SandisSprovid found for uci: {SandisSprovid_name}");
            }

            return StatusCode(StatusCodes.Status200OK, SandisSprovids);
        }

        [HttpGet]
        public async Task<IActionResult> GetSandisSprovid(string SandisSprovid_name)
        {
            var SandisSprovids = await _SandisSprovidService.GetSandisSprovid(SandisSprovid_name);

            if (SandisSprovids == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No SandisSprovid found for uci: {SandisSprovid_name}");
            }

            return StatusCode(StatusCodes.Status200OK, SandisSprovids);
        }

        [HttpPost]
        public async Task<ActionResult<SandisSprovid>> AddSandisSprovid(SandisSprovid SandisSprovid)
        {
            var dbSandisSprovid = await _SandisSprovidService.AddSandisSprovid(SandisSprovid);

            if (dbSandisSprovid == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{SandisSprovid.TbSandisSprovidName} could not be added."
                );
            }

            return CreatedAtAction("GetSandisSprovid", new { uci = SandisSprovid.TbSandisSprovidName }, SandisSprovid);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSandisSprovid(SandisSprovid SandisSprovid)
        {           
            SandisSprovid dbSandisSprovid = await _SandisSprovidService.UpdateSandisSprovid(SandisSprovid);

            if (dbSandisSprovid == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{SandisSprovid.TbSandisSprovidName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSandisSprovid(SandisSprovid SandisSprovid)
        {            
            (bool status, string message) = await _SandisSprovidService.DeleteSandisSprovid(SandisSprovid);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, SandisSprovid);
        }
    }
}
