using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormIfspprController : ControllerBase
    {
        private readonly IFormIfspprService _FormIfspprService;

        public FormIfspprController(IFormIfspprService FormIfspprService)
        {
            _FormIfspprService = FormIfspprService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIfspprList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormIfspprs = await _FormIfspprService.GetFormIfspprListByValue(offset, limit, val);

            if (FormIfspprs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormIfspprs in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormIfspprs);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIfspprList(string FormIfsppr_name)
        {
            var FormIfspprs = await _FormIfspprService.GetFormIfspprList(FormIfsppr_name);

            if (FormIfspprs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormIfsppr found for uci: {FormIfsppr_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormIfspprs);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIfsppr(string FormIfsppr_name)
        {
            var FormIfspprs = await _FormIfspprService.GetFormIfsppr(FormIfsppr_name);

            if (FormIfspprs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormIfsppr found for uci: {FormIfsppr_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormIfspprs);
        }

        [HttpPost]
        public async Task<ActionResult<FormIfsppr>> AddFormIfsppr(FormIfsppr FormIfsppr)
        {
            var dbFormIfsppr = await _FormIfspprService.AddFormIfsppr(FormIfsppr);

            if (dbFormIfsppr == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormIfsppr.TbFormIfspprName} could not be added."
                );
            }

            return CreatedAtAction("GetFormIfsppr", new { uci = FormIfsppr.TbFormIfspprName }, FormIfsppr);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormIfsppr(FormIfsppr FormIfsppr)
        {           
            FormIfsppr dbFormIfsppr = await _FormIfspprService.UpdateFormIfsppr(FormIfsppr);

            if (dbFormIfsppr == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormIfsppr.TbFormIfspprName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormIfsppr(FormIfsppr FormIfsppr)
        {            
            (bool status, string message) = await _FormIfspprService.DeleteFormIfsppr(FormIfsppr);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormIfsppr);
        }
    }
}
