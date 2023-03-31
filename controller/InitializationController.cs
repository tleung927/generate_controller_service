using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InitializationController : ControllerBase
    {
        private readonly IInitializationService _InitializationService;

        public InitializationController(IInitializationService InitializationService)
        {
            _InitializationService = InitializationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetInitializationList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Initializations = await _InitializationService.GetInitializationListByValue(offset, limit, val);

            if (Initializations == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Initializations in database");
            }

            return StatusCode(StatusCodes.Status200OK, Initializations);
        }

        [HttpGet]
        public async Task<IActionResult> GetInitializationList(string Initialization_name)
        {
            var Initializations = await _InitializationService.GetInitializationList(Initialization_name);

            if (Initializations == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Initialization found for uci: {Initialization_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Initializations);
        }

        [HttpGet]
        public async Task<IActionResult> GetInitialization(string Initialization_name)
        {
            var Initializations = await _InitializationService.GetInitialization(Initialization_name);

            if (Initializations == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Initialization found for uci: {Initialization_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Initializations);
        }

        [HttpPost]
        public async Task<ActionResult<Initialization>> AddInitialization(Initialization Initialization)
        {
            var dbInitialization = await _InitializationService.AddInitialization(Initialization);

            if (dbInitialization == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Initialization.TbInitializationName} could not be added."
                );
            }

            return CreatedAtAction("GetInitialization", new { uci = Initialization.TbInitializationName }, Initialization);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateInitialization(Initialization Initialization)
        {           
            Initialization dbInitialization = await _InitializationService.UpdateInitialization(Initialization);

            if (dbInitialization == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Initialization.TbInitializationName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteInitialization(Initialization Initialization)
        {            
            (bool status, string message) = await _InitializationService.DeleteInitialization(Initialization);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Initialization);
        }
    }
}
