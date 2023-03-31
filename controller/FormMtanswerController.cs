using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormMtanswerController : ControllerBase
    {
        private readonly IFormMtanswerService _FormMtanswerService;

        public FormMtanswerController(IFormMtanswerService FormMtanswerService)
        {
            _FormMtanswerService = FormMtanswerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormMtanswerList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormMtanswers = await _FormMtanswerService.GetFormMtanswerListByValue(offset, limit, val);

            if (FormMtanswers == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormMtanswers in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormMtanswers);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormMtanswerList(string FormMtanswer_name)
        {
            var FormMtanswers = await _FormMtanswerService.GetFormMtanswerList(FormMtanswer_name);

            if (FormMtanswers == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormMtanswer found for uci: {FormMtanswer_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormMtanswers);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormMtanswer(string FormMtanswer_name)
        {
            var FormMtanswers = await _FormMtanswerService.GetFormMtanswer(FormMtanswer_name);

            if (FormMtanswers == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormMtanswer found for uci: {FormMtanswer_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormMtanswers);
        }

        [HttpPost]
        public async Task<ActionResult<FormMtanswer>> AddFormMtanswer(FormMtanswer FormMtanswer)
        {
            var dbFormMtanswer = await _FormMtanswerService.AddFormMtanswer(FormMtanswer);

            if (dbFormMtanswer == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormMtanswer.TbFormMtanswerName} could not be added."
                );
            }

            return CreatedAtAction("GetFormMtanswer", new { uci = FormMtanswer.TbFormMtanswerName }, FormMtanswer);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormMtanswer(FormMtanswer FormMtanswer)
        {           
            FormMtanswer dbFormMtanswer = await _FormMtanswerService.UpdateFormMtanswer(FormMtanswer);

            if (dbFormMtanswer == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormMtanswer.TbFormMtanswerName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormMtanswer(FormMtanswer FormMtanswer)
        {            
            (bool status, string message) = await _FormMtanswerService.DeleteFormMtanswer(FormMtanswer);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormMtanswer);
        }
    }
}
