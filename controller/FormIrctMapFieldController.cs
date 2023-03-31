using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormIrctMapFieldController : ControllerBase
    {
        private readonly IFormIrctMapFieldService _FormIrctMapFieldService;

        public FormIrctMapFieldController(IFormIrctMapFieldService FormIrctMapFieldService)
        {
            _FormIrctMapFieldService = FormIrctMapFieldService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIrctMapFieldList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormIrctMapFields = await _FormIrctMapFieldService.GetFormIrctMapFieldListByValue(offset, limit, val);

            if (FormIrctMapFields == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormIrctMapFields in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormIrctMapFields);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIrctMapFieldList(string FormIrctMapField_name)
        {
            var FormIrctMapFields = await _FormIrctMapFieldService.GetFormIrctMapFieldList(FormIrctMapField_name);

            if (FormIrctMapFields == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormIrctMapField found for uci: {FormIrctMapField_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormIrctMapFields);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIrctMapField(string FormIrctMapField_name)
        {
            var FormIrctMapFields = await _FormIrctMapFieldService.GetFormIrctMapField(FormIrctMapField_name);

            if (FormIrctMapFields == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormIrctMapField found for uci: {FormIrctMapField_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormIrctMapFields);
        }

        [HttpPost]
        public async Task<ActionResult<FormIrctMapField>> AddFormIrctMapField(FormIrctMapField FormIrctMapField)
        {
            var dbFormIrctMapField = await _FormIrctMapFieldService.AddFormIrctMapField(FormIrctMapField);

            if (dbFormIrctMapField == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormIrctMapField.TbFormIrctMapFieldName} could not be added."
                );
            }

            return CreatedAtAction("GetFormIrctMapField", new { uci = FormIrctMapField.TbFormIrctMapFieldName }, FormIrctMapField);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormIrctMapField(FormIrctMapField FormIrctMapField)
        {           
            FormIrctMapField dbFormIrctMapField = await _FormIrctMapFieldService.UpdateFormIrctMapField(FormIrctMapField);

            if (dbFormIrctMapField == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormIrctMapField.TbFormIrctMapFieldName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormIrctMapField(FormIrctMapField FormIrctMapField)
        {            
            (bool status, string message) = await _FormIrctMapFieldService.DeleteFormIrctMapField(FormIrctMapField);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormIrctMapField);
        }
    }
}
