using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SprohstController : ControllerBase
    {
        private readonly ISprohstService _SprohstService;

        public SprohstController(ISprohstService SprohstService)
        {
            _SprohstService = SprohstService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSprohstList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Sprohsts = await _SprohstService.GetSprohstListByValue(offset, limit, val);

            if (Sprohsts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Sprohsts in database");
            }

            return StatusCode(StatusCodes.Status200OK, Sprohsts);
        }

        [HttpGet]
        public async Task<IActionResult> GetSprohstList(string Sprohst_name)
        {
            var Sprohsts = await _SprohstService.GetSprohstList(Sprohst_name);

            if (Sprohsts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Sprohst found for uci: {Sprohst_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Sprohsts);
        }

        [HttpGet]
        public async Task<IActionResult> GetSprohst(string Sprohst_name)
        {
            var Sprohsts = await _SprohstService.GetSprohst(Sprohst_name);

            if (Sprohsts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Sprohst found for uci: {Sprohst_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Sprohsts);
        }

        [HttpPost]
        public async Task<ActionResult<Sprohst>> AddSprohst(Sprohst Sprohst)
        {
            var dbSprohst = await _SprohstService.AddSprohst(Sprohst);

            if (dbSprohst == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Sprohst.TbSprohstName} could not be added."
                );
            }

            return CreatedAtAction("GetSprohst", new { uci = Sprohst.TbSprohstName }, Sprohst);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSprohst(Sprohst Sprohst)
        {           
            Sprohst dbSprohst = await _SprohstService.UpdateSprohst(Sprohst);

            if (dbSprohst == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Sprohst.TbSprohstName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSprohst(Sprohst Sprohst)
        {            
            (bool status, string message) = await _SprohstService.DeleteSprohst(Sprohst);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Sprohst);
        }
    }
}
