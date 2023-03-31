using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Mwc03082021Controller : ControllerBase
    {
        private readonly IMwc03082021Service _Mwc03082021Service;

        public Mwc03082021Controller(IMwc03082021Service Mwc03082021Service)
        {
            _Mwc03082021Service = Mwc03082021Service;
        }

        [HttpGet]
        public async Task<IActionResult> GetMwc03082021List(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Mwc03082021s = await _Mwc03082021Service.GetMwc03082021ListByValue(offset, limit, val);

            if (Mwc03082021s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Mwc03082021s in database");
            }

            return StatusCode(StatusCodes.Status200OK, Mwc03082021s);
        }

        [HttpGet]
        public async Task<IActionResult> GetMwc03082021List(string Mwc03082021_name)
        {
            var Mwc03082021s = await _Mwc03082021Service.GetMwc03082021List(Mwc03082021_name);

            if (Mwc03082021s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Mwc03082021 found for uci: {Mwc03082021_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Mwc03082021s);
        }

        [HttpGet]
        public async Task<IActionResult> GetMwc03082021(string Mwc03082021_name)
        {
            var Mwc03082021s = await _Mwc03082021Service.GetMwc03082021(Mwc03082021_name);

            if (Mwc03082021s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Mwc03082021 found for uci: {Mwc03082021_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Mwc03082021s);
        }

        [HttpPost]
        public async Task<ActionResult<Mwc03082021>> AddMwc03082021(Mwc03082021 Mwc03082021)
        {
            var dbMwc03082021 = await _Mwc03082021Service.AddMwc03082021(Mwc03082021);

            if (dbMwc03082021 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Mwc03082021.TbMwc03082021Name} could not be added."
                );
            }

            return CreatedAtAction("GetMwc03082021", new { uci = Mwc03082021.TbMwc03082021Name }, Mwc03082021);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMwc03082021(Mwc03082021 Mwc03082021)
        {           
            Mwc03082021 dbMwc03082021 = await _Mwc03082021Service.UpdateMwc03082021(Mwc03082021);

            if (dbMwc03082021 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Mwc03082021.TbMwc03082021Name} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMwc03082021(Mwc03082021 Mwc03082021)
        {            
            (bool status, string message) = await _Mwc03082021Service.DeleteMwc03082021(Mwc03082021);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Mwc03082021);
        }
    }
}
