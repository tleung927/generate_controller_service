using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _ImageService;

        public ImageController(IImageService ImageService)
        {
            _ImageService = ImageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetImageList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Images = await _ImageService.GetImageListByValue(offset, limit, val);

            if (Images == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Images in database");
            }

            return StatusCode(StatusCodes.Status200OK, Images);
        }

        [HttpGet]
        public async Task<IActionResult> GetImageList(string Image_name)
        {
            var Images = await _ImageService.GetImageList(Image_name);

            if (Images == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Image found for uci: {Image_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Images);
        }

        [HttpGet]
        public async Task<IActionResult> GetImage(string Image_name)
        {
            var Images = await _ImageService.GetImage(Image_name);

            if (Images == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Image found for uci: {Image_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Images);
        }

        [HttpPost]
        public async Task<ActionResult<Image>> AddImage(Image Image)
        {
            var dbImage = await _ImageService.AddImage(Image);

            if (dbImage == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Image.TbImageName} could not be added."
                );
            }

            return CreatedAtAction("GetImage", new { uci = Image.TbImageName }, Image);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateImage(Image Image)
        {           
            Image dbImage = await _ImageService.UpdateImage(Image);

            if (dbImage == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Image.TbImageName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteImage(Image Image)
        {            
            (bool status, string message) = await _ImageService.DeleteImage(Image);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Image);
        }
    }
}
