using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SandisSpromatController : ControllerBase
    {
        private readonly ISandisSpromatService _SandisSpromatService;

        public SandisSpromatController(ISandisSpromatService SandisSpromatService)
        {
            _SandisSpromatService = SandisSpromatService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSandisSpromatList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var SandisSpromats = await _SandisSpromatService.GetSandisSpromatListByValue(offset, limit, val);

            if (SandisSpromats == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No SandisSpromats in database");
            }

            return StatusCode(StatusCodes.Status200OK, SandisSpromats);
        }

        [HttpGet]
        public async Task<IActionResult> GetSandisSpromatList(string SandisSpromat_name)
        {
            var SandisSpromats = await _SandisSpromatService.GetSandisSpromatList(SandisSpromat_name);

            if (SandisSpromats == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No SandisSpromat found for uci: {SandisSpromat_name}");
            }

            return StatusCode(StatusCodes.Status200OK, SandisSpromats);
        }

        [HttpGet]
        public async Task<IActionResult> GetSandisSpromat(string SandisSpromat_name)
        {
            var SandisSpromats = await _SandisSpromatService.GetSandisSpromat(SandisSpromat_name);

            if (SandisSpromats == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No SandisSpromat found for uci: {SandisSpromat_name}");
            }

            return StatusCode(StatusCodes.Status200OK, SandisSpromats);
        }

        [HttpPost]
        public async Task<ActionResult<SandisSpromat>> AddSandisSpromat(SandisSpromat SandisSpromat)
        {
            var dbSandisSpromat = await _SandisSpromatService.AddSandisSpromat(SandisSpromat);

            if (dbSandisSpromat == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{SandisSpromat.TbSandisSpromatName} could not be added."
                );
            }

            return CreatedAtAction("GetSandisSpromat", new { uci = SandisSpromat.TbSandisSpromatName }, SandisSpromat);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSandisSpromat(SandisSpromat SandisSpromat)
        {           
            SandisSpromat dbSandisSpromat = await _SandisSpromatService.UpdateSandisSpromat(SandisSpromat);

            if (dbSandisSpromat == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{SandisSpromat.TbSandisSpromatName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSandisSpromat(SandisSpromat SandisSpromat)
        {            
            (bool status, string message) = await _SandisSpromatService.DeleteSandisSpromat(SandisSpromat);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, SandisSpromat);
        }
    }
}
