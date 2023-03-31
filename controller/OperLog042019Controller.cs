using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OperLog042019Controller : ControllerBase
    {
        private readonly IOperLog042019Service _OperLog042019Service;

        public OperLog042019Controller(IOperLog042019Service OperLog042019Service)
        {
            _OperLog042019Service = OperLog042019Service;
        }

        [HttpGet]
        public async Task<IActionResult> GetOperLog042019List(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var OperLog042019s = await _OperLog042019Service.GetOperLog042019ListByValue(offset, limit, val);

            if (OperLog042019s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No OperLog042019s in database");
            }

            return StatusCode(StatusCodes.Status200OK, OperLog042019s);
        }

        [HttpGet]
        public async Task<IActionResult> GetOperLog042019List(string OperLog042019_name)
        {
            var OperLog042019s = await _OperLog042019Service.GetOperLog042019List(OperLog042019_name);

            if (OperLog042019s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No OperLog042019 found for uci: {OperLog042019_name}");
            }

            return StatusCode(StatusCodes.Status200OK, OperLog042019s);
        }

        [HttpGet]
        public async Task<IActionResult> GetOperLog042019(string OperLog042019_name)
        {
            var OperLog042019s = await _OperLog042019Service.GetOperLog042019(OperLog042019_name);

            if (OperLog042019s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No OperLog042019 found for uci: {OperLog042019_name}");
            }

            return StatusCode(StatusCodes.Status200OK, OperLog042019s);
        }

        [HttpPost]
        public async Task<ActionResult<OperLog042019>> AddOperLog042019(OperLog042019 OperLog042019)
        {
            var dbOperLog042019 = await _OperLog042019Service.AddOperLog042019(OperLog042019);

            if (dbOperLog042019 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{OperLog042019.TbOperLog042019Name} could not be added."
                );
            }

            return CreatedAtAction("GetOperLog042019", new { uci = OperLog042019.TbOperLog042019Name }, OperLog042019);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOperLog042019(OperLog042019 OperLog042019)
        {           
            OperLog042019 dbOperLog042019 = await _OperLog042019Service.UpdateOperLog042019(OperLog042019);

            if (dbOperLog042019 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{OperLog042019.TbOperLog042019Name} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOperLog042019(OperLog042019 OperLog042019)
        {            
            (bool status, string message) = await _OperLog042019Service.DeleteOperLog042019(OperLog042019);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, OperLog042019);
        }
    }
}
