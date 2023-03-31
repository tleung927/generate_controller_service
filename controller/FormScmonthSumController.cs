using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormScmonthSumController : ControllerBase
    {
        private readonly IFormScmonthSumService _FormScmonthSumService;

        public FormScmonthSumController(IFormScmonthSumService FormScmonthSumService)
        {
            _FormScmonthSumService = FormScmonthSumService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormScmonthSumList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormScmonthSums = await _FormScmonthSumService.GetFormScmonthSumListByValue(offset, limit, val);

            if (FormScmonthSums == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormScmonthSums in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormScmonthSums);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormScmonthSumList(string FormScmonthSum_name)
        {
            var FormScmonthSums = await _FormScmonthSumService.GetFormScmonthSumList(FormScmonthSum_name);

            if (FormScmonthSums == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormScmonthSum found for uci: {FormScmonthSum_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormScmonthSums);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormScmonthSum(string FormScmonthSum_name)
        {
            var FormScmonthSums = await _FormScmonthSumService.GetFormScmonthSum(FormScmonthSum_name);

            if (FormScmonthSums == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormScmonthSum found for uci: {FormScmonthSum_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormScmonthSums);
        }

        [HttpPost]
        public async Task<ActionResult<FormScmonthSum>> AddFormScmonthSum(FormScmonthSum FormScmonthSum)
        {
            var dbFormScmonthSum = await _FormScmonthSumService.AddFormScmonthSum(FormScmonthSum);

            if (dbFormScmonthSum == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormScmonthSum.TbFormScmonthSumName} could not be added."
                );
            }

            return CreatedAtAction("GetFormScmonthSum", new { uci = FormScmonthSum.TbFormScmonthSumName }, FormScmonthSum);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormScmonthSum(FormScmonthSum FormScmonthSum)
        {           
            FormScmonthSum dbFormScmonthSum = await _FormScmonthSumService.UpdateFormScmonthSum(FormScmonthSum);

            if (dbFormScmonthSum == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormScmonthSum.TbFormScmonthSumName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormScmonthSum(FormScmonthSum FormScmonthSum)
        {            
            (bool status, string message) = await _FormScmonthSumService.DeleteFormScmonthSum(FormScmonthSum);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormScmonthSum);
        }
    }
}
