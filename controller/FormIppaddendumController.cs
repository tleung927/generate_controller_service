using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormIppaddendumController : ControllerBase
    {
        private readonly IFormIppaddendumService _FormIppaddendumService;

        public FormIppaddendumController(IFormIppaddendumService FormIppaddendumService)
        {
            _FormIppaddendumService = FormIppaddendumService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIppaddendumList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormIppaddendums = await _FormIppaddendumService.GetFormIppaddendumListByValue(offset, limit, val);

            if (FormIppaddendums == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormIppaddendums in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormIppaddendums);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIppaddendumList(string FormIppaddendum_name)
        {
            var FormIppaddendums = await _FormIppaddendumService.GetFormIppaddendumList(FormIppaddendum_name);

            if (FormIppaddendums == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormIppaddendum found for uci: {FormIppaddendum_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormIppaddendums);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIppaddendum(string FormIppaddendum_name)
        {
            var FormIppaddendums = await _FormIppaddendumService.GetFormIppaddendum(FormIppaddendum_name);

            if (FormIppaddendums == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormIppaddendum found for uci: {FormIppaddendum_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormIppaddendums);
        }

        [HttpPost]
        public async Task<ActionResult<FormIppaddendum>> AddFormIppaddendum(FormIppaddendum FormIppaddendum)
        {
            var dbFormIppaddendum = await _FormIppaddendumService.AddFormIppaddendum(FormIppaddendum);

            if (dbFormIppaddendum == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormIppaddendum.TbFormIppaddendumName} could not be added."
                );
            }

            return CreatedAtAction("GetFormIppaddendum", new { uci = FormIppaddendum.TbFormIppaddendumName }, FormIppaddendum);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormIppaddendum(FormIppaddendum FormIppaddendum)
        {           
            FormIppaddendum dbFormIppaddendum = await _FormIppaddendumService.UpdateFormIppaddendum(FormIppaddendum);

            if (dbFormIppaddendum == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormIppaddendum.TbFormIppaddendumName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormIppaddendum(FormIppaddendum FormIppaddendum)
        {            
            (bool status, string message) = await _FormIppaddendumService.DeleteFormIppaddendum(FormIppaddendum);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormIppaddendum);
        }
    }
}
