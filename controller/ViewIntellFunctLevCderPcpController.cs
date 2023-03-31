using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewIntellFunctLevCderPcpController : ControllerBase
    {
        private readonly IViewIntellFunctLevCderPcpService _ViewIntellFunctLevCderPcpService;

        public ViewIntellFunctLevCderPcpController(IViewIntellFunctLevCderPcpService ViewIntellFunctLevCderPcpService)
        {
            _ViewIntellFunctLevCderPcpService = ViewIntellFunctLevCderPcpService;
        }

        [HttpGet]
        public async Task<IActionResult> GetViewIntellFunctLevCderPcpList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ViewIntellFunctLevCderPcps = await _ViewIntellFunctLevCderPcpService.GetViewIntellFunctLevCderPcpListByValue(offset, limit, val);

            if (ViewIntellFunctLevCderPcps == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ViewIntellFunctLevCderPcps in database");
            }

            return StatusCode(StatusCodes.Status200OK, ViewIntellFunctLevCderPcps);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewIntellFunctLevCderPcpList(string ViewIntellFunctLevCderPcp_name)
        {
            var ViewIntellFunctLevCderPcps = await _ViewIntellFunctLevCderPcpService.GetViewIntellFunctLevCderPcpList(ViewIntellFunctLevCderPcp_name);

            if (ViewIntellFunctLevCderPcps == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewIntellFunctLevCderPcp found for uci: {ViewIntellFunctLevCderPcp_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewIntellFunctLevCderPcps);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewIntellFunctLevCderPcp(string ViewIntellFunctLevCderPcp_name)
        {
            var ViewIntellFunctLevCderPcps = await _ViewIntellFunctLevCderPcpService.GetViewIntellFunctLevCderPcp(ViewIntellFunctLevCderPcp_name);

            if (ViewIntellFunctLevCderPcps == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewIntellFunctLevCderPcp found for uci: {ViewIntellFunctLevCderPcp_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewIntellFunctLevCderPcps);
        }

        [HttpPost]
        public async Task<ActionResult<ViewIntellFunctLevCderPcp>> AddViewIntellFunctLevCderPcp(ViewIntellFunctLevCderPcp ViewIntellFunctLevCderPcp)
        {
            var dbViewIntellFunctLevCderPcp = await _ViewIntellFunctLevCderPcpService.AddViewIntellFunctLevCderPcp(ViewIntellFunctLevCderPcp);

            if (dbViewIntellFunctLevCderPcp == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewIntellFunctLevCderPcp.TbViewIntellFunctLevCderPcpName} could not be added."
                );
            }

            return CreatedAtAction("GetViewIntellFunctLevCderPcp", new { uci = ViewIntellFunctLevCderPcp.TbViewIntellFunctLevCderPcpName }, ViewIntellFunctLevCderPcp);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViewIntellFunctLevCderPcp(ViewIntellFunctLevCderPcp ViewIntellFunctLevCderPcp)
        {           
            ViewIntellFunctLevCderPcp dbViewIntellFunctLevCderPcp = await _ViewIntellFunctLevCderPcpService.UpdateViewIntellFunctLevCderPcp(ViewIntellFunctLevCderPcp);

            if (dbViewIntellFunctLevCderPcp == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewIntellFunctLevCderPcp.TbViewIntellFunctLevCderPcpName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteViewIntellFunctLevCderPcp(ViewIntellFunctLevCderPcp ViewIntellFunctLevCderPcp)
        {            
            (bool status, string message) = await _ViewIntellFunctLevCderPcpService.DeleteViewIntellFunctLevCderPcp(ViewIntellFunctLevCderPcp);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ViewIntellFunctLevCderPcp);
        }
    }
}
