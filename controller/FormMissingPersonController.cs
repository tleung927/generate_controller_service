using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormMissingPersonController : ControllerBase
    {
        private readonly IFormMissingPersonService _FormMissingPersonService;

        public FormMissingPersonController(IFormMissingPersonService FormMissingPersonService)
        {
            _FormMissingPersonService = FormMissingPersonService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormMissingPersonList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormMissingPersons = await _FormMissingPersonService.GetFormMissingPersonListByValue(offset, limit, val);

            if (FormMissingPersons == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormMissingPersons in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormMissingPersons);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormMissingPersonList(string FormMissingPerson_name)
        {
            var FormMissingPersons = await _FormMissingPersonService.GetFormMissingPersonList(FormMissingPerson_name);

            if (FormMissingPersons == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormMissingPerson found for uci: {FormMissingPerson_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormMissingPersons);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormMissingPerson(string FormMissingPerson_name)
        {
            var FormMissingPersons = await _FormMissingPersonService.GetFormMissingPerson(FormMissingPerson_name);

            if (FormMissingPersons == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormMissingPerson found for uci: {FormMissingPerson_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormMissingPersons);
        }

        [HttpPost]
        public async Task<ActionResult<FormMissingPerson>> AddFormMissingPerson(FormMissingPerson FormMissingPerson)
        {
            var dbFormMissingPerson = await _FormMissingPersonService.AddFormMissingPerson(FormMissingPerson);

            if (dbFormMissingPerson == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormMissingPerson.TbFormMissingPersonName} could not be added."
                );
            }

            return CreatedAtAction("GetFormMissingPerson", new { uci = FormMissingPerson.TbFormMissingPersonName }, FormMissingPerson);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormMissingPerson(FormMissingPerson FormMissingPerson)
        {           
            FormMissingPerson dbFormMissingPerson = await _FormMissingPersonService.UpdateFormMissingPerson(FormMissingPerson);

            if (dbFormMissingPerson == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormMissingPerson.TbFormMissingPersonName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormMissingPerson(FormMissingPerson FormMissingPerson)
        {            
            (bool status, string message) = await _FormMissingPersonService.DeleteFormMissingPerson(FormMissingPerson);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormMissingPerson);
        }
    }
}
