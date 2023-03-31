using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeletedResourceController : ControllerBase
    {
        private readonly IDeletedResourceService _DeletedResourceService;

        public DeletedResourceController(IDeletedResourceService DeletedResourceService)
        {
            _DeletedResourceService = DeletedResourceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDeletedResourceList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var DeletedResources = await _DeletedResourceService.GetDeletedResourceListByValue(offset, limit, val);

            if (DeletedResources == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No DeletedResources in database");
            }

            return StatusCode(StatusCodes.Status200OK, DeletedResources);
        }

        [HttpGet]
        public async Task<IActionResult> GetDeletedResourceList(string DeletedResource_name)
        {
            var DeletedResources = await _DeletedResourceService.GetDeletedResourceList(DeletedResource_name);

            if (DeletedResources == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No DeletedResource found for uci: {DeletedResource_name}");
            }

            return StatusCode(StatusCodes.Status200OK, DeletedResources);
        }

        [HttpGet]
        public async Task<IActionResult> GetDeletedResource(string DeletedResource_name)
        {
            var DeletedResources = await _DeletedResourceService.GetDeletedResource(DeletedResource_name);

            if (DeletedResources == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No DeletedResource found for uci: {DeletedResource_name}");
            }

            return StatusCode(StatusCodes.Status200OK, DeletedResources);
        }

        [HttpPost]
        public async Task<ActionResult<DeletedResource>> AddDeletedResource(DeletedResource DeletedResource)
        {
            var dbDeletedResource = await _DeletedResourceService.AddDeletedResource(DeletedResource);

            if (dbDeletedResource == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{DeletedResource.TbDeletedResourceName} could not be added."
                );
            }

            return CreatedAtAction("GetDeletedResource", new { uci = DeletedResource.TbDeletedResourceName }, DeletedResource);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDeletedResource(DeletedResource DeletedResource)
        {           
            DeletedResource dbDeletedResource = await _DeletedResourceService.UpdateDeletedResource(DeletedResource);

            if (dbDeletedResource == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{DeletedResource.TbDeletedResourceName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDeletedResource(DeletedResource DeletedResource)
        {            
            (bool status, string message) = await _DeletedResourceService.DeleteDeletedResource(DeletedResource);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, DeletedResource);
        }
    }
}
