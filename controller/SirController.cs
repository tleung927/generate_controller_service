using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SirController : ControllerBase
    {
        private readonly ISirService _SirService;

        public SirController(ISirService SirService)
        {
            _SirService = SirService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSirList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Sirs = await _SirService.GetSirListByValue(offset, limit, val);

            if (Sirs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Sirs in database");
            }

            return StatusCode(StatusCodes.Status200OK, Sirs);
        }

        [HttpGet]
        public async Task<IActionResult> GetSirList(string Sir_name)
        {
            var Sirs = await _SirService.GetSirList(Sir_name);

            if (Sirs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Sir found for uci: {Sir_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Sirs);
        }

        [HttpGet]
        public async Task<IActionResult> GetSir(string Sir_name)
        {
            var Sirs = await _SirService.GetSir(Sir_name);

            if (Sirs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Sir found for uci: {Sir_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Sirs);
        }

        [HttpPost]
        public async Task<ActionResult<Sir>> AddSir(Sir Sir)
        {
            var dbSir = await _SirService.AddSir(Sir);

            if (dbSir == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Sir.TbSirName} could not be added."
                );
            }

            return CreatedAtAction("GetSir", new { uci = Sir.TbSirName }, Sir);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSir(Sir Sir)
        {           
            Sir dbSir = await _SirService.UpdateSir(Sir);

            if (dbSir == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Sir.TbSirName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSir(Sir Sir)
        {            
            (bool status, string message) = await _SirService.DeleteSir(Sir);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Sir);
        }
    }
}
