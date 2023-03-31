using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PbcatvldController : ControllerBase
    {
        private readonly IPbcatvldService _PbcatvldService;

        public PbcatvldController(IPbcatvldService PbcatvldService)
        {
            _PbcatvldService = PbcatvldService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPbcatvldList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Pbcatvlds = await _PbcatvldService.GetPbcatvldListByValue(offset, limit, val);

            if (Pbcatvlds == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Pbcatvlds in database");
            }

            return StatusCode(StatusCodes.Status200OK, Pbcatvlds);
        }

        [HttpGet]
        public async Task<IActionResult> GetPbcatvldList(string Pbcatvld_name)
        {
            var Pbcatvlds = await _PbcatvldService.GetPbcatvldList(Pbcatvld_name);

            if (Pbcatvlds == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Pbcatvld found for uci: {Pbcatvld_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Pbcatvlds);
        }

        [HttpGet]
        public async Task<IActionResult> GetPbcatvld(string Pbcatvld_name)
        {
            var Pbcatvlds = await _PbcatvldService.GetPbcatvld(Pbcatvld_name);

            if (Pbcatvlds == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Pbcatvld found for uci: {Pbcatvld_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Pbcatvlds);
        }

        [HttpPost]
        public async Task<ActionResult<Pbcatvld>> AddPbcatvld(Pbcatvld Pbcatvld)
        {
            var dbPbcatvld = await _PbcatvldService.AddPbcatvld(Pbcatvld);

            if (dbPbcatvld == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Pbcatvld.TbPbcatvldName} could not be added."
                );
            }

            return CreatedAtAction("GetPbcatvld", new { uci = Pbcatvld.TbPbcatvldName }, Pbcatvld);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePbcatvld(Pbcatvld Pbcatvld)
        {           
            Pbcatvld dbPbcatvld = await _PbcatvldService.UpdatePbcatvld(Pbcatvld);

            if (dbPbcatvld == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Pbcatvld.TbPbcatvldName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePbcatvld(Pbcatvld Pbcatvld)
        {            
            (bool status, string message) = await _PbcatvldService.DeletePbcatvld(Pbcatvld);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Pbcatvld);
        }
    }
}
