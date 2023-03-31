using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormMtquestionController : ControllerBase
    {
        private readonly IFormMtquestionService _FormMtquestionService;

        public FormMtquestionController(IFormMtquestionService FormMtquestionService)
        {
            _FormMtquestionService = FormMtquestionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormMtquestionList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormMtquestions = await _FormMtquestionService.GetFormMtquestionListByValue(offset, limit, val);

            if (FormMtquestions == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormMtquestions in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormMtquestions);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormMtquestionList(string FormMtquestion_name)
        {
            var FormMtquestions = await _FormMtquestionService.GetFormMtquestionList(FormMtquestion_name);

            if (FormMtquestions == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormMtquestion found for uci: {FormMtquestion_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormMtquestions);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormMtquestion(string FormMtquestion_name)
        {
            var FormMtquestions = await _FormMtquestionService.GetFormMtquestion(FormMtquestion_name);

            if (FormMtquestions == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormMtquestion found for uci: {FormMtquestion_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormMtquestions);
        }

        [HttpPost]
        public async Task<ActionResult<FormMtquestion>> AddFormMtquestion(FormMtquestion FormMtquestion)
        {
            var dbFormMtquestion = await _FormMtquestionService.AddFormMtquestion(FormMtquestion);

            if (dbFormMtquestion == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormMtquestion.TbFormMtquestionName} could not be added."
                );
            }

            return CreatedAtAction("GetFormMtquestion", new { uci = FormMtquestion.TbFormMtquestionName }, FormMtquestion);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormMtquestion(FormMtquestion FormMtquestion)
        {           
            FormMtquestion dbFormMtquestion = await _FormMtquestionService.UpdateFormMtquestion(FormMtquestion);

            if (dbFormMtquestion == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormMtquestion.TbFormMtquestionName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormMtquestion(FormMtquestion FormMtquestion)
        {            
            (bool status, string message) = await _FormMtquestionService.DeleteFormMtquestion(FormMtquestion);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormMtquestion);
        }
    }
}
