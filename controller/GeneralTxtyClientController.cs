using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GeneralTxtyClientController : ControllerBase
    {
        private readonly IGeneralTxtyClientService _GeneralTxtyClientService;

        public GeneralTxtyClientController(IGeneralTxtyClientService GeneralTxtyClientService)
        {
            _GeneralTxtyClientService = GeneralTxtyClientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGeneralTxtyClientList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var GeneralTxtyClients = await _GeneralTxtyClientService.GetGeneralTxtyClientListByValue(offset, limit, val);

            if (GeneralTxtyClients == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No GeneralTxtyClients in database");
            }

            return StatusCode(StatusCodes.Status200OK, GeneralTxtyClients);
        }

        [HttpGet]
        public async Task<IActionResult> GetGeneralTxtyClientList(string GeneralTxtyClient_name)
        {
            var GeneralTxtyClients = await _GeneralTxtyClientService.GetGeneralTxtyClientList(GeneralTxtyClient_name);

            if (GeneralTxtyClients == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No GeneralTxtyClient found for uci: {GeneralTxtyClient_name}");
            }

            return StatusCode(StatusCodes.Status200OK, GeneralTxtyClients);
        }

        [HttpGet]
        public async Task<IActionResult> GetGeneralTxtyClient(string GeneralTxtyClient_name)
        {
            var GeneralTxtyClients = await _GeneralTxtyClientService.GetGeneralTxtyClient(GeneralTxtyClient_name);

            if (GeneralTxtyClients == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No GeneralTxtyClient found for uci: {GeneralTxtyClient_name}");
            }

            return StatusCode(StatusCodes.Status200OK, GeneralTxtyClients);
        }

        [HttpPost]
        public async Task<ActionResult<GeneralTxtyClient>> AddGeneralTxtyClient(GeneralTxtyClient GeneralTxtyClient)
        {
            var dbGeneralTxtyClient = await _GeneralTxtyClientService.AddGeneralTxtyClient(GeneralTxtyClient);

            if (dbGeneralTxtyClient == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{GeneralTxtyClient.TbGeneralTxtyClientName} could not be added."
                );
            }

            return CreatedAtAction("GetGeneralTxtyClient", new { uci = GeneralTxtyClient.TbGeneralTxtyClientName }, GeneralTxtyClient);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGeneralTxtyClient(GeneralTxtyClient GeneralTxtyClient)
        {           
            GeneralTxtyClient dbGeneralTxtyClient = await _GeneralTxtyClientService.UpdateGeneralTxtyClient(GeneralTxtyClient);

            if (dbGeneralTxtyClient == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{GeneralTxtyClient.TbGeneralTxtyClientName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGeneralTxtyClient(GeneralTxtyClient GeneralTxtyClient)
        {            
            (bool status, string message) = await _GeneralTxtyClientService.DeleteGeneralTxtyClient(GeneralTxtyClient);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, GeneralTxtyClient);
        }
    }
}
