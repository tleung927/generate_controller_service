using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResourceLabelController : ControllerBase
    {
        private readonly IResourceLabelService _ResourceLabelService;

        public ResourceLabelController(IResourceLabelService ResourceLabelService)
        {
            _ResourceLabelService = ResourceLabelService;
        }

        [HttpGet]
        public async Task<IActionResult> GetResourceLabelList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ResourceLabels = await _ResourceLabelService.GetResourceLabelListByValue(offset, limit, val);

            if (ResourceLabels == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ResourceLabels in database");
            }

            return StatusCode(StatusCodes.Status200OK, ResourceLabels);
        }

        [HttpGet]
        public async Task<IActionResult> GetResourceLabelList(string ResourceLabel_name)
        {
            var ResourceLabels = await _ResourceLabelService.GetResourceLabelList(ResourceLabel_name);

            if (ResourceLabels == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ResourceLabel found for uci: {ResourceLabel_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ResourceLabels);
        }

        [HttpGet]
        public async Task<IActionResult> GetResourceLabel(string ResourceLabel_name)
        {
            var ResourceLabels = await _ResourceLabelService.GetResourceLabel(ResourceLabel_name);

            if (ResourceLabels == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ResourceLabel found for uci: {ResourceLabel_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ResourceLabels);
        }

        [HttpPost]
        public async Task<ActionResult<ResourceLabel>> AddResourceLabel(ResourceLabel ResourceLabel)
        {
            var dbResourceLabel = await _ResourceLabelService.AddResourceLabel(ResourceLabel);

            if (dbResourceLabel == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ResourceLabel.TbResourceLabelName} could not be added."
                );
            }

            return CreatedAtAction("GetResourceLabel", new { uci = ResourceLabel.TbResourceLabelName }, ResourceLabel);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateResourceLabel(ResourceLabel ResourceLabel)
        {           
            ResourceLabel dbResourceLabel = await _ResourceLabelService.UpdateResourceLabel(ResourceLabel);

            if (dbResourceLabel == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ResourceLabel.TbResourceLabelName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteResourceLabel(ResourceLabel ResourceLabel)
        {            
            (bool status, string message) = await _ResourceLabelService.DeleteResourceLabel(ResourceLabel);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ResourceLabel);
        }
    }
}
