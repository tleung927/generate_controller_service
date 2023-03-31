using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormLawEnforceController : ControllerBase
    {
        private readonly IFormLawEnforceService _FormLawEnforceService;

        public FormLawEnforceController(IFormLawEnforceService FormLawEnforceService)
        {
            _FormLawEnforceService = FormLawEnforceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormLawEnforceList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormLawEnforces = await _FormLawEnforceService.GetFormLawEnforceListByValue(offset, limit, val);

            if (FormLawEnforces == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormLawEnforces in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormLawEnforces);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormLawEnforceList(string FormLawEnforce_name)
        {
            var FormLawEnforces = await _FormLawEnforceService.GetFormLawEnforceList(FormLawEnforce_name);

            if (FormLawEnforces == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormLawEnforce found for uci: {FormLawEnforce_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormLawEnforces);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormLawEnforce(string FormLawEnforce_name)
        {
            var FormLawEnforces = await _FormLawEnforceService.GetFormLawEnforce(FormLawEnforce_name);

            if (FormLawEnforces == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormLawEnforce found for uci: {FormLawEnforce_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormLawEnforces);
        }

        [HttpPost]
        public async Task<ActionResult<FormLawEnforce>> AddFormLawEnforce(FormLawEnforce FormLawEnforce)
        {
            var dbFormLawEnforce = await _FormLawEnforceService.AddFormLawEnforce(FormLawEnforce);

            if (dbFormLawEnforce == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormLawEnforce.TbFormLawEnforceName} could not be added."
                );
            }

            return CreatedAtAction("GetFormLawEnforce", new { uci = FormLawEnforce.TbFormLawEnforceName }, FormLawEnforce);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormLawEnforce(FormLawEnforce FormLawEnforce)
        {           
            FormLawEnforce dbFormLawEnforce = await _FormLawEnforceService.UpdateFormLawEnforce(FormLawEnforce);

            if (dbFormLawEnforce == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormLawEnforce.TbFormLawEnforceName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormLawEnforce(FormLawEnforce FormLawEnforce)
        {            
            (bool status, string message) = await _FormLawEnforceService.DeleteFormLawEnforce(FormLawEnforce);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormLawEnforce);
        }
    }
}
