using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PcpController : ControllerBase
    {
        private readonly IPcpService _PcpService;

        public PcpController(IPcpService PcpService)
        {
            _PcpService = PcpService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPcpList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Pcps = await _PcpService.GetPcpListByValue(offset, limit, val);

            if (Pcps == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Pcps in database");
            }

            return StatusCode(StatusCodes.Status200OK, Pcps);
        }

        [HttpGet]
        public async Task<IActionResult> GetPcpList(string Pcp_name)
        {
            var Pcps = await _PcpService.GetPcpList(Pcp_name);

            if (Pcps == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Pcp found for uci: {Pcp_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Pcps);
        }

        [HttpGet]
        public async Task<IActionResult> GetPcp(string Pcp_name)
        {
            var Pcps = await _PcpService.GetPcp(Pcp_name);

            if (Pcps == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Pcp found for uci: {Pcp_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Pcps);
        }

        [HttpPost]
        public async Task<ActionResult<Pcp>> AddPcp(Pcp Pcp)
        {
            var dbPcp = await _PcpService.AddPcp(Pcp);

            if (dbPcp == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Pcp.TbPcpName} could not be added."
                );
            }

            return CreatedAtAction("GetPcp", new { uci = Pcp.TbPcpName }, Pcp);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePcp(Pcp Pcp)
        {           
            Pcp dbPcp = await _PcpService.UpdatePcp(Pcp);

            if (dbPcp == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Pcp.TbPcpName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePcp(Pcp Pcp)
        {            
            (bool status, string message) = await _PcpService.DeletePcp(Pcp);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Pcp);
        }
    }
}
