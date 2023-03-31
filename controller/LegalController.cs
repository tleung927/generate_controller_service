using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LegalController : ControllerBase
    {
        private readonly ILegalService _LegalService;

        public LegalController(ILegalService LegalService)
        {
            _LegalService = LegalService;
        }

        [HttpGet]
        public async Task<IActionResult> GetLegalList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Legals = await _LegalService.GetLegalListByValue(offset, limit, val);

            if (Legals == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Legals in database");
            }

            return StatusCode(StatusCodes.Status200OK, Legals);
        }

        [HttpGet]
        public async Task<IActionResult> GetLegalList(string Legal_name)
        {
            var Legals = await _LegalService.GetLegalList(Legal_name);

            if (Legals == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Legal found for uci: {Legal_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Legals);
        }

        [HttpGet]
        public async Task<IActionResult> GetLegal(string Legal_name)
        {
            var Legals = await _LegalService.GetLegal(Legal_name);

            if (Legals == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Legal found for uci: {Legal_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Legals);
        }

        [HttpPost]
        public async Task<ActionResult<Legal>> AddLegal(Legal Legal)
        {
            var dbLegal = await _LegalService.AddLegal(Legal);

            if (dbLegal == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Legal.TbLegalName} could not be added."
                );
            }

            return CreatedAtAction("GetLegal", new { uci = Legal.TbLegalName }, Legal);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLegal(Legal Legal)
        {           
            Legal dbLegal = await _LegalService.UpdateLegal(Legal);

            if (dbLegal == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Legal.TbLegalName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteLegal(Legal Legal)
        {            
            (bool status, string message) = await _LegalService.DeleteLegal(Legal);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Legal);
        }
    }
}
