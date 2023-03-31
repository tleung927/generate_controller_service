using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormIppannualFdlrcController : ControllerBase
    {
        private readonly IFormIppannualFdlrcService _FormIppannualFdlrcService;

        public FormIppannualFdlrcController(IFormIppannualFdlrcService FormIppannualFdlrcService)
        {
            _FormIppannualFdlrcService = FormIppannualFdlrcService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIppannualFdlrcList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormIppannualFdlrcs = await _FormIppannualFdlrcService.GetFormIppannualFdlrcListByValue(offset, limit, val);

            if (FormIppannualFdlrcs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormIppannualFdlrcs in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormIppannualFdlrcs);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIppannualFdlrcList(string FormIppannualFdlrc_name)
        {
            var FormIppannualFdlrcs = await _FormIppannualFdlrcService.GetFormIppannualFdlrcList(FormIppannualFdlrc_name);

            if (FormIppannualFdlrcs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormIppannualFdlrc found for uci: {FormIppannualFdlrc_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormIppannualFdlrcs);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIppannualFdlrc(string FormIppannualFdlrc_name)
        {
            var FormIppannualFdlrcs = await _FormIppannualFdlrcService.GetFormIppannualFdlrc(FormIppannualFdlrc_name);

            if (FormIppannualFdlrcs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormIppannualFdlrc found for uci: {FormIppannualFdlrc_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormIppannualFdlrcs);
        }

        [HttpPost]
        public async Task<ActionResult<FormIppannualFdlrc>> AddFormIppannualFdlrc(FormIppannualFdlrc FormIppannualFdlrc)
        {
            var dbFormIppannualFdlrc = await _FormIppannualFdlrcService.AddFormIppannualFdlrc(FormIppannualFdlrc);

            if (dbFormIppannualFdlrc == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormIppannualFdlrc.TbFormIppannualFdlrcName} could not be added."
                );
            }

            return CreatedAtAction("GetFormIppannualFdlrc", new { uci = FormIppannualFdlrc.TbFormIppannualFdlrcName }, FormIppannualFdlrc);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormIppannualFdlrc(FormIppannualFdlrc FormIppannualFdlrc)
        {           
            FormIppannualFdlrc dbFormIppannualFdlrc = await _FormIppannualFdlrcService.UpdateFormIppannualFdlrc(FormIppannualFdlrc);

            if (dbFormIppannualFdlrc == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormIppannualFdlrc.TbFormIppannualFdlrcName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormIppannualFdlrc(FormIppannualFdlrc FormIppannualFdlrc)
        {            
            (bool status, string message) = await _FormIppannualFdlrcService.DeleteFormIppannualFdlrc(FormIppannualFdlrc);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormIppannualFdlrc);
        }
    }
}
