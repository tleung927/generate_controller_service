using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Esr11Controller : ControllerBase
    {
        private readonly IEsr11Service _Esr11Service;

        public Esr11Controller(IEsr11Service Esr11Service)
        {
            _Esr11Service = Esr11Service;
        }

        [HttpGet]
        public async Task<IActionResult> GetEsr11List(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Esr11s = await _Esr11Service.GetEsr11ListByValue(offset, limit, val);

            if (Esr11s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Esr11s in database");
            }

            return StatusCode(StatusCodes.Status200OK, Esr11s);
        }

        [HttpGet]
        public async Task<IActionResult> GetEsr11List(string Esr11_name)
        {
            var Esr11s = await _Esr11Service.GetEsr11List(Esr11_name);

            if (Esr11s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Esr11 found for uci: {Esr11_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Esr11s);
        }

        [HttpGet]
        public async Task<IActionResult> GetEsr11(string Esr11_name)
        {
            var Esr11s = await _Esr11Service.GetEsr11(Esr11_name);

            if (Esr11s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Esr11 found for uci: {Esr11_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Esr11s);
        }

        [HttpPost]
        public async Task<ActionResult<Esr11>> AddEsr11(Esr11 Esr11)
        {
            var dbEsr11 = await _Esr11Service.AddEsr11(Esr11);

            if (dbEsr11 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Esr11.TbEsr11Name} could not be added."
                );
            }

            return CreatedAtAction("GetEsr11", new { uci = Esr11.TbEsr11Name }, Esr11);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEsr11(Esr11 Esr11)
        {           
            Esr11 dbEsr11 = await _Esr11Service.UpdateEsr11(Esr11);

            if (dbEsr11 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Esr11.TbEsr11Name} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEsr11(Esr11 Esr11)
        {            
            (bool status, string message) = await _Esr11Service.DeleteEsr11(Esr11);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Esr11);
        }
    }
}
