using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SclicdeController : ControllerBase
    {
        private readonly ISclicdeService _SclicdeService;

        public SclicdeController(ISclicdeService SclicdeService)
        {
            _SclicdeService = SclicdeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSclicdeList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Sclicdes = await _SclicdeService.GetSclicdeListByValue(offset, limit, val);

            if (Sclicdes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Sclicdes in database");
            }

            return StatusCode(StatusCodes.Status200OK, Sclicdes);
        }

        [HttpGet]
        public async Task<IActionResult> GetSclicdeList(string Sclicde_name)
        {
            var Sclicdes = await _SclicdeService.GetSclicdeList(Sclicde_name);

            if (Sclicdes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Sclicde found for uci: {Sclicde_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Sclicdes);
        }

        [HttpGet]
        public async Task<IActionResult> GetSclicde(string Sclicde_name)
        {
            var Sclicdes = await _SclicdeService.GetSclicde(Sclicde_name);

            if (Sclicdes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Sclicde found for uci: {Sclicde_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Sclicdes);
        }

        [HttpPost]
        public async Task<ActionResult<Sclicde>> AddSclicde(Sclicde Sclicde)
        {
            var dbSclicde = await _SclicdeService.AddSclicde(Sclicde);

            if (dbSclicde == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Sclicde.TbSclicdeName} could not be added."
                );
            }

            return CreatedAtAction("GetSclicde", new { uci = Sclicde.TbSclicdeName }, Sclicde);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSclicde(Sclicde Sclicde)
        {           
            Sclicde dbSclicde = await _SclicdeService.UpdateSclicde(Sclicde);

            if (dbSclicde == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Sclicde.TbSclicdeName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSclicde(Sclicde Sclicde)
        {            
            (bool status, string message) = await _SclicdeService.DeleteSclicde(Sclicde);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Sclicde);
        }
    }
}
