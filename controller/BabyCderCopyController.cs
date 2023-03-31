using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BabyCderCopyController : ControllerBase
    {
        private readonly IBabyCderCopyService _BabyCderCopyService;

        public BabyCderCopyController(IBabyCderCopyService BabyCderCopyService)
        {
            _BabyCderCopyService = BabyCderCopyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBabyCderCopyList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var BabyCderCopys = await _BabyCderCopyService.GetBabyCderCopyListByValue(offset, limit, val);

            if (BabyCderCopys == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No BabyCderCopys in database");
            }

            return StatusCode(StatusCodes.Status200OK, BabyCderCopys);
        }

        [HttpGet]
        public async Task<IActionResult> GetBabyCderCopyList(string BabyCderCopy_name)
        {
            var BabyCderCopys = await _BabyCderCopyService.GetBabyCderCopyList(BabyCderCopy_name);

            if (BabyCderCopys == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No BabyCderCopy found for uci: {BabyCderCopy_name}");
            }

            return StatusCode(StatusCodes.Status200OK, BabyCderCopys);
        }

        [HttpGet]
        public async Task<IActionResult> GetBabyCderCopy(string BabyCderCopy_name)
        {
            var BabyCderCopys = await _BabyCderCopyService.GetBabyCderCopy(BabyCderCopy_name);

            if (BabyCderCopys == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No BabyCderCopy found for uci: {BabyCderCopy_name}");
            }

            return StatusCode(StatusCodes.Status200OK, BabyCderCopys);
        }

        [HttpPost]
        public async Task<ActionResult<BabyCderCopy>> AddBabyCderCopy(BabyCderCopy BabyCderCopy)
        {
            var dbBabyCderCopy = await _BabyCderCopyService.AddBabyCderCopy(BabyCderCopy);

            if (dbBabyCderCopy == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{BabyCderCopy.TbBabyCderCopyName} could not be added."
                );
            }

            return CreatedAtAction("GetBabyCderCopy", new { uci = BabyCderCopy.TbBabyCderCopyName }, BabyCderCopy);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBabyCderCopy(BabyCderCopy BabyCderCopy)
        {           
            BabyCderCopy dbBabyCderCopy = await _BabyCderCopyService.UpdateBabyCderCopy(BabyCderCopy);

            if (dbBabyCderCopy == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{BabyCderCopy.TbBabyCderCopyName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBabyCderCopy(BabyCderCopy BabyCderCopy)
        {            
            (bool status, string message) = await _BabyCderCopyService.DeleteBabyCderCopy(BabyCderCopy);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, BabyCderCopy);
        }
    }
}
