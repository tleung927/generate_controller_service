using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SandisSprotaxController : ControllerBase
    {
        private readonly ISandisSprotaxService _SandisSprotaxService;

        public SandisSprotaxController(ISandisSprotaxService SandisSprotaxService)
        {
            _SandisSprotaxService = SandisSprotaxService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSandisSprotaxList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var SandisSprotaxs = await _SandisSprotaxService.GetSandisSprotaxListByValue(offset, limit, val);

            if (SandisSprotaxs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No SandisSprotaxs in database");
            }

            return StatusCode(StatusCodes.Status200OK, SandisSprotaxs);
        }

        [HttpGet]
        public async Task<IActionResult> GetSandisSprotaxList(string SandisSprotax_name)
        {
            var SandisSprotaxs = await _SandisSprotaxService.GetSandisSprotaxList(SandisSprotax_name);

            if (SandisSprotaxs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No SandisSprotax found for uci: {SandisSprotax_name}");
            }

            return StatusCode(StatusCodes.Status200OK, SandisSprotaxs);
        }

        [HttpGet]
        public async Task<IActionResult> GetSandisSprotax(string SandisSprotax_name)
        {
            var SandisSprotaxs = await _SandisSprotaxService.GetSandisSprotax(SandisSprotax_name);

            if (SandisSprotaxs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No SandisSprotax found for uci: {SandisSprotax_name}");
            }

            return StatusCode(StatusCodes.Status200OK, SandisSprotaxs);
        }

        [HttpPost]
        public async Task<ActionResult<SandisSprotax>> AddSandisSprotax(SandisSprotax SandisSprotax)
        {
            var dbSandisSprotax = await _SandisSprotaxService.AddSandisSprotax(SandisSprotax);

            if (dbSandisSprotax == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{SandisSprotax.TbSandisSprotaxName} could not be added."
                );
            }

            return CreatedAtAction("GetSandisSprotax", new { uci = SandisSprotax.TbSandisSprotaxName }, SandisSprotax);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSandisSprotax(SandisSprotax SandisSprotax)
        {           
            SandisSprotax dbSandisSprotax = await _SandisSprotaxService.UpdateSandisSprotax(SandisSprotax);

            if (dbSandisSprotax == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{SandisSprotax.TbSandisSprotaxName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSandisSprotax(SandisSprotax SandisSprotax)
        {            
            (bool status, string message) = await _SandisSprotaxService.DeleteSandisSprotax(SandisSprotax);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, SandisSprotax);
        }
    }
}
