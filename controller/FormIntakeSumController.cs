using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormIntakeSumController : ControllerBase
    {
        private readonly IFormIntakeSumService _FormIntakeSumService;

        public FormIntakeSumController(IFormIntakeSumService FormIntakeSumService)
        {
            _FormIntakeSumService = FormIntakeSumService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIntakeSumList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormIntakeSums = await _FormIntakeSumService.GetFormIntakeSumListByValue(offset, limit, val);

            if (FormIntakeSums == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormIntakeSums in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormIntakeSums);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIntakeSumList(string FormIntakeSum_name)
        {
            var FormIntakeSums = await _FormIntakeSumService.GetFormIntakeSumList(FormIntakeSum_name);

            if (FormIntakeSums == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormIntakeSum found for uci: {FormIntakeSum_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormIntakeSums);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIntakeSum(string FormIntakeSum_name)
        {
            var FormIntakeSums = await _FormIntakeSumService.GetFormIntakeSum(FormIntakeSum_name);

            if (FormIntakeSums == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormIntakeSum found for uci: {FormIntakeSum_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormIntakeSums);
        }

        [HttpPost]
        public async Task<ActionResult<FormIntakeSum>> AddFormIntakeSum(FormIntakeSum FormIntakeSum)
        {
            var dbFormIntakeSum = await _FormIntakeSumService.AddFormIntakeSum(FormIntakeSum);

            if (dbFormIntakeSum == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormIntakeSum.TbFormIntakeSumName} could not be added."
                );
            }

            return CreatedAtAction("GetFormIntakeSum", new { uci = FormIntakeSum.TbFormIntakeSumName }, FormIntakeSum);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormIntakeSum(FormIntakeSum FormIntakeSum)
        {           
            FormIntakeSum dbFormIntakeSum = await _FormIntakeSumService.UpdateFormIntakeSum(FormIntakeSum);

            if (dbFormIntakeSum == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormIntakeSum.TbFormIntakeSumName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormIntakeSum(FormIntakeSum FormIntakeSum)
        {            
            (bool status, string message) = await _FormIntakeSumService.DeleteFormIntakeSum(FormIntakeSum);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormIntakeSum);
        }
    }
}
