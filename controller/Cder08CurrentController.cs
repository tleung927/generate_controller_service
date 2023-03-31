using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Cder08CurrentController : ControllerBase
    {
        private readonly ICder08CurrentService _Cder08CurrentService;

        public Cder08CurrentController(ICder08CurrentService Cder08CurrentService)
        {
            _Cder08CurrentService = Cder08CurrentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCder08CurrentList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Cder08Currents = await _Cder08CurrentService.GetCder08CurrentListByValue(offset, limit, val);

            if (Cder08Currents == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Cder08Currents in database");
            }

            return StatusCode(StatusCodes.Status200OK, Cder08Currents);
        }

        [HttpGet]
        public async Task<IActionResult> GetCder08CurrentList(string Cder08Current_name)
        {
            var Cder08Currents = await _Cder08CurrentService.GetCder08CurrentList(Cder08Current_name);

            if (Cder08Currents == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Cder08Current found for uci: {Cder08Current_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Cder08Currents);
        }

        [HttpGet]
        public async Task<IActionResult> GetCder08Current(string Cder08Current_name)
        {
            var Cder08Currents = await _Cder08CurrentService.GetCder08Current(Cder08Current_name);

            if (Cder08Currents == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Cder08Current found for uci: {Cder08Current_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Cder08Currents);
        }

        [HttpPost]
        public async Task<ActionResult<Cder08Current>> AddCder08Current(Cder08Current Cder08Current)
        {
            var dbCder08Current = await _Cder08CurrentService.AddCder08Current(Cder08Current);

            if (dbCder08Current == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Cder08Current.TbCder08CurrentName} could not be added."
                );
            }

            return CreatedAtAction("GetCder08Current", new { uci = Cder08Current.TbCder08CurrentName }, Cder08Current);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCder08Current(Cder08Current Cder08Current)
        {           
            Cder08Current dbCder08Current = await _Cder08CurrentService.UpdateCder08Current(Cder08Current);

            if (dbCder08Current == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Cder08Current.TbCder08CurrentName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCder08Current(Cder08Current Cder08Current)
        {            
            (bool status, string message) = await _Cder08CurrentService.DeleteCder08Current(Cder08Current);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Cder08Current);
        }
    }
}
