using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActiveConsumerCmController : ControllerBase
    {
        private readonly IActiveConsumerCmService _ActiveConsumerCmService;

        public ActiveConsumerCmController(IActiveConsumerCmService ActiveConsumerCmService)
        {
            _ActiveConsumerCmService = ActiveConsumerCmService;
        }

        [HttpGet]
        public async Task<IActionResult> GetActiveConsumerCmList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ActiveConsumerCms = await _ActiveConsumerCmService.GetActiveConsumerCmListByValue(offset, limit, val);

            if (ActiveConsumerCms == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ActiveConsumerCms in database");
            }

            return StatusCode(StatusCodes.Status200OK, ActiveConsumerCms);
        }

        [HttpGet]
        public async Task<IActionResult> GetActiveConsumerCmList(string ActiveConsumerCm_name)
        {
            var ActiveConsumerCms = await _ActiveConsumerCmService.GetActiveConsumerCmList(ActiveConsumerCm_name);

            if (ActiveConsumerCms == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ActiveConsumerCm found for uci: {ActiveConsumerCm_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ActiveConsumerCms);
        }

        [HttpGet]
        public async Task<IActionResult> GetActiveConsumerCm(string ActiveConsumerCm_name)
        {
            var ActiveConsumerCms = await _ActiveConsumerCmService.GetActiveConsumerCm(ActiveConsumerCm_name);

            if (ActiveConsumerCms == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ActiveConsumerCm found for uci: {ActiveConsumerCm_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ActiveConsumerCms);
        }

        [HttpPost]
        public async Task<ActionResult<ActiveConsumerCm>> AddActiveConsumerCm(ActiveConsumerCm ActiveConsumerCm)
        {
            var dbActiveConsumerCm = await _ActiveConsumerCmService.AddActiveConsumerCm(ActiveConsumerCm);

            if (dbActiveConsumerCm == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ActiveConsumerCm.TbActiveConsumerCmName} could not be added."
                );
            }

            return CreatedAtAction("GetActiveConsumerCm", new { uci = ActiveConsumerCm.TbActiveConsumerCmName }, ActiveConsumerCm);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateActiveConsumerCm(ActiveConsumerCm ActiveConsumerCm)
        {           
            ActiveConsumerCm dbActiveConsumerCm = await _ActiveConsumerCmService.UpdateActiveConsumerCm(ActiveConsumerCm);

            if (dbActiveConsumerCm == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ActiveConsumerCm.TbActiveConsumerCmName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteActiveConsumerCm(ActiveConsumerCm ActiveConsumerCm)
        {            
            (bool status, string message) = await _ActiveConsumerCmService.DeleteActiveConsumerCm(ActiveConsumerCm);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ActiveConsumerCm);
        }
    }
}
