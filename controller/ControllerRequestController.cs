using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ControllerRequestController : ControllerBase
    {
        private readonly IControllerRequestService _ControllerRequestService;

        public ControllerRequestController(IControllerRequestService ControllerRequestService)
        {
            _ControllerRequestService = ControllerRequestService;
        }

        [HttpGet]
        public async Task<IActionResult> GetControllerRequestList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ControllerRequests = await _ControllerRequestService.GetControllerRequestListByValue(offset, limit, val);

            if (ControllerRequests == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ControllerRequests in database");
            }

            return StatusCode(StatusCodes.Status200OK, ControllerRequests);
        }

        [HttpGet]
        public async Task<IActionResult> GetControllerRequestList(string ControllerRequest_name)
        {
            var ControllerRequests = await _ControllerRequestService.GetControllerRequestList(ControllerRequest_name);

            if (ControllerRequests == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ControllerRequest found for uci: {ControllerRequest_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ControllerRequests);
        }

        [HttpGet]
        public async Task<IActionResult> GetControllerRequest(string ControllerRequest_name)
        {
            var ControllerRequests = await _ControllerRequestService.GetControllerRequest(ControllerRequest_name);

            if (ControllerRequests == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ControllerRequest found for uci: {ControllerRequest_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ControllerRequests);
        }

        [HttpPost]
        public async Task<ActionResult<ControllerRequest>> AddControllerRequest(ControllerRequest ControllerRequest)
        {
            var dbControllerRequest = await _ControllerRequestService.AddControllerRequest(ControllerRequest);

            if (dbControllerRequest == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ControllerRequest.TbControllerRequestName} could not be added."
                );
            }

            return CreatedAtAction("GetControllerRequest", new { uci = ControllerRequest.TbControllerRequestName }, ControllerRequest);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateControllerRequest(ControllerRequest ControllerRequest)
        {           
            ControllerRequest dbControllerRequest = await _ControllerRequestService.UpdateControllerRequest(ControllerRequest);

            if (dbControllerRequest == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ControllerRequest.TbControllerRequestName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteControllerRequest(ControllerRequest ControllerRequest)
        {            
            (bool status, string message) = await _ControllerRequestService.DeleteControllerRequest(ControllerRequest);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ControllerRequest);
        }
    }
}
