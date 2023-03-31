using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GiServerController : ControllerBase
    {
        private readonly IGiServerService _GiServerService;

        public GiServerController(IGiServerService GiServerService)
        {
            _GiServerService = GiServerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGiServerList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var GiServers = await _GiServerService.GetGiServerListByValue(offset, limit, val);

            if (GiServers == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No GiServers in database");
            }

            return StatusCode(StatusCodes.Status200OK, GiServers);
        }

        [HttpGet]
        public async Task<IActionResult> GetGiServerList(string GiServer_name)
        {
            var GiServers = await _GiServerService.GetGiServerList(GiServer_name);

            if (GiServers == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No GiServer found for uci: {GiServer_name}");
            }

            return StatusCode(StatusCodes.Status200OK, GiServers);
        }

        [HttpGet]
        public async Task<IActionResult> GetGiServer(string GiServer_name)
        {
            var GiServers = await _GiServerService.GetGiServer(GiServer_name);

            if (GiServers == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No GiServer found for uci: {GiServer_name}");
            }

            return StatusCode(StatusCodes.Status200OK, GiServers);
        }

        [HttpPost]
        public async Task<ActionResult<GiServer>> AddGiServer(GiServer GiServer)
        {
            var dbGiServer = await _GiServerService.AddGiServer(GiServer);

            if (dbGiServer == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{GiServer.TbGiServerName} could not be added."
                );
            }

            return CreatedAtAction("GetGiServer", new { uci = GiServer.TbGiServerName }, GiServer);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGiServer(GiServer GiServer)
        {           
            GiServer dbGiServer = await _GiServerService.UpdateGiServer(GiServer);

            if (dbGiServer == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{GiServer.TbGiServerName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGiServer(GiServer GiServer)
        {            
            (bool status, string message) = await _GiServerService.DeleteGiServer(GiServer);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, GiServer);
        }
    }
}
