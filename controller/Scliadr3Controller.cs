using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Scliadr3Controller : ControllerBase
    {
        private readonly IScliadr3Service _Scliadr3Service;

        public Scliadr3Controller(IScliadr3Service Scliadr3Service)
        {
            _Scliadr3Service = Scliadr3Service;
        }

        [HttpGet]
        public async Task<IActionResult> GetScliadr3List(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Scliadr3s = await _Scliadr3Service.GetScliadr3ListByValue(offset, limit, val);

            if (Scliadr3s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Scliadr3s in database");
            }

            return StatusCode(StatusCodes.Status200OK, Scliadr3s);
        }

        [HttpGet]
        public async Task<IActionResult> GetScliadr3List(string Scliadr3_name)
        {
            var Scliadr3s = await _Scliadr3Service.GetScliadr3List(Scliadr3_name);

            if (Scliadr3s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Scliadr3 found for uci: {Scliadr3_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Scliadr3s);
        }

        [HttpGet]
        public async Task<IActionResult> GetScliadr3(string Scliadr3_name)
        {
            var Scliadr3s = await _Scliadr3Service.GetScliadr3(Scliadr3_name);

            if (Scliadr3s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Scliadr3 found for uci: {Scliadr3_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Scliadr3s);
        }

        [HttpPost]
        public async Task<ActionResult<Scliadr3>> AddScliadr3(Scliadr3 Scliadr3)
        {
            var dbScliadr3 = await _Scliadr3Service.AddScliadr3(Scliadr3);

            if (dbScliadr3 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Scliadr3.TbScliadr3Name} could not be added."
                );
            }

            return CreatedAtAction("GetScliadr3", new { uci = Scliadr3.TbScliadr3Name }, Scliadr3);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateScliadr3(Scliadr3 Scliadr3)
        {           
            Scliadr3 dbScliadr3 = await _Scliadr3Service.UpdateScliadr3(Scliadr3);

            if (dbScliadr3 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Scliadr3.TbScliadr3Name} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteScliadr3(Scliadr3 Scliadr3)
        {            
            (bool status, string message) = await _Scliadr3Service.DeleteScliadr3(Scliadr3);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Scliadr3);
        }
    }
}
