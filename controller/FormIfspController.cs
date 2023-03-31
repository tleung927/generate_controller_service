using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormIfspController : ControllerBase
    {
        private readonly IFormIfspService _FormIfspService;

        public FormIfspController(IFormIfspService FormIfspService)
        {
            _FormIfspService = FormIfspService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIfspList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormIfsps = await _FormIfspService.GetFormIfspListByValue(offset, limit, val);

            if (FormIfsps == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormIfsps in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormIfsps);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIfspList(string FormIfsp_name)
        {
            var FormIfsps = await _FormIfspService.GetFormIfspList(FormIfsp_name);

            if (FormIfsps == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormIfsp found for uci: {FormIfsp_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormIfsps);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIfsp(string FormIfsp_name)
        {
            var FormIfsps = await _FormIfspService.GetFormIfsp(FormIfsp_name);

            if (FormIfsps == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormIfsp found for uci: {FormIfsp_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormIfsps);
        }

        [HttpPost]
        public async Task<ActionResult<FormIfsp>> AddFormIfsp(FormIfsp FormIfsp)
        {
            var dbFormIfsp = await _FormIfspService.AddFormIfsp(FormIfsp);

            if (dbFormIfsp == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormIfsp.TbFormIfspName} could not be added."
                );
            }

            return CreatedAtAction("GetFormIfsp", new { uci = FormIfsp.TbFormIfspName }, FormIfsp);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormIfsp(FormIfsp FormIfsp)
        {           
            FormIfsp dbFormIfsp = await _FormIfspService.UpdateFormIfsp(FormIfsp);

            if (dbFormIfsp == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormIfsp.TbFormIfspName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormIfsp(FormIfsp FormIfsp)
        {            
            (bool status, string message) = await _FormIfspService.DeleteFormIfsp(FormIfsp);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormIfsp);
        }
    }
}
