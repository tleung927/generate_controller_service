using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormController : ControllerBase
    {
        private readonly IFormService _FormService;

        public FormController(IFormService FormService)
        {
            _FormService = FormService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Forms = await _FormService.GetFormListByValue(offset, limit, val);

            if (Forms == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Forms in database");
            }

            return StatusCode(StatusCodes.Status200OK, Forms);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormList(string Form_name)
        {
            var Forms = await _FormService.GetFormList(Form_name);

            if (Forms == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Form found for uci: {Form_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Forms);
        }

        [HttpGet]
        public async Task<IActionResult> GetForm(string Form_name)
        {
            var Forms = await _FormService.GetForm(Form_name);

            if (Forms == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Form found for uci: {Form_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Forms);
        }

        [HttpPost]
        public async Task<ActionResult<Form>> AddForm(Form Form)
        {
            var dbForm = await _FormService.AddForm(Form);

            if (dbForm == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Form.TbFormName} could not be added."
                );
            }

            return CreatedAtAction("GetForm", new { uci = Form.TbFormName }, Form);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateForm(Form Form)
        {           
            Form dbForm = await _FormService.UpdateForm(Form);

            if (dbForm == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Form.TbFormName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteForm(Form Form)
        {            
            (bool status, string message) = await _FormService.DeleteForm(Form);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Form);
        }
    }
}
