using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Mwc11052020Controller : ControllerBase
    {
        private readonly IMwc11052020Service _Mwc11052020Service;

        public Mwc11052020Controller(IMwc11052020Service Mwc11052020Service)
        {
            _Mwc11052020Service = Mwc11052020Service;
        }

        [HttpGet]
        public async Task<IActionResult> GetMwc11052020List(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Mwc11052020s = await _Mwc11052020Service.GetMwc11052020ListByValue(offset, limit, val);

            if (Mwc11052020s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Mwc11052020s in database");
            }

            return StatusCode(StatusCodes.Status200OK, Mwc11052020s);
        }

        [HttpGet]
        public async Task<IActionResult> GetMwc11052020List(string Mwc11052020_name)
        {
            var Mwc11052020s = await _Mwc11052020Service.GetMwc11052020List(Mwc11052020_name);

            if (Mwc11052020s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Mwc11052020 found for uci: {Mwc11052020_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Mwc11052020s);
        }

        [HttpGet]
        public async Task<IActionResult> GetMwc11052020(string Mwc11052020_name)
        {
            var Mwc11052020s = await _Mwc11052020Service.GetMwc11052020(Mwc11052020_name);

            if (Mwc11052020s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Mwc11052020 found for uci: {Mwc11052020_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Mwc11052020s);
        }

        [HttpPost]
        public async Task<ActionResult<Mwc11052020>> AddMwc11052020(Mwc11052020 Mwc11052020)
        {
            var dbMwc11052020 = await _Mwc11052020Service.AddMwc11052020(Mwc11052020);

            if (dbMwc11052020 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Mwc11052020.TbMwc11052020Name} could not be added."
                );
            }

            return CreatedAtAction("GetMwc11052020", new { uci = Mwc11052020.TbMwc11052020Name }, Mwc11052020);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMwc11052020(Mwc11052020 Mwc11052020)
        {           
            Mwc11052020 dbMwc11052020 = await _Mwc11052020Service.UpdateMwc11052020(Mwc11052020);

            if (dbMwc11052020 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Mwc11052020.TbMwc11052020Name} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMwc11052020(Mwc11052020 Mwc11052020)
        {            
            (bool status, string message) = await _Mwc11052020Service.DeleteMwc11052020(Mwc11052020);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Mwc11052020);
        }
    }
}
