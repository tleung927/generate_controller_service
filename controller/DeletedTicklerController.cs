using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeletedTicklerController : ControllerBase
    {
        private readonly IDeletedTicklerService _DeletedTicklerService;

        public DeletedTicklerController(IDeletedTicklerService DeletedTicklerService)
        {
            _DeletedTicklerService = DeletedTicklerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDeletedTicklerList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var DeletedTicklers = await _DeletedTicklerService.GetDeletedTicklerListByValue(offset, limit, val);

            if (DeletedTicklers == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No DeletedTicklers in database");
            }

            return StatusCode(StatusCodes.Status200OK, DeletedTicklers);
        }

        [HttpGet]
        public async Task<IActionResult> GetDeletedTicklerList(string DeletedTickler_name)
        {
            var DeletedTicklers = await _DeletedTicklerService.GetDeletedTicklerList(DeletedTickler_name);

            if (DeletedTicklers == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No DeletedTickler found for uci: {DeletedTickler_name}");
            }

            return StatusCode(StatusCodes.Status200OK, DeletedTicklers);
        }

        [HttpGet]
        public async Task<IActionResult> GetDeletedTickler(string DeletedTickler_name)
        {
            var DeletedTicklers = await _DeletedTicklerService.GetDeletedTickler(DeletedTickler_name);

            if (DeletedTicklers == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No DeletedTickler found for uci: {DeletedTickler_name}");
            }

            return StatusCode(StatusCodes.Status200OK, DeletedTicklers);
        }

        [HttpPost]
        public async Task<ActionResult<DeletedTickler>> AddDeletedTickler(DeletedTickler DeletedTickler)
        {
            var dbDeletedTickler = await _DeletedTicklerService.AddDeletedTickler(DeletedTickler);

            if (dbDeletedTickler == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{DeletedTickler.TbDeletedTicklerName} could not be added."
                );
            }

            return CreatedAtAction("GetDeletedTickler", new { uci = DeletedTickler.TbDeletedTicklerName }, DeletedTickler);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDeletedTickler(DeletedTickler DeletedTickler)
        {           
            DeletedTickler dbDeletedTickler = await _DeletedTicklerService.UpdateDeletedTickler(DeletedTickler);

            if (dbDeletedTickler == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{DeletedTickler.TbDeletedTicklerName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDeletedTickler(DeletedTickler DeletedTickler)
        {            
            (bool status, string message) = await _DeletedTicklerService.DeleteDeletedTickler(DeletedTickler);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, DeletedTickler);
        }
    }
}
