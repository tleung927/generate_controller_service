using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SlController : ControllerBase
    {
        private readonly ISlService _SlService;

        public SlController(ISlService SlService)
        {
            _SlService = SlService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSlList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Sls = await _SlService.GetSlListByValue(offset, limit, val);

            if (Sls == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Sls in database");
            }

            return StatusCode(StatusCodes.Status200OK, Sls);
        }

        [HttpGet]
        public async Task<IActionResult> GetSlList(string Sl_name)
        {
            var Sls = await _SlService.GetSlList(Sl_name);

            if (Sls == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Sl found for uci: {Sl_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Sls);
        }

        [HttpGet]
        public async Task<IActionResult> GetSl(string Sl_name)
        {
            var Sls = await _SlService.GetSl(Sl_name);

            if (Sls == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Sl found for uci: {Sl_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Sls);
        }

        [HttpPost]
        public async Task<ActionResult<Sl>> AddSl(Sl Sl)
        {
            var dbSl = await _SlService.AddSl(Sl);

            if (dbSl == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Sl.TbSlName} could not be added."
                );
            }

            return CreatedAtAction("GetSl", new { uci = Sl.TbSlName }, Sl);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSl(Sl Sl)
        {           
            Sl dbSl = await _SlService.UpdateSl(Sl);

            if (dbSl == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Sl.TbSlName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSl(Sl Sl)
        {            
            (bool status, string message) = await _SlService.DeleteSl(Sl);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Sl);
        }
    }
}
