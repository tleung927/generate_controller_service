using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormAnnualContactController : ControllerBase
    {
        private readonly IFormAnnualContactService _FormAnnualContactService;

        public FormAnnualContactController(IFormAnnualContactService FormAnnualContactService)
        {
            _FormAnnualContactService = FormAnnualContactService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormAnnualContactList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormAnnualContacts = await _FormAnnualContactService.GetFormAnnualContactListByValue(offset, limit, val);

            if (FormAnnualContacts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormAnnualContacts in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormAnnualContacts);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormAnnualContactList(string FormAnnualContact_name)
        {
            var FormAnnualContacts = await _FormAnnualContactService.GetFormAnnualContactList(FormAnnualContact_name);

            if (FormAnnualContacts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormAnnualContact found for uci: {FormAnnualContact_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormAnnualContacts);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormAnnualContact(string FormAnnualContact_name)
        {
            var FormAnnualContacts = await _FormAnnualContactService.GetFormAnnualContact(FormAnnualContact_name);

            if (FormAnnualContacts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormAnnualContact found for uci: {FormAnnualContact_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormAnnualContacts);
        }

        [HttpPost]
        public async Task<ActionResult<FormAnnualContact>> AddFormAnnualContact(FormAnnualContact FormAnnualContact)
        {
            var dbFormAnnualContact = await _FormAnnualContactService.AddFormAnnualContact(FormAnnualContact);

            if (dbFormAnnualContact == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormAnnualContact.TbFormAnnualContactName} could not be added."
                );
            }

            return CreatedAtAction("GetFormAnnualContact", new { uci = FormAnnualContact.TbFormAnnualContactName }, FormAnnualContact);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormAnnualContact(FormAnnualContact FormAnnualContact)
        {           
            FormAnnualContact dbFormAnnualContact = await _FormAnnualContactService.UpdateFormAnnualContact(FormAnnualContact);

            if (dbFormAnnualContact == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormAnnualContact.TbFormAnnualContactName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormAnnualContact(FormAnnualContact FormAnnualContact)
        {            
            (bool status, string message) = await _FormAnnualContactService.DeleteFormAnnualContact(FormAnnualContact);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormAnnualContact);
        }
    }
}
