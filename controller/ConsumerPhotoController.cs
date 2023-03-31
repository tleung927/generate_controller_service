using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsumerPhotoController : ControllerBase
    {
        private readonly IConsumerPhotoService _ConsumerPhotoService;

        public ConsumerPhotoController(IConsumerPhotoService ConsumerPhotoService)
        {
            _ConsumerPhotoService = ConsumerPhotoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerPhotoList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ConsumerPhotos = await _ConsumerPhotoService.GetConsumerPhotoListByValue(offset, limit, val);

            if (ConsumerPhotos == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ConsumerPhotos in database");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerPhotos);
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerPhotoList(string ConsumerPhoto_name)
        {
            var ConsumerPhotos = await _ConsumerPhotoService.GetConsumerPhotoList(ConsumerPhoto_name);

            if (ConsumerPhotos == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ConsumerPhoto found for uci: {ConsumerPhoto_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerPhotos);
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerPhoto(string ConsumerPhoto_name)
        {
            var ConsumerPhotos = await _ConsumerPhotoService.GetConsumerPhoto(ConsumerPhoto_name);

            if (ConsumerPhotos == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ConsumerPhoto found for uci: {ConsumerPhoto_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerPhotos);
        }

        [HttpPost]
        public async Task<ActionResult<ConsumerPhoto>> AddConsumerPhoto(ConsumerPhoto ConsumerPhoto)
        {
            var dbConsumerPhoto = await _ConsumerPhotoService.AddConsumerPhoto(ConsumerPhoto);

            if (dbConsumerPhoto == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ConsumerPhoto.TbConsumerPhotoName} could not be added."
                );
            }

            return CreatedAtAction("GetConsumerPhoto", new { uci = ConsumerPhoto.TbConsumerPhotoName }, ConsumerPhoto);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateConsumerPhoto(ConsumerPhoto ConsumerPhoto)
        {           
            ConsumerPhoto dbConsumerPhoto = await _ConsumerPhotoService.UpdateConsumerPhoto(ConsumerPhoto);

            if (dbConsumerPhoto == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ConsumerPhoto.TbConsumerPhotoName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteConsumerPhoto(ConsumerPhoto ConsumerPhoto)
        {            
            (bool status, string message) = await _ConsumerPhotoService.DeleteConsumerPhoto(ConsumerPhoto);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerPhoto);
        }
    }
}
