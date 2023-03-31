using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormScmontholdController : ControllerBase
    {
        private readonly IFormScmontholdService _FormScmontholdService;

        public FormScmontholdController(IFormScmontholdService FormScmontholdService)
        {
            _FormScmontholdService = FormScmontholdService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormScmontholdList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormScmontholds = await _FormScmontholdService.GetFormScmontholdListByValue(offset, limit, val);

            if (FormScmontholds == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormScmontholds in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormScmontholds);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormScmontholdList(string FormScmonthold_name)
        {
            var FormScmontholds = await _FormScmontholdService.GetFormScmontholdList(FormScmonthold_name);

            if (FormScmontholds == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormScmonthold found for uci: {FormScmonthold_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormScmontholds);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormScmonthold(string FormScmonthold_name)
        {
            var FormScmontholds = await _FormScmontholdService.GetFormScmonthold(FormScmonthold_name);

            if (FormScmontholds == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormScmonthold found for uci: {FormScmonthold_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormScmontholds);
        }

        [HttpPost]
        public async Task<ActionResult<FormScmonthold>> AddFormScmonthold(FormScmonthold FormScmonthold)
        {
            var dbFormScmonthold = await _FormScmontholdService.AddFormScmonthold(FormScmonthold);

            if (dbFormScmonthold == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormScmonthold.TbFormScmontholdName} could not be added."
                );
            }

            return CreatedAtAction("GetFormScmonthold", new { uci = FormScmonthold.TbFormScmontholdName }, FormScmonthold);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormScmonthold(FormScmonthold FormScmonthold)
        {           
            FormScmonthold dbFormScmonthold = await _FormScmontholdService.UpdateFormScmonthold(FormScmonthold);

            if (dbFormScmonthold == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormScmonthold.TbFormScmontholdName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormScmonthold(FormScmonthold FormScmonthold)
        {            
            (bool status, string message) = await _FormScmontholdService.DeleteFormScmonthold(FormScmonthold);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormScmonthold);
        }
    }
}
