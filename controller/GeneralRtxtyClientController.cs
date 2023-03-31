using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GeneralRtxtyClientController : ControllerBase
    {
        private readonly IGeneralRtxtyClientService _GeneralRtxtyClientService;

        public GeneralRtxtyClientController(IGeneralRtxtyClientService GeneralRtxtyClientService)
        {
            _GeneralRtxtyClientService = GeneralRtxtyClientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGeneralRtxtyClientList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var GeneralRtxtyClients = await _GeneralRtxtyClientService.GetGeneralRtxtyClientListByValue(offset, limit, val);

            if (GeneralRtxtyClients == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No GeneralRtxtyClients in database");
            }

            return StatusCode(StatusCodes.Status200OK, GeneralRtxtyClients);
        }

        [HttpGet]
        public async Task<IActionResult> GetGeneralRtxtyClientList(string GeneralRtxtyClient_name)
        {
            var GeneralRtxtyClients = await _GeneralRtxtyClientService.GetGeneralRtxtyClientList(GeneralRtxtyClient_name);

            if (GeneralRtxtyClients == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No GeneralRtxtyClient found for uci: {GeneralRtxtyClient_name}");
            }

            return StatusCode(StatusCodes.Status200OK, GeneralRtxtyClients);
        }

        [HttpGet]
        public async Task<IActionResult> GetGeneralRtxtyClient(string GeneralRtxtyClient_name)
        {
            var GeneralRtxtyClients = await _GeneralRtxtyClientService.GetGeneralRtxtyClient(GeneralRtxtyClient_name);

            if (GeneralRtxtyClients == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No GeneralRtxtyClient found for uci: {GeneralRtxtyClient_name}");
            }

            return StatusCode(StatusCodes.Status200OK, GeneralRtxtyClients);
        }

        [HttpPost]
        public async Task<ActionResult<GeneralRtxtyClient>> AddGeneralRtxtyClient(GeneralRtxtyClient GeneralRtxtyClient)
        {
            var dbGeneralRtxtyClient = await _GeneralRtxtyClientService.AddGeneralRtxtyClient(GeneralRtxtyClient);

            if (dbGeneralRtxtyClient == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{GeneralRtxtyClient.TbGeneralRtxtyClientName} could not be added."
                );
            }

            return CreatedAtAction("GetGeneralRtxtyClient", new { uci = GeneralRtxtyClient.TbGeneralRtxtyClientName }, GeneralRtxtyClient);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGeneralRtxtyClient(GeneralRtxtyClient GeneralRtxtyClient)
        {           
            GeneralRtxtyClient dbGeneralRtxtyClient = await _GeneralRtxtyClientService.UpdateGeneralRtxtyClient(GeneralRtxtyClient);

            if (dbGeneralRtxtyClient == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{GeneralRtxtyClient.TbGeneralRtxtyClientName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGeneralRtxtyClient(GeneralRtxtyClient GeneralRtxtyClient)
        {            
            (bool status, string message) = await _GeneralRtxtyClientService.DeleteGeneralRtxtyClient(GeneralRtxtyClient);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, GeneralRtxtyClient);
        }
    }
}
