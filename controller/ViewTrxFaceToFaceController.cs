using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewTrxFaceToFaceController : ControllerBase
    {
        private readonly IViewTrxFaceToFaceService _ViewTrxFaceToFaceService;

        public ViewTrxFaceToFaceController(IViewTrxFaceToFaceService ViewTrxFaceToFaceService)
        {
            _ViewTrxFaceToFaceService = ViewTrxFaceToFaceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetViewTrxFaceToFaceList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ViewTrxFaceToFaces = await _ViewTrxFaceToFaceService.GetViewTrxFaceToFaceListByValue(offset, limit, val);

            if (ViewTrxFaceToFaces == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ViewTrxFaceToFaces in database");
            }

            return StatusCode(StatusCodes.Status200OK, ViewTrxFaceToFaces);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewTrxFaceToFaceList(string ViewTrxFaceToFace_name)
        {
            var ViewTrxFaceToFaces = await _ViewTrxFaceToFaceService.GetViewTrxFaceToFaceList(ViewTrxFaceToFace_name);

            if (ViewTrxFaceToFaces == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewTrxFaceToFace found for uci: {ViewTrxFaceToFace_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewTrxFaceToFaces);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewTrxFaceToFace(string ViewTrxFaceToFace_name)
        {
            var ViewTrxFaceToFaces = await _ViewTrxFaceToFaceService.GetViewTrxFaceToFace(ViewTrxFaceToFace_name);

            if (ViewTrxFaceToFaces == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewTrxFaceToFace found for uci: {ViewTrxFaceToFace_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewTrxFaceToFaces);
        }

        [HttpPost]
        public async Task<ActionResult<ViewTrxFaceToFace>> AddViewTrxFaceToFace(ViewTrxFaceToFace ViewTrxFaceToFace)
        {
            var dbViewTrxFaceToFace = await _ViewTrxFaceToFaceService.AddViewTrxFaceToFace(ViewTrxFaceToFace);

            if (dbViewTrxFaceToFace == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewTrxFaceToFace.TbViewTrxFaceToFaceName} could not be added."
                );
            }

            return CreatedAtAction("GetViewTrxFaceToFace", new { uci = ViewTrxFaceToFace.TbViewTrxFaceToFaceName }, ViewTrxFaceToFace);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViewTrxFaceToFace(ViewTrxFaceToFace ViewTrxFaceToFace)
        {           
            ViewTrxFaceToFace dbViewTrxFaceToFace = await _ViewTrxFaceToFaceService.UpdateViewTrxFaceToFace(ViewTrxFaceToFace);

            if (dbViewTrxFaceToFace == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewTrxFaceToFace.TbViewTrxFaceToFaceName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteViewTrxFaceToFace(ViewTrxFaceToFace ViewTrxFaceToFace)
        {            
            (bool status, string message) = await _ViewTrxFaceToFaceService.DeleteViewTrxFaceToFace(ViewTrxFaceToFace);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ViewTrxFaceToFace);
        }
    }
}
