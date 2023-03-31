using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EsrDelayController : ControllerBase
    {
        private readonly IEsrDelayService _EsrDelayService;

        public EsrDelayController(IEsrDelayService EsrDelayService)
        {
            _EsrDelayService = EsrDelayService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEsrDelayList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var EsrDelays = await _EsrDelayService.GetEsrDelayListByValue(offset, limit, val);

            if (EsrDelays == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No EsrDelays in database");
            }

            return StatusCode(StatusCodes.Status200OK, EsrDelays);
        }

        [HttpGet]
        public async Task<IActionResult> GetEsrDelayList(string EsrDelay_name)
        {
            var EsrDelays = await _EsrDelayService.GetEsrDelayList(EsrDelay_name);

            if (EsrDelays == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No EsrDelay found for uci: {EsrDelay_name}");
            }

            return StatusCode(StatusCodes.Status200OK, EsrDelays);
        }

        [HttpGet]
        public async Task<IActionResult> GetEsrDelay(string EsrDelay_name)
        {
            var EsrDelays = await _EsrDelayService.GetEsrDelay(EsrDelay_name);

            if (EsrDelays == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No EsrDelay found for uci: {EsrDelay_name}");
            }

            return StatusCode(StatusCodes.Status200OK, EsrDelays);
        }

        [HttpPost]
        public async Task<ActionResult<EsrDelay>> AddEsrDelay(EsrDelay EsrDelay)
        {
            var dbEsrDelay = await _EsrDelayService.AddEsrDelay(EsrDelay);

            if (dbEsrDelay == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{EsrDelay.TbEsrDelayName} could not be added."
                );
            }

            return CreatedAtAction("GetEsrDelay", new { uci = EsrDelay.TbEsrDelayName }, EsrDelay);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEsrDelay(EsrDelay EsrDelay)
        {           
            EsrDelay dbEsrDelay = await _EsrDelayService.UpdateEsrDelay(EsrDelay);

            if (dbEsrDelay == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{EsrDelay.TbEsrDelayName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEsrDelay(EsrDelay EsrDelay)
        {            
            (bool status, string message) = await _EsrDelayService.DeleteEsrDelay(EsrDelay);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, EsrDelay);
        }
    }
}
