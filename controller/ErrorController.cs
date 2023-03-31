using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ErrorController : ControllerBase
    {
        private readonly IErrorService _ErrorService;

        public ErrorController(IErrorService ErrorService)
        {
            _ErrorService = ErrorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetErrorList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Errors = await _ErrorService.GetErrorListByValue(offset, limit, val);

            if (Errors == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Errors in database");
            }

            return StatusCode(StatusCodes.Status200OK, Errors);
        }

        [HttpGet]
        public async Task<IActionResult> GetErrorList(string Error_name)
        {
            var Errors = await _ErrorService.GetErrorList(Error_name);

            if (Errors == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Error found for uci: {Error_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Errors);
        }

        [HttpGet]
        public async Task<IActionResult> GetError(string Error_name)
        {
            var Errors = await _ErrorService.GetError(Error_name);

            if (Errors == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Error found for uci: {Error_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Errors);
        }

        [HttpPost]
        public async Task<ActionResult<Error>> AddError(Error Error)
        {
            var dbError = await _ErrorService.AddError(Error);

            if (dbError == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Error.TbErrorName} could not be added."
                );
            }

            return CreatedAtAction("GetError", new { uci = Error.TbErrorName }, Error);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateError(Error Error)
        {           
            Error dbError = await _ErrorService.UpdateError(Error);

            if (dbError == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Error.TbErrorName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteError(Error Error)
        {            
            (bool status, string message) = await _ErrorService.DeleteError(Error);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Error);
        }
    }
}
