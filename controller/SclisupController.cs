using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SclisupController : ControllerBase
    {
        private readonly ISclisupService _SclisupService;

        public SclisupController(ISclisupService SclisupService)
        {
            _SclisupService = SclisupService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSclisupList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Sclisups = await _SclisupService.GetSclisupListByValue(offset, limit, val);

            if (Sclisups == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Sclisups in database");
            }

            return StatusCode(StatusCodes.Status200OK, Sclisups);
        }

        [HttpGet]
        public async Task<IActionResult> GetSclisupList(string Sclisup_name)
        {
            var Sclisups = await _SclisupService.GetSclisupList(Sclisup_name);

            if (Sclisups == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Sclisup found for uci: {Sclisup_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Sclisups);
        }

        [HttpGet]
        public async Task<IActionResult> GetSclisup(string Sclisup_name)
        {
            var Sclisups = await _SclisupService.GetSclisup(Sclisup_name);

            if (Sclisups == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Sclisup found for uci: {Sclisup_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Sclisups);
        }

        [HttpPost]
        public async Task<ActionResult<Sclisup>> AddSclisup(Sclisup Sclisup)
        {
            var dbSclisup = await _SclisupService.AddSclisup(Sclisup);

            if (dbSclisup == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Sclisup.TbSclisupName} could not be added."
                );
            }

            return CreatedAtAction("GetSclisup", new { uci = Sclisup.TbSclisupName }, Sclisup);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSclisup(Sclisup Sclisup)
        {           
            Sclisup dbSclisup = await _SclisupService.UpdateSclisup(Sclisup);

            if (dbSclisup == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Sclisup.TbSclisupName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSclisup(Sclisup Sclisup)
        {            
            (bool status, string message) = await _SclisupService.DeleteSclisup(Sclisup);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Sclisup);
        }
    }
}
