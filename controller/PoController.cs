using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PoController : ControllerBase
    {
        private readonly IPoService _PoService;

        public PoController(IPoService PoService)
        {
            _PoService = PoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPoList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Pos = await _PoService.GetPoListByValue(offset, limit, val);

            if (Pos == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Pos in database");
            }

            return StatusCode(StatusCodes.Status200OK, Pos);
        }

        [HttpGet]
        public async Task<IActionResult> GetPoList(string Po_name)
        {
            var Pos = await _PoService.GetPoList(Po_name);

            if (Pos == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Po found for uci: {Po_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Pos);
        }

        [HttpGet]
        public async Task<IActionResult> GetPo(string Po_name)
        {
            var Pos = await _PoService.GetPo(Po_name);

            if (Pos == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Po found for uci: {Po_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Pos);
        }

        [HttpPost]
        public async Task<ActionResult<Po>> AddPo(Po Po)
        {
            var dbPo = await _PoService.AddPo(Po);

            if (dbPo == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Po.TbPoName} could not be added."
                );
            }

            return CreatedAtAction("GetPo", new { uci = Po.TbPoName }, Po);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePo(Po Po)
        {           
            Po dbPo = await _PoService.UpdatePo(Po);

            if (dbPo == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Po.TbPoName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePo(Po Po)
        {            
            (bool status, string message) = await _PoService.DeletePo(Po);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Po);
        }
    }
}
