using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PosAuthController : ControllerBase
    {
        private readonly IPosAuthService _PosAuthService;

        public PosAuthController(IPosAuthService PosAuthService)
        {
            _PosAuthService = PosAuthService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosAuthList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var PosAuths = await _PosAuthService.GetPosAuthListByValue(offset, limit, val);

            if (PosAuths == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No PosAuths in database");
            }

            return StatusCode(StatusCodes.Status200OK, PosAuths);
        }

        [HttpGet]
        public async Task<IActionResult> GetPosAuthList(string PosAuth_name)
        {
            var PosAuths = await _PosAuthService.GetPosAuthList(PosAuth_name);

            if (PosAuths == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No PosAuth found for uci: {PosAuth_name}");
            }

            return StatusCode(StatusCodes.Status200OK, PosAuths);
        }

        [HttpGet]
        public async Task<IActionResult> GetPosAuth(string PosAuth_name)
        {
            var PosAuths = await _PosAuthService.GetPosAuth(PosAuth_name);

            if (PosAuths == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No PosAuth found for uci: {PosAuth_name}");
            }

            return StatusCode(StatusCodes.Status200OK, PosAuths);
        }

        [HttpPost]
        public async Task<ActionResult<PosAuth>> AddPosAuth(PosAuth PosAuth)
        {
            var dbPosAuth = await _PosAuthService.AddPosAuth(PosAuth);

            if (dbPosAuth == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{PosAuth.TbPosAuthName} could not be added."
                );
            }

            return CreatedAtAction("GetPosAuth", new { uci = PosAuth.TbPosAuthName }, PosAuth);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePosAuth(PosAuth PosAuth)
        {           
            PosAuth dbPosAuth = await _PosAuthService.UpdatePosAuth(PosAuth);

            if (dbPosAuth == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{PosAuth.TbPosAuthName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePosAuth(PosAuth PosAuth)
        {            
            (bool status, string message) = await _PosAuthService.DeletePosAuth(PosAuth);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, PosAuth);
        }
    }
}
