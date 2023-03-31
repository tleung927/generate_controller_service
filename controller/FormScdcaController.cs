using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormScdcaController : ControllerBase
    {
        private readonly IFormScdcaService _FormScdcaService;

        public FormScdcaController(IFormScdcaService FormScdcaService)
        {
            _FormScdcaService = FormScdcaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormScdcaList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormScdcas = await _FormScdcaService.GetFormScdcaListByValue(offset, limit, val);

            if (FormScdcas == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormScdcas in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormScdcas);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormScdcaList(string FormScdca_name)
        {
            var FormScdcas = await _FormScdcaService.GetFormScdcaList(FormScdca_name);

            if (FormScdcas == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormScdca found for uci: {FormScdca_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormScdcas);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormScdca(string FormScdca_name)
        {
            var FormScdcas = await _FormScdcaService.GetFormScdca(FormScdca_name);

            if (FormScdcas == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormScdca found for uci: {FormScdca_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormScdcas);
        }

        [HttpPost]
        public async Task<ActionResult<FormScdca>> AddFormScdca(FormScdca FormScdca)
        {
            var dbFormScdca = await _FormScdcaService.AddFormScdca(FormScdca);

            if (dbFormScdca == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormScdca.TbFormScdcaName} could not be added."
                );
            }

            return CreatedAtAction("GetFormScdca", new { uci = FormScdca.TbFormScdcaName }, FormScdca);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormScdca(FormScdca FormScdca)
        {           
            FormScdca dbFormScdca = await _FormScdcaService.UpdateFormScdca(FormScdca);

            if (dbFormScdca == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormScdca.TbFormScdcaName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormScdca(FormScdca FormScdca)
        {            
            (bool status, string message) = await _FormScdcaService.DeleteFormScdca(FormScdca);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormScdca);
        }
    }
}
