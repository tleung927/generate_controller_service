using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormIppquarterlyController : ControllerBase
    {
        private readonly IFormIppquarterlyService _FormIppquarterlyService;

        public FormIppquarterlyController(IFormIppquarterlyService FormIppquarterlyService)
        {
            _FormIppquarterlyService = FormIppquarterlyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIppquarterlyList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormIppquarterlys = await _FormIppquarterlyService.GetFormIppquarterlyListByValue(offset, limit, val);

            if (FormIppquarterlys == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormIppquarterlys in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormIppquarterlys);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIppquarterlyList(string FormIppquarterly_name)
        {
            var FormIppquarterlys = await _FormIppquarterlyService.GetFormIppquarterlyList(FormIppquarterly_name);

            if (FormIppquarterlys == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormIppquarterly found for uci: {FormIppquarterly_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormIppquarterlys);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIppquarterly(string FormIppquarterly_name)
        {
            var FormIppquarterlys = await _FormIppquarterlyService.GetFormIppquarterly(FormIppquarterly_name);

            if (FormIppquarterlys == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormIppquarterly found for uci: {FormIppquarterly_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormIppquarterlys);
        }

        [HttpPost]
        public async Task<ActionResult<FormIppquarterly>> AddFormIppquarterly(FormIppquarterly FormIppquarterly)
        {
            var dbFormIppquarterly = await _FormIppquarterlyService.AddFormIppquarterly(FormIppquarterly);

            if (dbFormIppquarterly == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormIppquarterly.TbFormIppquarterlyName} could not be added."
                );
            }

            return CreatedAtAction("GetFormIppquarterly", new { uci = FormIppquarterly.TbFormIppquarterlyName }, FormIppquarterly);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormIppquarterly(FormIppquarterly FormIppquarterly)
        {           
            FormIppquarterly dbFormIppquarterly = await _FormIppquarterlyService.UpdateFormIppquarterly(FormIppquarterly);

            if (dbFormIppquarterly == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormIppquarterly.TbFormIppquarterlyName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormIppquarterly(FormIppquarterly FormIppquarterly)
        {            
            (bool status, string message) = await _FormIppquarterlyService.DeleteFormIppquarterly(FormIppquarterly);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormIppquarterly);
        }
    }
}
