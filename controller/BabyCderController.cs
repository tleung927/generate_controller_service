using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BabyCderController : ControllerBase
    {
        private readonly IBabyCderService _BabyCderService;

        public BabyCderController(IBabyCderService BabyCderService)
        {
            _BabyCderService = BabyCderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBabyCderList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var BabyCders = await _BabyCderService.GetBabyCderListByValue(offset, limit, val);

            if (BabyCders == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No BabyCders in database");
            }

            return StatusCode(StatusCodes.Status200OK, BabyCders);
        }

        [HttpGet]
        public async Task<IActionResult> GetBabyCderList(string BabyCder_name)
        {
            var BabyCders = await _BabyCderService.GetBabyCderList(BabyCder_name);

            if (BabyCders == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No BabyCder found for uci: {BabyCder_name}");
            }

            return StatusCode(StatusCodes.Status200OK, BabyCders);
        }

        [HttpGet]
        public async Task<IActionResult> GetBabyCder(string BabyCder_name)
        {
            var BabyCders = await _BabyCderService.GetBabyCder(BabyCder_name);

            if (BabyCders == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No BabyCder found for uci: {BabyCder_name}");
            }

            return StatusCode(StatusCodes.Status200OK, BabyCders);
        }

        [HttpPost]
        public async Task<ActionResult<BabyCder>> AddBabyCder(BabyCder BabyCder)
        {
            var dbBabyCder = await _BabyCderService.AddBabyCder(BabyCder);

            if (dbBabyCder == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{BabyCder.TbBabyCderName} could not be added."
                );
            }

            return CreatedAtAction("GetBabyCder", new { uci = BabyCder.TbBabyCderName }, BabyCder);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBabyCder(BabyCder BabyCder)
        {           
            BabyCder dbBabyCder = await _BabyCderService.UpdateBabyCder(BabyCder);

            if (dbBabyCder == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{BabyCder.TbBabyCderName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBabyCder(BabyCder BabyCder)
        {            
            (bool status, string message) = await _BabyCderService.DeleteBabyCder(BabyCder);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, BabyCder);
        }
    }
}
