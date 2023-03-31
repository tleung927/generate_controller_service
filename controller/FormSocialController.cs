using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormSocialController : ControllerBase
    {
        private readonly IFormSocialService _FormSocialService;

        public FormSocialController(IFormSocialService FormSocialService)
        {
            _FormSocialService = FormSocialService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormSocialList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormSocials = await _FormSocialService.GetFormSocialListByValue(offset, limit, val);

            if (FormSocials == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormSocials in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormSocials);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormSocialList(string FormSocial_name)
        {
            var FormSocials = await _FormSocialService.GetFormSocialList(FormSocial_name);

            if (FormSocials == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormSocial found for uci: {FormSocial_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormSocials);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormSocial(string FormSocial_name)
        {
            var FormSocials = await _FormSocialService.GetFormSocial(FormSocial_name);

            if (FormSocials == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormSocial found for uci: {FormSocial_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormSocials);
        }

        [HttpPost]
        public async Task<ActionResult<FormSocial>> AddFormSocial(FormSocial FormSocial)
        {
            var dbFormSocial = await _FormSocialService.AddFormSocial(FormSocial);

            if (dbFormSocial == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormSocial.TbFormSocialName} could not be added."
                );
            }

            return CreatedAtAction("GetFormSocial", new { uci = FormSocial.TbFormSocialName }, FormSocial);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormSocial(FormSocial FormSocial)
        {           
            FormSocial dbFormSocial = await _FormSocialService.UpdateFormSocial(FormSocial);

            if (dbFormSocial == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormSocial.TbFormSocialName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormSocial(FormSocial FormSocial)
        {            
            (bool status, string message) = await _FormSocialService.DeleteFormSocial(FormSocial);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormSocial);
        }
    }
}
