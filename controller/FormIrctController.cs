using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormIrctController : ControllerBase
    {
        private readonly IFormIrctService _FormIrctService;

        public FormIrctController(IFormIrctService FormIrctService)
        {
            _FormIrctService = FormIrctService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIrctList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormIrcts = await _FormIrctService.GetFormIrctListByValue(offset, limit, val);

            if (FormIrcts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormIrcts in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormIrcts);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIrctList(string FormIrct_name)
        {
            var FormIrcts = await _FormIrctService.GetFormIrctList(FormIrct_name);

            if (FormIrcts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormIrct found for uci: {FormIrct_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormIrcts);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIrct(string FormIrct_name)
        {
            var FormIrcts = await _FormIrctService.GetFormIrct(FormIrct_name);

            if (FormIrcts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormIrct found for uci: {FormIrct_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormIrcts);
        }

        [HttpPost]
        public async Task<ActionResult<FormIrct>> AddFormIrct(FormIrct FormIrct)
        {
            var dbFormIrct = await _FormIrctService.AddFormIrct(FormIrct);

            if (dbFormIrct == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormIrct.TbFormIrctName} could not be added."
                );
            }

            return CreatedAtAction("GetFormIrct", new { uci = FormIrct.TbFormIrctName }, FormIrct);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormIrct(FormIrct FormIrct)
        {           
            FormIrct dbFormIrct = await _FormIrctService.UpdateFormIrct(FormIrct);

            if (dbFormIrct == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormIrct.TbFormIrctName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormIrct(FormIrct FormIrct)
        {            
            (bool status, string message) = await _FormIrctService.DeleteFormIrct(FormIrct);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormIrct);
        }
    }
}
