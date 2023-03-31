using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewRelationshipController : ControllerBase
    {
        private readonly IViewRelationshipService _ViewRelationshipService;

        public ViewRelationshipController(IViewRelationshipService ViewRelationshipService)
        {
            _ViewRelationshipService = ViewRelationshipService;
        }

        [HttpGet]
        public async Task<IActionResult> GetViewRelationshipList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ViewRelationships = await _ViewRelationshipService.GetViewRelationshipListByValue(offset, limit, val);

            if (ViewRelationships == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ViewRelationships in database");
            }

            return StatusCode(StatusCodes.Status200OK, ViewRelationships);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewRelationshipList(string ViewRelationship_name)
        {
            var ViewRelationships = await _ViewRelationshipService.GetViewRelationshipList(ViewRelationship_name);

            if (ViewRelationships == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewRelationship found for uci: {ViewRelationship_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewRelationships);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewRelationship(string ViewRelationship_name)
        {
            var ViewRelationships = await _ViewRelationshipService.GetViewRelationship(ViewRelationship_name);

            if (ViewRelationships == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewRelationship found for uci: {ViewRelationship_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewRelationships);
        }

        [HttpPost]
        public async Task<ActionResult<ViewRelationship>> AddViewRelationship(ViewRelationship ViewRelationship)
        {
            var dbViewRelationship = await _ViewRelationshipService.AddViewRelationship(ViewRelationship);

            if (dbViewRelationship == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewRelationship.TbViewRelationshipName} could not be added."
                );
            }

            return CreatedAtAction("GetViewRelationship", new { uci = ViewRelationship.TbViewRelationshipName }, ViewRelationship);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViewRelationship(ViewRelationship ViewRelationship)
        {           
            ViewRelationship dbViewRelationship = await _ViewRelationshipService.UpdateViewRelationship(ViewRelationship);

            if (dbViewRelationship == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewRelationship.TbViewRelationshipName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteViewRelationship(ViewRelationship ViewRelationship)
        {            
            (bool status, string message) = await _ViewRelationshipService.DeleteViewRelationship(ViewRelationship);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ViewRelationship);
        }
    }
}
