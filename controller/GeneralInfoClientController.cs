using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GeneralInfoClientController : ControllerBase
    {
        private readonly IGeneralInfoClientService _GeneralInfoClientService;

        public GeneralInfoClientController(IGeneralInfoClientService GeneralInfoClientService)
        {
            _GeneralInfoClientService = GeneralInfoClientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGeneralInfoClientList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var GeneralInfoClients = await _GeneralInfoClientService.GetGeneralInfoClientListByValue(offset, limit, val);

            if (GeneralInfoClients == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No GeneralInfoClients in database");
            }

            return StatusCode(StatusCodes.Status200OK, GeneralInfoClients);
        }

        [HttpGet]
        public async Task<IActionResult> GetGeneralInfoClientList(string GeneralInfoClient_name)
        {
            var GeneralInfoClients = await _GeneralInfoClientService.GetGeneralInfoClientList(GeneralInfoClient_name);

            if (GeneralInfoClients == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No GeneralInfoClient found for uci: {GeneralInfoClient_name}");
            }

            return StatusCode(StatusCodes.Status200OK, GeneralInfoClients);
        }

        [HttpGet]
        public async Task<IActionResult> GetGeneralInfoClient(string GeneralInfoClient_name)
        {
            var GeneralInfoClients = await _GeneralInfoClientService.GetGeneralInfoClient(GeneralInfoClient_name);

            if (GeneralInfoClients == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No GeneralInfoClient found for uci: {GeneralInfoClient_name}");
            }

            return StatusCode(StatusCodes.Status200OK, GeneralInfoClients);
        }

        [HttpPost]
        public async Task<ActionResult<GeneralInfoClient>> AddGeneralInfoClient(GeneralInfoClient GeneralInfoClient)
        {
            var dbGeneralInfoClient = await _GeneralInfoClientService.AddGeneralInfoClient(GeneralInfoClient);

            if (dbGeneralInfoClient == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{GeneralInfoClient.TbGeneralInfoClientName} could not be added."
                );
            }

            return CreatedAtAction("GetGeneralInfoClient", new { uci = GeneralInfoClient.TbGeneralInfoClientName }, GeneralInfoClient);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGeneralInfoClient(GeneralInfoClient GeneralInfoClient)
        {           
            GeneralInfoClient dbGeneralInfoClient = await _GeneralInfoClientService.UpdateGeneralInfoClient(GeneralInfoClient);

            if (dbGeneralInfoClient == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{GeneralInfoClient.TbGeneralInfoClientName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGeneralInfoClient(GeneralInfoClient GeneralInfoClient)
        {            
            (bool status, string message) = await _GeneralInfoClientService.DeleteGeneralInfoClient(GeneralInfoClient);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, GeneralInfoClient);
        }
    }
}
