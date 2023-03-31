using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormEsrTrfController : ControllerBase
    {
        private readonly IFormEsrTrfService _FormEsrTrfService;

        public FormEsrTrfController(IFormEsrTrfService FormEsrTrfService)
        {
            _FormEsrTrfService = FormEsrTrfService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormEsrTrfList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormEsrTrfs = await _FormEsrTrfService.GetFormEsrTrfListByValue(offset, limit, val);

            if (FormEsrTrfs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormEsrTrfs in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormEsrTrfs);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormEsrTrfList(string FormEsrTrf_name)
        {
            var FormEsrTrfs = await _FormEsrTrfService.GetFormEsrTrfList(FormEsrTrf_name);

            if (FormEsrTrfs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormEsrTrf found for uci: {FormEsrTrf_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormEsrTrfs);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormEsrTrf(string FormEsrTrf_name)
        {
            var FormEsrTrfs = await _FormEsrTrfService.GetFormEsrTrf(FormEsrTrf_name);

            if (FormEsrTrfs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormEsrTrf found for uci: {FormEsrTrf_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormEsrTrfs);
        }

        [HttpPost]
        public async Task<ActionResult<FormEsrTrf>> AddFormEsrTrf(FormEsrTrf FormEsrTrf)
        {
            var dbFormEsrTrf = await _FormEsrTrfService.AddFormEsrTrf(FormEsrTrf);

            if (dbFormEsrTrf == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormEsrTrf.TbFormEsrTrfName} could not be added."
                );
            }

            return CreatedAtAction("GetFormEsrTrf", new { uci = FormEsrTrf.TbFormEsrTrfName }, FormEsrTrf);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormEsrTrf(FormEsrTrf FormEsrTrf)
        {           
            FormEsrTrf dbFormEsrTrf = await _FormEsrTrfService.UpdateFormEsrTrf(FormEsrTrf);

            if (dbFormEsrTrf == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormEsrTrf.TbFormEsrTrfName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormEsrTrf(FormEsrTrf FormEsrTrf)
        {            
            (bool status, string message) = await _FormEsrTrfService.DeleteFormEsrTrf(FormEsrTrf);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormEsrTrf);
        }
    }
}
