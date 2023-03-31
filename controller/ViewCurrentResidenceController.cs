using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewCurrentResidenceController : ControllerBase
    {
        private readonly IViewCurrentResidenceService _ViewCurrentResidenceService;

        public ViewCurrentResidenceController(IViewCurrentResidenceService ViewCurrentResidenceService)
        {
            _ViewCurrentResidenceService = ViewCurrentResidenceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetViewCurrentResidenceList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ViewCurrentResidences = await _ViewCurrentResidenceService.GetViewCurrentResidenceListByValue(offset, limit, val);

            if (ViewCurrentResidences == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ViewCurrentResidences in database");
            }

            return StatusCode(StatusCodes.Status200OK, ViewCurrentResidences);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewCurrentResidenceList(string ViewCurrentResidence_name)
        {
            var ViewCurrentResidences = await _ViewCurrentResidenceService.GetViewCurrentResidenceList(ViewCurrentResidence_name);

            if (ViewCurrentResidences == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewCurrentResidence found for uci: {ViewCurrentResidence_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewCurrentResidences);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewCurrentResidence(string ViewCurrentResidence_name)
        {
            var ViewCurrentResidences = await _ViewCurrentResidenceService.GetViewCurrentResidence(ViewCurrentResidence_name);

            if (ViewCurrentResidences == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewCurrentResidence found for uci: {ViewCurrentResidence_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewCurrentResidences);
        }

        [HttpPost]
        public async Task<ActionResult<ViewCurrentResidence>> AddViewCurrentResidence(ViewCurrentResidence ViewCurrentResidence)
        {
            var dbViewCurrentResidence = await _ViewCurrentResidenceService.AddViewCurrentResidence(ViewCurrentResidence);

            if (dbViewCurrentResidence == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewCurrentResidence.TbViewCurrentResidenceName} could not be added."
                );
            }

            return CreatedAtAction("GetViewCurrentResidence", new { uci = ViewCurrentResidence.TbViewCurrentResidenceName }, ViewCurrentResidence);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViewCurrentResidence(ViewCurrentResidence ViewCurrentResidence)
        {           
            ViewCurrentResidence dbViewCurrentResidence = await _ViewCurrentResidenceService.UpdateViewCurrentResidence(ViewCurrentResidence);

            if (dbViewCurrentResidence == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewCurrentResidence.TbViewCurrentResidenceName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteViewCurrentResidence(ViewCurrentResidence ViewCurrentResidence)
        {            
            (bool status, string message) = await _ViewCurrentResidenceService.DeleteViewCurrentResidence(ViewCurrentResidence);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ViewCurrentResidence);
        }
    }
}
