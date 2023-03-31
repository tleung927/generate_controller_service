using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class As400OutputController : ControllerBase
    {
        private readonly IAs400OutputService _As400OutputService;

        public As400OutputController(IAs400OutputService As400OutputService)
        {
            _As400OutputService = As400OutputService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAs400OutputList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var As400Outputs = await _As400OutputService.GetAs400OutputListByValue(offset, limit, val);

            if (As400Outputs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No As400Outputs in database");
            }

            return StatusCode(StatusCodes.Status200OK, As400Outputs);
        }

        [HttpGet]
        public async Task<IActionResult> GetAs400OutputList(string As400Output_name)
        {
            var As400Outputs = await _As400OutputService.GetAs400OutputList(As400Output_name);

            if (As400Outputs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No As400Output found for uci: {As400Output_name}");
            }

            return StatusCode(StatusCodes.Status200OK, As400Outputs);
        }

        [HttpGet]
        public async Task<IActionResult> GetAs400Output(string As400Output_name)
        {
            var As400Outputs = await _As400OutputService.GetAs400Output(As400Output_name);

            if (As400Outputs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No As400Output found for uci: {As400Output_name}");
            }

            return StatusCode(StatusCodes.Status200OK, As400Outputs);
        }

        [HttpPost]
        public async Task<ActionResult<As400Output>> AddAs400Output(As400Output As400Output)
        {
            var dbAs400Output = await _As400OutputService.AddAs400Output(As400Output);

            if (dbAs400Output == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{As400Output.TbAs400OutputName} could not be added."
                );
            }

            return CreatedAtAction("GetAs400Output", new { uci = As400Output.TbAs400OutputName }, As400Output);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAs400Output(As400Output As400Output)
        {           
            As400Output dbAs400Output = await _As400OutputService.UpdateAs400Output(As400Output);

            if (dbAs400Output == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{As400Output.TbAs400OutputName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAs400Output(As400Output As400Output)
        {            
            (bool status, string message) = await _As400OutputService.DeleteAs400Output(As400Output);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, As400Output);
        }
    }
}
