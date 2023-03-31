using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResourceAllController : ControllerBase
    {
        private readonly IResourceAllService _ResourceAllService;

        public ResourceAllController(IResourceAllService ResourceAllService)
        {
            _ResourceAllService = ResourceAllService;
        }

        [HttpGet]
        public async Task<IActionResult> GetResourceAllList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ResourceAlls = await _ResourceAllService.GetResourceAllListByValue(offset, limit, val);

            if (ResourceAlls == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ResourceAlls in database");
            }

            return StatusCode(StatusCodes.Status200OK, ResourceAlls);
        }

        [HttpGet]
        public async Task<IActionResult> GetResourceAllList(string ResourceAll_name)
        {
            var ResourceAlls = await _ResourceAllService.GetResourceAllList(ResourceAll_name);

            if (ResourceAlls == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ResourceAll found for uci: {ResourceAll_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ResourceAlls);
        }

        [HttpGet]
        public async Task<IActionResult> GetResourceAll(string ResourceAll_name)
        {
            var ResourceAlls = await _ResourceAllService.GetResourceAll(ResourceAll_name);

            if (ResourceAlls == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ResourceAll found for uci: {ResourceAll_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ResourceAlls);
        }

        [HttpPost]
        public async Task<ActionResult<ResourceAll>> AddResourceAll(ResourceAll ResourceAll)
        {
            var dbResourceAll = await _ResourceAllService.AddResourceAll(ResourceAll);

            if (dbResourceAll == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ResourceAll.TbResourceAllName} could not be added."
                );
            }

            return CreatedAtAction("GetResourceAll", new { uci = ResourceAll.TbResourceAllName }, ResourceAll);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateResourceAll(ResourceAll ResourceAll)
        {           
            ResourceAll dbResourceAll = await _ResourceAllService.UpdateResourceAll(ResourceAll);

            if (dbResourceAll == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ResourceAll.TbResourceAllName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteResourceAll(ResourceAll ResourceAll)
        {            
            (bool status, string message) = await _ResourceAllService.DeleteResourceAll(ResourceAll);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ResourceAll);
        }
    }
}
