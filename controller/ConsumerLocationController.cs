using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsumerLocationController : ControllerBase
    {
        private readonly IConsumerLocationService _ConsumerLocationService;

        public ConsumerLocationController(IConsumerLocationService ConsumerLocationService)
        {
            _ConsumerLocationService = ConsumerLocationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerLocationList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ConsumerLocations = await _ConsumerLocationService.GetConsumerLocationListByValue(offset, limit, val);

            if (ConsumerLocations == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ConsumerLocations in database");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerLocations);
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerLocationList(string ConsumerLocation_name)
        {
            var ConsumerLocations = await _ConsumerLocationService.GetConsumerLocationList(ConsumerLocation_name);

            if (ConsumerLocations == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ConsumerLocation found for uci: {ConsumerLocation_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerLocations);
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerLocation(string ConsumerLocation_name)
        {
            var ConsumerLocations = await _ConsumerLocationService.GetConsumerLocation(ConsumerLocation_name);

            if (ConsumerLocations == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ConsumerLocation found for uci: {ConsumerLocation_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerLocations);
        }

        [HttpPost]
        public async Task<ActionResult<ConsumerLocation>> AddConsumerLocation(ConsumerLocation ConsumerLocation)
        {
            var dbConsumerLocation = await _ConsumerLocationService.AddConsumerLocation(ConsumerLocation);

            if (dbConsumerLocation == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ConsumerLocation.TbConsumerLocationName} could not be added."
                );
            }

            return CreatedAtAction("GetConsumerLocation", new { uci = ConsumerLocation.TbConsumerLocationName }, ConsumerLocation);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateConsumerLocation(ConsumerLocation ConsumerLocation)
        {           
            ConsumerLocation dbConsumerLocation = await _ConsumerLocationService.UpdateConsumerLocation(ConsumerLocation);

            if (dbConsumerLocation == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ConsumerLocation.TbConsumerLocationName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteConsumerLocation(ConsumerLocation ConsumerLocation)
        {            
            (bool status, string message) = await _ConsumerLocationService.DeleteConsumerLocation(ConsumerLocation);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerLocation);
        }
    }
}
