using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PbcatcolController : ControllerBase
    {
        private readonly IPbcatcolService _PbcatcolService;

        public PbcatcolController(IPbcatcolService PbcatcolService)
        {
            _PbcatcolService = PbcatcolService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPbcatcolList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Pbcatcols = await _PbcatcolService.GetPbcatcolListByValue(offset, limit, val);

            if (Pbcatcols == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Pbcatcols in database");
            }

            return StatusCode(StatusCodes.Status200OK, Pbcatcols);
        }

        [HttpGet]
        public async Task<IActionResult> GetPbcatcolList(string Pbcatcol_name)
        {
            var Pbcatcols = await _PbcatcolService.GetPbcatcolList(Pbcatcol_name);

            if (Pbcatcols == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Pbcatcol found for uci: {Pbcatcol_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Pbcatcols);
        }

        [HttpGet]
        public async Task<IActionResult> GetPbcatcol(string Pbcatcol_name)
        {
            var Pbcatcols = await _PbcatcolService.GetPbcatcol(Pbcatcol_name);

            if (Pbcatcols == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Pbcatcol found for uci: {Pbcatcol_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Pbcatcols);
        }

        [HttpPost]
        public async Task<ActionResult<Pbcatcol>> AddPbcatcol(Pbcatcol Pbcatcol)
        {
            var dbPbcatcol = await _PbcatcolService.AddPbcatcol(Pbcatcol);

            if (dbPbcatcol == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Pbcatcol.TbPbcatcolName} could not be added."
                );
            }

            return CreatedAtAction("GetPbcatcol", new { uci = Pbcatcol.TbPbcatcolName }, Pbcatcol);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePbcatcol(Pbcatcol Pbcatcol)
        {           
            Pbcatcol dbPbcatcol = await _PbcatcolService.UpdatePbcatcol(Pbcatcol);

            if (dbPbcatcol == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Pbcatcol.TbPbcatcolName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePbcatcol(Pbcatcol Pbcatcol)
        {            
            (bool status, string message) = await _PbcatcolService.DeletePbcatcol(Pbcatcol);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Pbcatcol);
        }
    }
}
