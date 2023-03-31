using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormEsrPsychoSocialController : ControllerBase
    {
        private readonly IFormEsrPsychoSocialService _FormEsrPsychoSocialService;

        public FormEsrPsychoSocialController(IFormEsrPsychoSocialService FormEsrPsychoSocialService)
        {
            _FormEsrPsychoSocialService = FormEsrPsychoSocialService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormEsrPsychoSocialList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormEsrPsychoSocials = await _FormEsrPsychoSocialService.GetFormEsrPsychoSocialListByValue(offset, limit, val);

            if (FormEsrPsychoSocials == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormEsrPsychoSocials in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormEsrPsychoSocials);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormEsrPsychoSocialList(string FormEsrPsychoSocial_name)
        {
            var FormEsrPsychoSocials = await _FormEsrPsychoSocialService.GetFormEsrPsychoSocialList(FormEsrPsychoSocial_name);

            if (FormEsrPsychoSocials == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormEsrPsychoSocial found for uci: {FormEsrPsychoSocial_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormEsrPsychoSocials);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormEsrPsychoSocial(string FormEsrPsychoSocial_name)
        {
            var FormEsrPsychoSocials = await _FormEsrPsychoSocialService.GetFormEsrPsychoSocial(FormEsrPsychoSocial_name);

            if (FormEsrPsychoSocials == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormEsrPsychoSocial found for uci: {FormEsrPsychoSocial_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormEsrPsychoSocials);
        }

        [HttpPost]
        public async Task<ActionResult<FormEsrPsychoSocial>> AddFormEsrPsychoSocial(FormEsrPsychoSocial FormEsrPsychoSocial)
        {
            var dbFormEsrPsychoSocial = await _FormEsrPsychoSocialService.AddFormEsrPsychoSocial(FormEsrPsychoSocial);

            if (dbFormEsrPsychoSocial == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormEsrPsychoSocial.TbFormEsrPsychoSocialName} could not be added."
                );
            }

            return CreatedAtAction("GetFormEsrPsychoSocial", new { uci = FormEsrPsychoSocial.TbFormEsrPsychoSocialName }, FormEsrPsychoSocial);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormEsrPsychoSocial(FormEsrPsychoSocial FormEsrPsychoSocial)
        {           
            FormEsrPsychoSocial dbFormEsrPsychoSocial = await _FormEsrPsychoSocialService.UpdateFormEsrPsychoSocial(FormEsrPsychoSocial);

            if (dbFormEsrPsychoSocial == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormEsrPsychoSocial.TbFormEsrPsychoSocialName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormEsrPsychoSocial(FormEsrPsychoSocial FormEsrPsychoSocial)
        {            
            (bool status, string message) = await _FormEsrPsychoSocialService.DeleteFormEsrPsychoSocial(FormEsrPsychoSocial);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormEsrPsychoSocial);
        }
    }
}
