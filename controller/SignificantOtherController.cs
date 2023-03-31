using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SignificantOtherController : ControllerBase
    {
        private readonly ISignificantOtherService _SignificantOtherService;

        public SignificantOtherController(ISignificantOtherService SignificantOtherService)
        {
            _SignificantOtherService = SignificantOtherService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSignificantOtherList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var SignificantOthers = await _SignificantOtherService.GetSignificantOtherListByValue(offset, limit, val);

            if (SignificantOthers == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No SignificantOthers in database");
            }

            return StatusCode(StatusCodes.Status200OK, SignificantOthers);
        }

        [HttpGet]
        public async Task<IActionResult> GetSignificantOtherList(string SignificantOther_name)
        {
            var SignificantOthers = await _SignificantOtherService.GetSignificantOtherList(SignificantOther_name);

            if (SignificantOthers == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No SignificantOther found for uci: {SignificantOther_name}");
            }

            return StatusCode(StatusCodes.Status200OK, SignificantOthers);
        }

        [HttpGet]
        public async Task<IActionResult> GetSignificantOther(string SignificantOther_name)
        {
            var SignificantOthers = await _SignificantOtherService.GetSignificantOther(SignificantOther_name);

            if (SignificantOthers == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No SignificantOther found for uci: {SignificantOther_name}");
            }

            return StatusCode(StatusCodes.Status200OK, SignificantOthers);
        }

        [HttpPost]
        public async Task<ActionResult<SignificantOther>> AddSignificantOther(SignificantOther SignificantOther)
        {
            var dbSignificantOther = await _SignificantOtherService.AddSignificantOther(SignificantOther);

            if (dbSignificantOther == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{SignificantOther.TbSignificantOtherName} could not be added."
                );
            }

            return CreatedAtAction("GetSignificantOther", new { uci = SignificantOther.TbSignificantOtherName }, SignificantOther);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSignificantOther(SignificantOther SignificantOther)
        {           
            SignificantOther dbSignificantOther = await _SignificantOtherService.UpdateSignificantOther(SignificantOther);

            if (dbSignificantOther == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{SignificantOther.TbSignificantOtherName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSignificantOther(SignificantOther SignificantOther)
        {            
            (bool status, string message) = await _SignificantOtherService.DeleteSignificantOther(SignificantOther);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, SignificantOther);
        }
    }
}
