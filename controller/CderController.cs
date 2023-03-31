using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CderController : ControllerBase
    {
        private readonly ICderService _CderService;

        public CderController(ICderService CderService)
        {
            _CderService = CderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCderList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Cders = await _CderService.GetCderListByValue(offset, limit, val);

            if (Cders == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Cders in database");
            }

            return StatusCode(StatusCodes.Status200OK, Cders);
        }

        [HttpGet]
        public async Task<IActionResult> GetCderList(string Cder_name)
        {
            var Cders = await _CderService.GetCderList(Cder_name);

            if (Cders == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Cder found for uci: {Cder_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Cders);
        }

        [HttpGet]
        public async Task<IActionResult> GetCder(string Cder_name)
        {
            var Cders = await _CderService.GetCder(Cder_name);

            if (Cders == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Cder found for uci: {Cder_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Cders);
        }

        [HttpPost]
        public async Task<ActionResult<Cder>> AddCder(Cder Cder)
        {
            var dbCder = await _CderService.AddCder(Cder);

            if (dbCder == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Cder.TbCderName} could not be added."
                );
            }

            return CreatedAtAction("GetCder", new { uci = Cder.TbCderName }, Cder);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCder(Cder Cder)
        {           
            Cder dbCder = await _CderService.UpdateCder(Cder);

            if (dbCder == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Cder.TbCderName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCder(Cder Cder)
        {            
            (bool status, string message) = await _CderService.DeleteCder(Cder);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Cder);
        }
    }
}
