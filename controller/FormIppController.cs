using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormIppController : ControllerBase
    {
        private readonly IFormIppService _FormIppService;

        public FormIppController(IFormIppService FormIppService)
        {
            _FormIppService = FormIppService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIppList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormIpps = await _FormIppService.GetFormIppListByValue(offset, limit, val);

            if (FormIpps == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormIpps in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormIpps);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIppList(string FormIpp_name)
        {
            var FormIpps = await _FormIppService.GetFormIppList(FormIpp_name);

            if (FormIpps == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormIpp found for uci: {FormIpp_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormIpps);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIpp(string FormIpp_name)
        {
            var FormIpps = await _FormIppService.GetFormIpp(FormIpp_name);

            if (FormIpps == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormIpp found for uci: {FormIpp_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormIpps);
        }

        [HttpPost]
        public async Task<ActionResult<FormIpp>> AddFormIpp(FormIpp FormIpp)
        {
            var dbFormIpp = await _FormIppService.AddFormIpp(FormIpp);

            if (dbFormIpp == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormIpp.TbFormIppName} could not be added."
                );
            }

            return CreatedAtAction("GetFormIpp", new { uci = FormIpp.TbFormIppName }, FormIpp);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormIpp(FormIpp FormIpp)
        {           
            FormIpp dbFormIpp = await _FormIppService.UpdateFormIpp(FormIpp);

            if (dbFormIpp == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormIpp.TbFormIppName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormIpp(FormIpp FormIpp)
        {            
            (bool status, string message) = await _FormIppService.DeleteFormIpp(FormIpp);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormIpp);
        }
    }
}
