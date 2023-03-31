using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Scliadr1Controller : ControllerBase
    {
        private readonly IScliadr1Service _Scliadr1Service;

        public Scliadr1Controller(IScliadr1Service Scliadr1Service)
        {
            _Scliadr1Service = Scliadr1Service;
        }

        [HttpGet]
        public async Task<IActionResult> GetScliadr1List(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Scliadr1s = await _Scliadr1Service.GetScliadr1ListByValue(offset, limit, val);

            if (Scliadr1s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Scliadr1s in database");
            }

            return StatusCode(StatusCodes.Status200OK, Scliadr1s);
        }

        [HttpGet]
        public async Task<IActionResult> GetScliadr1List(string Scliadr1_name)
        {
            var Scliadr1s = await _Scliadr1Service.GetScliadr1List(Scliadr1_name);

            if (Scliadr1s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Scliadr1 found for uci: {Scliadr1_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Scliadr1s);
        }

        [HttpGet]
        public async Task<IActionResult> GetScliadr1(string Scliadr1_name)
        {
            var Scliadr1s = await _Scliadr1Service.GetScliadr1(Scliadr1_name);

            if (Scliadr1s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Scliadr1 found for uci: {Scliadr1_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Scliadr1s);
        }

        [HttpPost]
        public async Task<ActionResult<Scliadr1>> AddScliadr1(Scliadr1 Scliadr1)
        {
            var dbScliadr1 = await _Scliadr1Service.AddScliadr1(Scliadr1);

            if (dbScliadr1 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Scliadr1.TbScliadr1Name} could not be added."
                );
            }

            return CreatedAtAction("GetScliadr1", new { uci = Scliadr1.TbScliadr1Name }, Scliadr1);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateScliadr1(Scliadr1 Scliadr1)
        {           
            Scliadr1 dbScliadr1 = await _Scliadr1Service.UpdateScliadr1(Scliadr1);

            if (dbScliadr1 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Scliadr1.TbScliadr1Name} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteScliadr1(Scliadr1 Scliadr1)
        {            
            (bool status, string message) = await _Scliadr1Service.DeleteScliadr1(Scliadr1);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Scliadr1);
        }
    }
}
