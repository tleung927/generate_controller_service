using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KeaDevFormIfspBakController : ControllerBase
    {
        private readonly IKeaDevFormIfspBakService _KeaDevFormIfspBakService;

        public KeaDevFormIfspBakController(IKeaDevFormIfspBakService KeaDevFormIfspBakService)
        {
            _KeaDevFormIfspBakService = KeaDevFormIfspBakService;
        }

        [HttpGet]
        public async Task<IActionResult> GetKeaDevFormIfspBakList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var KeaDevFormIfspBaks = await _KeaDevFormIfspBakService.GetKeaDevFormIfspBakListByValue(offset, limit, val);

            if (KeaDevFormIfspBaks == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No KeaDevFormIfspBaks in database");
            }

            return StatusCode(StatusCodes.Status200OK, KeaDevFormIfspBaks);
        }

        [HttpGet]
        public async Task<IActionResult> GetKeaDevFormIfspBakList(string KeaDevFormIfspBak_name)
        {
            var KeaDevFormIfspBaks = await _KeaDevFormIfspBakService.GetKeaDevFormIfspBakList(KeaDevFormIfspBak_name);

            if (KeaDevFormIfspBaks == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No KeaDevFormIfspBak found for uci: {KeaDevFormIfspBak_name}");
            }

            return StatusCode(StatusCodes.Status200OK, KeaDevFormIfspBaks);
        }

        [HttpGet]
        public async Task<IActionResult> GetKeaDevFormIfspBak(string KeaDevFormIfspBak_name)
        {
            var KeaDevFormIfspBaks = await _KeaDevFormIfspBakService.GetKeaDevFormIfspBak(KeaDevFormIfspBak_name);

            if (KeaDevFormIfspBaks == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No KeaDevFormIfspBak found for uci: {KeaDevFormIfspBak_name}");
            }

            return StatusCode(StatusCodes.Status200OK, KeaDevFormIfspBaks);
        }

        [HttpPost]
        public async Task<ActionResult<KeaDevFormIfspBak>> AddKeaDevFormIfspBak(KeaDevFormIfspBak KeaDevFormIfspBak)
        {
            var dbKeaDevFormIfspBak = await _KeaDevFormIfspBakService.AddKeaDevFormIfspBak(KeaDevFormIfspBak);

            if (dbKeaDevFormIfspBak == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{KeaDevFormIfspBak.TbKeaDevFormIfspBakName} could not be added."
                );
            }

            return CreatedAtAction("GetKeaDevFormIfspBak", new { uci = KeaDevFormIfspBak.TbKeaDevFormIfspBakName }, KeaDevFormIfspBak);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateKeaDevFormIfspBak(KeaDevFormIfspBak KeaDevFormIfspBak)
        {           
            KeaDevFormIfspBak dbKeaDevFormIfspBak = await _KeaDevFormIfspBakService.UpdateKeaDevFormIfspBak(KeaDevFormIfspBak);

            if (dbKeaDevFormIfspBak == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{KeaDevFormIfspBak.TbKeaDevFormIfspBakName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteKeaDevFormIfspBak(KeaDevFormIfspBak KeaDevFormIfspBak)
        {            
            (bool status, string message) = await _KeaDevFormIfspBakService.DeleteKeaDevFormIfspBak(KeaDevFormIfspBak);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, KeaDevFormIfspBak);
        }
    }
}
