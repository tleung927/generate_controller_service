using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Cder08Controller : ControllerBase
    {
        private readonly ICder08Service _Cder08Service;

        public Cder08Controller(ICder08Service Cder08Service)
        {
            _Cder08Service = Cder08Service;
        }

        [HttpGet]
        public async Task<IActionResult> GetCder08List(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Cder08s = await _Cder08Service.GetCder08ListByValue(offset, limit, val);

            if (Cder08s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Cder08s in database");
            }

            return StatusCode(StatusCodes.Status200OK, Cder08s);
        }

        [HttpGet]
        public async Task<IActionResult> GetCder08List(string Cder08_name)
        {
            var Cder08s = await _Cder08Service.GetCder08List(Cder08_name);

            if (Cder08s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Cder08 found for uci: {Cder08_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Cder08s);
        }

        [HttpGet]
        public async Task<IActionResult> GetCder08(string Cder08_name)
        {
            var Cder08s = await _Cder08Service.GetCder08(Cder08_name);

            if (Cder08s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Cder08 found for uci: {Cder08_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Cder08s);
        }

        [HttpPost]
        public async Task<ActionResult<Cder08>> AddCder08(Cder08 Cder08)
        {
            var dbCder08 = await _Cder08Service.AddCder08(Cder08);

            if (dbCder08 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Cder08.TbCder08Name} could not be added."
                );
            }

            return CreatedAtAction("GetCder08", new { uci = Cder08.TbCder08Name }, Cder08);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCder08(Cder08 Cder08)
        {           
            Cder08 dbCder08 = await _Cder08Service.UpdateCder08(Cder08);

            if (dbCder08 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Cder08.TbCder08Name} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCder08(Cder08 Cder08)
        {            
            (bool status, string message) = await _Cder08Service.DeleteCder08(Cder08);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Cder08);
        }
    }
}
