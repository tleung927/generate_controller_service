using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormMtController : ControllerBase
    {
        private readonly IFormMtService _FormMtService;

        public FormMtController(IFormMtService FormMtService)
        {
            _FormMtService = FormMtService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormMtList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormMts = await _FormMtService.GetFormMtListByValue(offset, limit, val);

            if (FormMts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormMts in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormMts);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormMtList(string FormMt_name)
        {
            var FormMts = await _FormMtService.GetFormMtList(FormMt_name);

            if (FormMts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormMt found for uci: {FormMt_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormMts);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormMt(string FormMt_name)
        {
            var FormMts = await _FormMtService.GetFormMt(FormMt_name);

            if (FormMts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormMt found for uci: {FormMt_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormMts);
        }

        [HttpPost]
        public async Task<ActionResult<FormMt>> AddFormMt(FormMt FormMt)
        {
            var dbFormMt = await _FormMtService.AddFormMt(FormMt);

            if (dbFormMt == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormMt.TbFormMtName} could not be added."
                );
            }

            return CreatedAtAction("GetFormMt", new { uci = FormMt.TbFormMtName }, FormMt);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormMt(FormMt FormMt)
        {           
            FormMt dbFormMt = await _FormMtService.UpdateFormMt(FormMt);

            if (dbFormMt == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormMt.TbFormMtName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormMt(FormMt FormMt)
        {            
            (bool status, string message) = await _FormMtService.DeleteFormMt(FormMt);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormMt);
        }
    }
}
