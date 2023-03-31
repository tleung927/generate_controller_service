using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class XxxxxxController : ControllerBase
    {
        private readonly IXxxxxxService _XxxxxxService;

        public XxxxxxController(IXxxxxxService XxxxxxService)
        {
            _XxxxxxService = XxxxxxService;
        }

        [HttpGet]
        public async Task<IActionResult> GetXxxxxxList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Xxxxxxs = await _XxxxxxService.GetXxxxxxListByValue(offset, limit, val);

            if (Xxxxxxs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Xxxxxxs in database");
            }

            return StatusCode(StatusCodes.Status200OK, Xxxxxxs);
        }

        [HttpGet]
        public async Task<IActionResult> GetXxxxxxList(string Xxxxxx_name)
        {
            var Xxxxxxs = await _XxxxxxService.GetXxxxxxList(Xxxxxx_name);

            if (Xxxxxxs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Xxxxxx found for uci: {Xxxxxx_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Xxxxxxs);
        }

        [HttpGet]
        public async Task<IActionResult> GetXxxxxx(string Xxxxxx_name)
        {
            var Xxxxxxs = await _XxxxxxService.GetXxxxxx(Xxxxxx_name);

            if (Xxxxxxs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Xxxxxx found for uci: {Xxxxxx_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Xxxxxxs);
        }

        [HttpPost]
        public async Task<ActionResult<Xxxxxx>> AddXxxxxx(Xxxxxx Xxxxxx)
        {
            var dbXxxxxx = await _XxxxxxService.AddXxxxxx(Xxxxxx);

            if (dbXxxxxx == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Xxxxxx.TbXxxxxxName} could not be added."
                );
            }

            return CreatedAtAction("GetXxxxxx", new { uci = Xxxxxx.TbXxxxxxName }, Xxxxxx);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateXxxxxx(Xxxxxx Xxxxxx)
        {           
            Xxxxxx dbXxxxxx = await _XxxxxxService.UpdateXxxxxx(Xxxxxx);

            if (dbXxxxxx == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Xxxxxx.TbXxxxxxName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteXxxxxx(Xxxxxx Xxxxxx)
        {            
            (bool status, string message) = await _XxxxxxService.DeleteXxxxxx(Xxxxxx);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Xxxxxx);
        }
    }
}
