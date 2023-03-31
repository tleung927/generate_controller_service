using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResourceController : ControllerBase
    {
        private readonly IResourceService _ResourceService;

        public ResourceController(IResourceService ResourceService)
        {
            _ResourceService = ResourceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetResourceList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Resources = await _ResourceService.GetResourceListByValue(offset, limit, val);

            if (Resources == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Resources in database");
            }

            return StatusCode(StatusCodes.Status200OK, Resources);
        }

        [HttpGet]
        public async Task<IActionResult> GetResourceList(string Resource_name)
        {
            var Resources = await _ResourceService.GetResourceList(Resource_name);

            if (Resources == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Resource found for uci: {Resource_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Resources);
        }

        [HttpGet]
        public async Task<IActionResult> GetResource(string Resource_name)
        {
            var Resources = await _ResourceService.GetResource(Resource_name);

            if (Resources == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Resource found for uci: {Resource_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Resources);
        }

        [HttpPost]
        public async Task<ActionResult<Resource>> AddResource(Resource Resource)
        {
            var dbResource = await _ResourceService.AddResource(Resource);

            if (dbResource == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Resource.TbResourceName} could not be added."
                );
            }

            return CreatedAtAction("GetResource", new { uci = Resource.TbResourceName }, Resource);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateResource(Resource Resource)
        {           
            Resource dbResource = await _ResourceService.UpdateResource(Resource);

            if (dbResource == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Resource.TbResourceName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteResource(Resource Resource)
        {            
            (bool status, string message) = await _ResourceService.DeleteResource(Resource);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Resource);
        }
    }
}
