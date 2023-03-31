using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormVaController : ControllerBase
    {
        private readonly IFormVaService _FormVaService;

        public FormVaController(IFormVaService FormVaService)
        {
            _FormVaService = FormVaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormVaList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormVas = await _FormVaService.GetFormVaListByValue(offset, limit, val);

            if (FormVas == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormVas in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormVas);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormVaList(string FormVa_name)
        {
            var FormVas = await _FormVaService.GetFormVaList(FormVa_name);

            if (FormVas == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormVa found for uci: {FormVa_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormVas);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormVa(string FormVa_name)
        {
            var FormVas = await _FormVaService.GetFormVa(FormVa_name);

            if (FormVas == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormVa found for uci: {FormVa_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormVas);
        }

        [HttpPost]
        public async Task<ActionResult<FormVa>> AddFormVa(FormVa FormVa)
        {
            var dbFormVa = await _FormVaService.AddFormVa(FormVa);

            if (dbFormVa == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormVa.TbFormVaName} could not be added."
                );
            }

            return CreatedAtAction("GetFormVa", new { uci = FormVa.TbFormVaName }, FormVa);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormVa(FormVa FormVa)
        {           
            FormVa dbFormVa = await _FormVaService.UpdateFormVa(FormVa);

            if (dbFormVa == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormVa.TbFormVaName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormVa(FormVa FormVa)
        {            
            (bool status, string message) = await _FormVaService.DeleteFormVa(FormVa);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormVa);
        }
    }
}
