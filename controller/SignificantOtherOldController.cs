using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SignificantOtherOldController : ControllerBase
    {
        private readonly ISignificantOtherOldService _SignificantOtherOldService;

        public SignificantOtherOldController(ISignificantOtherOldService SignificantOtherOldService)
        {
            _SignificantOtherOldService = SignificantOtherOldService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSignificantOtherOldList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var SignificantOtherOlds = await _SignificantOtherOldService.GetSignificantOtherOldListByValue(offset, limit, val);

            if (SignificantOtherOlds == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No SignificantOtherOlds in database");
            }

            return StatusCode(StatusCodes.Status200OK, SignificantOtherOlds);
        }

        [HttpGet]
        public async Task<IActionResult> GetSignificantOtherOldList(string SignificantOtherOld_name)
        {
            var SignificantOtherOlds = await _SignificantOtherOldService.GetSignificantOtherOldList(SignificantOtherOld_name);

            if (SignificantOtherOlds == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No SignificantOtherOld found for uci: {SignificantOtherOld_name}");
            }

            return StatusCode(StatusCodes.Status200OK, SignificantOtherOlds);
        }

        [HttpGet]
        public async Task<IActionResult> GetSignificantOtherOld(string SignificantOtherOld_name)
        {
            var SignificantOtherOlds = await _SignificantOtherOldService.GetSignificantOtherOld(SignificantOtherOld_name);

            if (SignificantOtherOlds == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No SignificantOtherOld found for uci: {SignificantOtherOld_name}");
            }

            return StatusCode(StatusCodes.Status200OK, SignificantOtherOlds);
        }

        [HttpPost]
        public async Task<ActionResult<SignificantOtherOld>> AddSignificantOtherOld(SignificantOtherOld SignificantOtherOld)
        {
            var dbSignificantOtherOld = await _SignificantOtherOldService.AddSignificantOtherOld(SignificantOtherOld);

            if (dbSignificantOtherOld == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{SignificantOtherOld.TbSignificantOtherOldName} could not be added."
                );
            }

            return CreatedAtAction("GetSignificantOtherOld", new { uci = SignificantOtherOld.TbSignificantOtherOldName }, SignificantOtherOld);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSignificantOtherOld(SignificantOtherOld SignificantOtherOld)
        {           
            SignificantOtherOld dbSignificantOtherOld = await _SignificantOtherOldService.UpdateSignificantOtherOld(SignificantOtherOld);

            if (dbSignificantOtherOld == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{SignificantOtherOld.TbSignificantOtherOldName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSignificantOtherOld(SignificantOtherOld SignificantOtherOld)
        {            
            (bool status, string message) = await _SignificantOtherOldService.DeleteSignificantOtherOld(SignificantOtherOld);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, SignificantOtherOld);
        }
    }
}
