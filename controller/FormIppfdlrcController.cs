using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormIppfdlrcController : ControllerBase
    {
        private readonly IFormIppfdlrcService _FormIppfdlrcService;

        public FormIppfdlrcController(IFormIppfdlrcService FormIppfdlrcService)
        {
            _FormIppfdlrcService = FormIppfdlrcService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIppfdlrcList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormIppfdlrcs = await _FormIppfdlrcService.GetFormIppfdlrcListByValue(offset, limit, val);

            if (FormIppfdlrcs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormIppfdlrcs in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormIppfdlrcs);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIppfdlrcList(string FormIppfdlrc_name)
        {
            var FormIppfdlrcs = await _FormIppfdlrcService.GetFormIppfdlrcList(FormIppfdlrc_name);

            if (FormIppfdlrcs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormIppfdlrc found for uci: {FormIppfdlrc_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormIppfdlrcs);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIppfdlrc(string FormIppfdlrc_name)
        {
            var FormIppfdlrcs = await _FormIppfdlrcService.GetFormIppfdlrc(FormIppfdlrc_name);

            if (FormIppfdlrcs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormIppfdlrc found for uci: {FormIppfdlrc_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormIppfdlrcs);
        }

        [HttpPost]
        public async Task<ActionResult<FormIppfdlrc>> AddFormIppfdlrc(FormIppfdlrc FormIppfdlrc)
        {
            var dbFormIppfdlrc = await _FormIppfdlrcService.AddFormIppfdlrc(FormIppfdlrc);

            if (dbFormIppfdlrc == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormIppfdlrc.TbFormIppfdlrcName} could not be added."
                );
            }

            return CreatedAtAction("GetFormIppfdlrc", new { uci = FormIppfdlrc.TbFormIppfdlrcName }, FormIppfdlrc);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormIppfdlrc(FormIppfdlrc FormIppfdlrc)
        {           
            FormIppfdlrc dbFormIppfdlrc = await _FormIppfdlrcService.UpdateFormIppfdlrc(FormIppfdlrc);

            if (dbFormIppfdlrc == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormIppfdlrc.TbFormIppfdlrcName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormIppfdlrc(FormIppfdlrc FormIppfdlrc)
        {            
            (bool status, string message) = await _FormIppfdlrcService.DeleteFormIppfdlrc(FormIppfdlrc);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormIppfdlrc);
        }
    }
}
