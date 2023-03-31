using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormMtBkController : ControllerBase
    {
        private readonly IFormMtBkService _FormMtBkService;

        public FormMtBkController(IFormMtBkService FormMtBkService)
        {
            _FormMtBkService = FormMtBkService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormMtBkList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormMtBks = await _FormMtBkService.GetFormMtBkListByValue(offset, limit, val);

            if (FormMtBks == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormMtBks in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormMtBks);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormMtBkList(string FormMtBk_name)
        {
            var FormMtBks = await _FormMtBkService.GetFormMtBkList(FormMtBk_name);

            if (FormMtBks == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormMtBk found for uci: {FormMtBk_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormMtBks);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormMtBk(string FormMtBk_name)
        {
            var FormMtBks = await _FormMtBkService.GetFormMtBk(FormMtBk_name);

            if (FormMtBks == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormMtBk found for uci: {FormMtBk_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormMtBks);
        }

        [HttpPost]
        public async Task<ActionResult<FormMtBk>> AddFormMtBk(FormMtBk FormMtBk)
        {
            var dbFormMtBk = await _FormMtBkService.AddFormMtBk(FormMtBk);

            if (dbFormMtBk == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormMtBk.TbFormMtBkName} could not be added."
                );
            }

            return CreatedAtAction("GetFormMtBk", new { uci = FormMtBk.TbFormMtBkName }, FormMtBk);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormMtBk(FormMtBk FormMtBk)
        {           
            FormMtBk dbFormMtBk = await _FormMtBkService.UpdateFormMtBk(FormMtBk);

            if (dbFormMtBk == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormMtBk.TbFormMtBkName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormMtBk(FormMtBk FormMtBk)
        {            
            (bool status, string message) = await _FormMtBkService.DeleteFormMtBk(FormMtBk);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormMtBk);
        }
    }
}
