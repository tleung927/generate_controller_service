using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TemplateController : ControllerBase
    {
        private readonly ITemplateService _TemplateService;

        public TemplateController(ITemplateService TemplateService)
        {
            _TemplateService = TemplateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTemplateList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Templates = await _TemplateService.GetTemplateListByValue(offset, limit, val);

            if (Templates == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Templates in database");
            }

            return StatusCode(StatusCodes.Status200OK, Templates);
        }

        [HttpGet]
        public async Task<IActionResult> GetTemplateList(string Template_name)
        {
            var Templates = await _TemplateService.GetTemplateList(Template_name);

            if (Templates == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Template found for uci: {Template_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Templates);
        }

        [HttpGet]
        public async Task<IActionResult> GetTemplate(string Template_name)
        {
            var Templates = await _TemplateService.GetTemplate(Template_name);

            if (Templates == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Template found for uci: {Template_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Templates);
        }

        [HttpPost]
        public async Task<ActionResult<Template>> AddTemplate(Template Template)
        {
            var dbTemplate = await _TemplateService.AddTemplate(Template);

            if (dbTemplate == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Template.TbTemplateName} could not be added."
                );
            }

            return CreatedAtAction("GetTemplate", new { uci = Template.TbTemplateName }, Template);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTemplate(Template Template)
        {           
            Template dbTemplate = await _TemplateService.UpdateTemplate(Template);

            if (dbTemplate == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Template.TbTemplateName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTemplate(Template Template)
        {            
            (bool status, string message) = await _TemplateService.DeleteTemplate(Template);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Template);
        }
    }
}
