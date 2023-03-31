using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SclifcpController : ControllerBase
    {
        private readonly ISclifcpService _SclifcpService;

        public SclifcpController(ISclifcpService SclifcpService)
        {
            _SclifcpService = SclifcpService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSclifcpList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Sclifcps = await _SclifcpService.GetSclifcpListByValue(offset, limit, val);

            if (Sclifcps == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Sclifcps in database");
            }

            return StatusCode(StatusCodes.Status200OK, Sclifcps);
        }

        [HttpGet]
        public async Task<IActionResult> GetSclifcpList(string Sclifcp_name)
        {
            var Sclifcps = await _SclifcpService.GetSclifcpList(Sclifcp_name);

            if (Sclifcps == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Sclifcp found for uci: {Sclifcp_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Sclifcps);
        }

        [HttpGet]
        public async Task<IActionResult> GetSclifcp(string Sclifcp_name)
        {
            var Sclifcps = await _SclifcpService.GetSclifcp(Sclifcp_name);

            if (Sclifcps == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Sclifcp found for uci: {Sclifcp_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Sclifcps);
        }

        [HttpPost]
        public async Task<ActionResult<Sclifcp>> AddSclifcp(Sclifcp Sclifcp)
        {
            var dbSclifcp = await _SclifcpService.AddSclifcp(Sclifcp);

            if (dbSclifcp == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Sclifcp.TbSclifcpName} could not be added."
                );
            }

            return CreatedAtAction("GetSclifcp", new { uci = Sclifcp.TbSclifcpName }, Sclifcp);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSclifcp(Sclifcp Sclifcp)
        {           
            Sclifcp dbSclifcp = await _SclifcpService.UpdateSclifcp(Sclifcp);

            if (dbSclifcp == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Sclifcp.TbSclifcpName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSclifcp(Sclifcp Sclifcp)
        {            
            (bool status, string message) = await _SclifcpService.DeleteSclifcp(Sclifcp);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Sclifcp);
        }
    }
}
