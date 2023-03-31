using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PbcattblController : ControllerBase
    {
        private readonly IPbcattblService _PbcattblService;

        public PbcattblController(IPbcattblService PbcattblService)
        {
            _PbcattblService = PbcattblService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPbcattblList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Pbcattbls = await _PbcattblService.GetPbcattblListByValue(offset, limit, val);

            if (Pbcattbls == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Pbcattbls in database");
            }

            return StatusCode(StatusCodes.Status200OK, Pbcattbls);
        }

        [HttpGet]
        public async Task<IActionResult> GetPbcattblList(string Pbcattbl_name)
        {
            var Pbcattbls = await _PbcattblService.GetPbcattblList(Pbcattbl_name);

            if (Pbcattbls == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Pbcattbl found for uci: {Pbcattbl_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Pbcattbls);
        }

        [HttpGet]
        public async Task<IActionResult> GetPbcattbl(string Pbcattbl_name)
        {
            var Pbcattbls = await _PbcattblService.GetPbcattbl(Pbcattbl_name);

            if (Pbcattbls == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Pbcattbl found for uci: {Pbcattbl_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Pbcattbls);
        }

        [HttpPost]
        public async Task<ActionResult<Pbcattbl>> AddPbcattbl(Pbcattbl Pbcattbl)
        {
            var dbPbcattbl = await _PbcattblService.AddPbcattbl(Pbcattbl);

            if (dbPbcattbl == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Pbcattbl.TbPbcattblName} could not be added."
                );
            }

            return CreatedAtAction("GetPbcattbl", new { uci = Pbcattbl.TbPbcattblName }, Pbcattbl);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePbcattbl(Pbcattbl Pbcattbl)
        {           
            Pbcattbl dbPbcattbl = await _PbcattblService.UpdatePbcattbl(Pbcattbl);

            if (dbPbcattbl == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Pbcattbl.TbPbcattblName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePbcattbl(Pbcattbl Pbcattbl)
        {            
            (bool status, string message) = await _PbcattblService.DeletePbcattbl(Pbcattbl);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Pbcattbl);
        }
    }
}
