using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormIppplanTitleController : ControllerBase
    {
        private readonly IFormIppplanTitleService _FormIppplanTitleService;

        public FormIppplanTitleController(IFormIppplanTitleService FormIppplanTitleService)
        {
            _FormIppplanTitleService = FormIppplanTitleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIppplanTitleList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormIppplanTitles = await _FormIppplanTitleService.GetFormIppplanTitleListByValue(offset, limit, val);

            if (FormIppplanTitles == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormIppplanTitles in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormIppplanTitles);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIppplanTitleList(string FormIppplanTitle_name)
        {
            var FormIppplanTitles = await _FormIppplanTitleService.GetFormIppplanTitleList(FormIppplanTitle_name);

            if (FormIppplanTitles == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormIppplanTitle found for uci: {FormIppplanTitle_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormIppplanTitles);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIppplanTitle(string FormIppplanTitle_name)
        {
            var FormIppplanTitles = await _FormIppplanTitleService.GetFormIppplanTitle(FormIppplanTitle_name);

            if (FormIppplanTitles == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormIppplanTitle found for uci: {FormIppplanTitle_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormIppplanTitles);
        }

        [HttpPost]
        public async Task<ActionResult<FormIppplanTitle>> AddFormIppplanTitle(FormIppplanTitle FormIppplanTitle)
        {
            var dbFormIppplanTitle = await _FormIppplanTitleService.AddFormIppplanTitle(FormIppplanTitle);

            if (dbFormIppplanTitle == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormIppplanTitle.TbFormIppplanTitleName} could not be added."
                );
            }

            return CreatedAtAction("GetFormIppplanTitle", new { uci = FormIppplanTitle.TbFormIppplanTitleName }, FormIppplanTitle);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormIppplanTitle(FormIppplanTitle FormIppplanTitle)
        {           
            FormIppplanTitle dbFormIppplanTitle = await _FormIppplanTitleService.UpdateFormIppplanTitle(FormIppplanTitle);

            if (dbFormIppplanTitle == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormIppplanTitle.TbFormIppplanTitleName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormIppplanTitle(FormIppplanTitle FormIppplanTitle)
        {            
            (bool status, string message) = await _FormIppplanTitleService.DeleteFormIppplanTitle(FormIppplanTitle);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormIppplanTitle);
        }
    }
}
