using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Scliadr2Controller : ControllerBase
    {
        private readonly IScliadr2Service _Scliadr2Service;

        public Scliadr2Controller(IScliadr2Service Scliadr2Service)
        {
            _Scliadr2Service = Scliadr2Service;
        }

        [HttpGet]
        public async Task<IActionResult> GetScliadr2List(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Scliadr2s = await _Scliadr2Service.GetScliadr2ListByValue(offset, limit, val);

            if (Scliadr2s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Scliadr2s in database");
            }

            return StatusCode(StatusCodes.Status200OK, Scliadr2s);
        }

        [HttpGet]
        public async Task<IActionResult> GetScliadr2List(string Scliadr2_name)
        {
            var Scliadr2s = await _Scliadr2Service.GetScliadr2List(Scliadr2_name);

            if (Scliadr2s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Scliadr2 found for uci: {Scliadr2_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Scliadr2s);
        }

        [HttpGet]
        public async Task<IActionResult> GetScliadr2(string Scliadr2_name)
        {
            var Scliadr2s = await _Scliadr2Service.GetScliadr2(Scliadr2_name);

            if (Scliadr2s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Scliadr2 found for uci: {Scliadr2_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Scliadr2s);
        }

        [HttpPost]
        public async Task<ActionResult<Scliadr2>> AddScliadr2(Scliadr2 Scliadr2)
        {
            var dbScliadr2 = await _Scliadr2Service.AddScliadr2(Scliadr2);

            if (dbScliadr2 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Scliadr2.TbScliadr2Name} could not be added."
                );
            }

            return CreatedAtAction("GetScliadr2", new { uci = Scliadr2.TbScliadr2Name }, Scliadr2);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateScliadr2(Scliadr2 Scliadr2)
        {           
            Scliadr2 dbScliadr2 = await _Scliadr2Service.UpdateScliadr2(Scliadr2);

            if (dbScliadr2 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Scliadr2.TbScliadr2Name} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteScliadr2(Scliadr2 Scliadr2)
        {            
            (bool status, string message) = await _Scliadr2Service.DeleteScliadr2(Scliadr2);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Scliadr2);
        }
    }
}
