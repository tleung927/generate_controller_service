using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LegalOldController : ControllerBase
    {
        private readonly ILegalOldService _LegalOldService;

        public LegalOldController(ILegalOldService LegalOldService)
        {
            _LegalOldService = LegalOldService;
        }

        [HttpGet]
        public async Task<IActionResult> GetLegalOldList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var LegalOlds = await _LegalOldService.GetLegalOldListByValue(offset, limit, val);

            if (LegalOlds == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No LegalOlds in database");
            }

            return StatusCode(StatusCodes.Status200OK, LegalOlds);
        }

        [HttpGet]
        public async Task<IActionResult> GetLegalOldList(string LegalOld_name)
        {
            var LegalOlds = await _LegalOldService.GetLegalOldList(LegalOld_name);

            if (LegalOlds == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No LegalOld found for uci: {LegalOld_name}");
            }

            return StatusCode(StatusCodes.Status200OK, LegalOlds);
        }

        [HttpGet]
        public async Task<IActionResult> GetLegalOld(string LegalOld_name)
        {
            var LegalOlds = await _LegalOldService.GetLegalOld(LegalOld_name);

            if (LegalOlds == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No LegalOld found for uci: {LegalOld_name}");
            }

            return StatusCode(StatusCodes.Status200OK, LegalOlds);
        }

        [HttpPost]
        public async Task<ActionResult<LegalOld>> AddLegalOld(LegalOld LegalOld)
        {
            var dbLegalOld = await _LegalOldService.AddLegalOld(LegalOld);

            if (dbLegalOld == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{LegalOld.TbLegalOldName} could not be added."
                );
            }

            return CreatedAtAction("GetLegalOld", new { uci = LegalOld.TbLegalOldName }, LegalOld);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLegalOld(LegalOld LegalOld)
        {           
            LegalOld dbLegalOld = await _LegalOldService.UpdateLegalOld(LegalOld);

            if (dbLegalOld == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{LegalOld.TbLegalOldName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteLegalOld(LegalOld LegalOld)
        {            
            (bool status, string message) = await _LegalOldService.DeleteLegalOld(LegalOld);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, LegalOld);
        }
    }
}
