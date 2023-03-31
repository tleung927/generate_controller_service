using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SignificantOtherDeleteController : ControllerBase
    {
        private readonly ISignificantOtherDeleteService _SignificantOtherDeleteService;

        public SignificantOtherDeleteController(ISignificantOtherDeleteService SignificantOtherDeleteService)
        {
            _SignificantOtherDeleteService = SignificantOtherDeleteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSignificantOtherDeleteList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var SignificantOtherDeletes = await _SignificantOtherDeleteService.GetSignificantOtherDeleteListByValue(offset, limit, val);

            if (SignificantOtherDeletes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No SignificantOtherDeletes in database");
            }

            return StatusCode(StatusCodes.Status200OK, SignificantOtherDeletes);
        }

        [HttpGet]
        public async Task<IActionResult> GetSignificantOtherDeleteList(string SignificantOtherDelete_name)
        {
            var SignificantOtherDeletes = await _SignificantOtherDeleteService.GetSignificantOtherDeleteList(SignificantOtherDelete_name);

            if (SignificantOtherDeletes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No SignificantOtherDelete found for uci: {SignificantOtherDelete_name}");
            }

            return StatusCode(StatusCodes.Status200OK, SignificantOtherDeletes);
        }

        [HttpGet]
        public async Task<IActionResult> GetSignificantOtherDelete(string SignificantOtherDelete_name)
        {
            var SignificantOtherDeletes = await _SignificantOtherDeleteService.GetSignificantOtherDelete(SignificantOtherDelete_name);

            if (SignificantOtherDeletes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No SignificantOtherDelete found for uci: {SignificantOtherDelete_name}");
            }

            return StatusCode(StatusCodes.Status200OK, SignificantOtherDeletes);
        }

        [HttpPost]
        public async Task<ActionResult<SignificantOtherDelete>> AddSignificantOtherDelete(SignificantOtherDelete SignificantOtherDelete)
        {
            var dbSignificantOtherDelete = await _SignificantOtherDeleteService.AddSignificantOtherDelete(SignificantOtherDelete);

            if (dbSignificantOtherDelete == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{SignificantOtherDelete.TbSignificantOtherDeleteName} could not be added."
                );
            }

            return CreatedAtAction("GetSignificantOtherDelete", new { uci = SignificantOtherDelete.TbSignificantOtherDeleteName }, SignificantOtherDelete);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSignificantOtherDelete(SignificantOtherDelete SignificantOtherDelete)
        {           
            SignificantOtherDelete dbSignificantOtherDelete = await _SignificantOtherDeleteService.UpdateSignificantOtherDelete(SignificantOtherDelete);

            if (dbSignificantOtherDelete == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{SignificantOtherDelete.TbSignificantOtherDeleteName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSignificantOtherDelete(SignificantOtherDelete SignificantOtherDelete)
        {            
            (bool status, string message) = await _SignificantOtherDeleteService.DeleteSignificantOtherDelete(SignificantOtherDelete);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, SignificantOtherDelete);
        }
    }
}
