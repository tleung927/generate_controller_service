using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormIppannualFdlrcBakController : ControllerBase
    {
        private readonly IFormIppannualFdlrcBakService _FormIppannualFdlrcBakService;

        public FormIppannualFdlrcBakController(IFormIppannualFdlrcBakService FormIppannualFdlrcBakService)
        {
            _FormIppannualFdlrcBakService = FormIppannualFdlrcBakService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIppannualFdlrcBakList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormIppannualFdlrcBaks = await _FormIppannualFdlrcBakService.GetFormIppannualFdlrcBakListByValue(offset, limit, val);

            if (FormIppannualFdlrcBaks == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormIppannualFdlrcBaks in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormIppannualFdlrcBaks);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIppannualFdlrcBakList(string FormIppannualFdlrcBak_name)
        {
            var FormIppannualFdlrcBaks = await _FormIppannualFdlrcBakService.GetFormIppannualFdlrcBakList(FormIppannualFdlrcBak_name);

            if (FormIppannualFdlrcBaks == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormIppannualFdlrcBak found for uci: {FormIppannualFdlrcBak_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormIppannualFdlrcBaks);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIppannualFdlrcBak(string FormIppannualFdlrcBak_name)
        {
            var FormIppannualFdlrcBaks = await _FormIppannualFdlrcBakService.GetFormIppannualFdlrcBak(FormIppannualFdlrcBak_name);

            if (FormIppannualFdlrcBaks == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormIppannualFdlrcBak found for uci: {FormIppannualFdlrcBak_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormIppannualFdlrcBaks);
        }

        [HttpPost]
        public async Task<ActionResult<FormIppannualFdlrcBak>> AddFormIppannualFdlrcBak(FormIppannualFdlrcBak FormIppannualFdlrcBak)
        {
            var dbFormIppannualFdlrcBak = await _FormIppannualFdlrcBakService.AddFormIppannualFdlrcBak(FormIppannualFdlrcBak);

            if (dbFormIppannualFdlrcBak == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormIppannualFdlrcBak.TbFormIppannualFdlrcBakName} could not be added."
                );
            }

            return CreatedAtAction("GetFormIppannualFdlrcBak", new { uci = FormIppannualFdlrcBak.TbFormIppannualFdlrcBakName }, FormIppannualFdlrcBak);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormIppannualFdlrcBak(FormIppannualFdlrcBak FormIppannualFdlrcBak)
        {           
            FormIppannualFdlrcBak dbFormIppannualFdlrcBak = await _FormIppannualFdlrcBakService.UpdateFormIppannualFdlrcBak(FormIppannualFdlrcBak);

            if (dbFormIppannualFdlrcBak == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormIppannualFdlrcBak.TbFormIppannualFdlrcBakName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormIppannualFdlrcBak(FormIppannualFdlrcBak FormIppannualFdlrcBak)
        {            
            (bool status, string message) = await _FormIppannualFdlrcBakService.DeleteFormIppannualFdlrcBak(FormIppannualFdlrcBak);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormIppannualFdlrcBak);
        }
    }
}
