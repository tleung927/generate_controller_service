using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormCaseLoadDelegateController : ControllerBase
    {
        private readonly IFormCaseLoadDelegateService _FormCaseLoadDelegateService;

        public FormCaseLoadDelegateController(IFormCaseLoadDelegateService FormCaseLoadDelegateService)
        {
            _FormCaseLoadDelegateService = FormCaseLoadDelegateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormCaseLoadDelegateList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormCaseLoadDelegates = await _FormCaseLoadDelegateService.GetFormCaseLoadDelegateListByValue(offset, limit, val);

            if (FormCaseLoadDelegates == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormCaseLoadDelegates in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormCaseLoadDelegates);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormCaseLoadDelegateList(string FormCaseLoadDelegate_name)
        {
            var FormCaseLoadDelegates = await _FormCaseLoadDelegateService.GetFormCaseLoadDelegateList(FormCaseLoadDelegate_name);

            if (FormCaseLoadDelegates == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormCaseLoadDelegate found for uci: {FormCaseLoadDelegate_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormCaseLoadDelegates);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormCaseLoadDelegate(string FormCaseLoadDelegate_name)
        {
            var FormCaseLoadDelegates = await _FormCaseLoadDelegateService.GetFormCaseLoadDelegate(FormCaseLoadDelegate_name);

            if (FormCaseLoadDelegates == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormCaseLoadDelegate found for uci: {FormCaseLoadDelegate_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormCaseLoadDelegates);
        }

        [HttpPost]
        public async Task<ActionResult<FormCaseLoadDelegate>> AddFormCaseLoadDelegate(FormCaseLoadDelegate FormCaseLoadDelegate)
        {
            var dbFormCaseLoadDelegate = await _FormCaseLoadDelegateService.AddFormCaseLoadDelegate(FormCaseLoadDelegate);

            if (dbFormCaseLoadDelegate == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormCaseLoadDelegate.TbFormCaseLoadDelegateName} could not be added."
                );
            }

            return CreatedAtAction("GetFormCaseLoadDelegate", new { uci = FormCaseLoadDelegate.TbFormCaseLoadDelegateName }, FormCaseLoadDelegate);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormCaseLoadDelegate(FormCaseLoadDelegate FormCaseLoadDelegate)
        {           
            FormCaseLoadDelegate dbFormCaseLoadDelegate = await _FormCaseLoadDelegateService.UpdateFormCaseLoadDelegate(FormCaseLoadDelegate);

            if (dbFormCaseLoadDelegate == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormCaseLoadDelegate.TbFormCaseLoadDelegateName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormCaseLoadDelegate(FormCaseLoadDelegate FormCaseLoadDelegate)
        {            
            (bool status, string message) = await _FormCaseLoadDelegateService.DeleteFormCaseLoadDelegate(FormCaseLoadDelegate);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormCaseLoadDelegate);
        }
    }
}
