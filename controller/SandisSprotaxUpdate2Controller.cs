using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SandisSprotaxUpdate2Controller : ControllerBase
    {
        private readonly ISandisSprotaxUpdate2Service _SandisSprotaxUpdate2Service;

        public SandisSprotaxUpdate2Controller(ISandisSprotaxUpdate2Service SandisSprotaxUpdate2Service)
        {
            _SandisSprotaxUpdate2Service = SandisSprotaxUpdate2Service;
        }

        [HttpGet]
        public async Task<IActionResult> GetSandisSprotaxUpdate2List(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var SandisSprotaxUpdate2s = await _SandisSprotaxUpdate2Service.GetSandisSprotaxUpdate2ListByValue(offset, limit, val);

            if (SandisSprotaxUpdate2s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No SandisSprotaxUpdate2s in database");
            }

            return StatusCode(StatusCodes.Status200OK, SandisSprotaxUpdate2s);
        }

        [HttpGet]
        public async Task<IActionResult> GetSandisSprotaxUpdate2List(string SandisSprotaxUpdate2_name)
        {
            var SandisSprotaxUpdate2s = await _SandisSprotaxUpdate2Service.GetSandisSprotaxUpdate2List(SandisSprotaxUpdate2_name);

            if (SandisSprotaxUpdate2s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No SandisSprotaxUpdate2 found for uci: {SandisSprotaxUpdate2_name}");
            }

            return StatusCode(StatusCodes.Status200OK, SandisSprotaxUpdate2s);
        }

        [HttpGet]
        public async Task<IActionResult> GetSandisSprotaxUpdate2(string SandisSprotaxUpdate2_name)
        {
            var SandisSprotaxUpdate2s = await _SandisSprotaxUpdate2Service.GetSandisSprotaxUpdate2(SandisSprotaxUpdate2_name);

            if (SandisSprotaxUpdate2s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No SandisSprotaxUpdate2 found for uci: {SandisSprotaxUpdate2_name}");
            }

            return StatusCode(StatusCodes.Status200OK, SandisSprotaxUpdate2s);
        }

        [HttpPost]
        public async Task<ActionResult<SandisSprotaxUpdate2>> AddSandisSprotaxUpdate2(SandisSprotaxUpdate2 SandisSprotaxUpdate2)
        {
            var dbSandisSprotaxUpdate2 = await _SandisSprotaxUpdate2Service.AddSandisSprotaxUpdate2(SandisSprotaxUpdate2);

            if (dbSandisSprotaxUpdate2 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{SandisSprotaxUpdate2.TbSandisSprotaxUpdate2Name} could not be added."
                );
            }

            return CreatedAtAction("GetSandisSprotaxUpdate2", new { uci = SandisSprotaxUpdate2.TbSandisSprotaxUpdate2Name }, SandisSprotaxUpdate2);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSandisSprotaxUpdate2(SandisSprotaxUpdate2 SandisSprotaxUpdate2)
        {           
            SandisSprotaxUpdate2 dbSandisSprotaxUpdate2 = await _SandisSprotaxUpdate2Service.UpdateSandisSprotaxUpdate2(SandisSprotaxUpdate2);

            if (dbSandisSprotaxUpdate2 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{SandisSprotaxUpdate2.TbSandisSprotaxUpdate2Name} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSandisSprotaxUpdate2(SandisSprotaxUpdate2 SandisSprotaxUpdate2)
        {            
            (bool status, string message) = await _SandisSprotaxUpdate2Service.DeleteSandisSprotaxUpdate2(SandisSprotaxUpdate2);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, SandisSprotaxUpdate2);
        }
    }
}
