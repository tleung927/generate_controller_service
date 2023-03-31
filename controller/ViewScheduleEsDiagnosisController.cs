using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewScheduleEsDiagnosisController : ControllerBase
    {
        private readonly IViewScheduleEsDiagnosisService _ViewScheduleEsDiagnosisService;

        public ViewScheduleEsDiagnosisController(IViewScheduleEsDiagnosisService ViewScheduleEsDiagnosisService)
        {
            _ViewScheduleEsDiagnosisService = ViewScheduleEsDiagnosisService;
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleEsDiagnosisList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ViewScheduleEsDiagnosiss = await _ViewScheduleEsDiagnosisService.GetViewScheduleEsDiagnosisListByValue(offset, limit, val);

            if (ViewScheduleEsDiagnosiss == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ViewScheduleEsDiagnosiss in database");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleEsDiagnosiss);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleEsDiagnosisList(string ViewScheduleEsDiagnosis_name)
        {
            var ViewScheduleEsDiagnosiss = await _ViewScheduleEsDiagnosisService.GetViewScheduleEsDiagnosisList(ViewScheduleEsDiagnosis_name);

            if (ViewScheduleEsDiagnosiss == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewScheduleEsDiagnosis found for uci: {ViewScheduleEsDiagnosis_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleEsDiagnosiss);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleEsDiagnosis(string ViewScheduleEsDiagnosis_name)
        {
            var ViewScheduleEsDiagnosiss = await _ViewScheduleEsDiagnosisService.GetViewScheduleEsDiagnosis(ViewScheduleEsDiagnosis_name);

            if (ViewScheduleEsDiagnosiss == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewScheduleEsDiagnosis found for uci: {ViewScheduleEsDiagnosis_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleEsDiagnosiss);
        }

        [HttpPost]
        public async Task<ActionResult<ViewScheduleEsDiagnosis>> AddViewScheduleEsDiagnosis(ViewScheduleEsDiagnosis ViewScheduleEsDiagnosis)
        {
            var dbViewScheduleEsDiagnosis = await _ViewScheduleEsDiagnosisService.AddViewScheduleEsDiagnosis(ViewScheduleEsDiagnosis);

            if (dbViewScheduleEsDiagnosis == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewScheduleEsDiagnosis.TbViewScheduleEsDiagnosisName} could not be added."
                );
            }

            return CreatedAtAction("GetViewScheduleEsDiagnosis", new { uci = ViewScheduleEsDiagnosis.TbViewScheduleEsDiagnosisName }, ViewScheduleEsDiagnosis);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViewScheduleEsDiagnosis(ViewScheduleEsDiagnosis ViewScheduleEsDiagnosis)
        {           
            ViewScheduleEsDiagnosis dbViewScheduleEsDiagnosis = await _ViewScheduleEsDiagnosisService.UpdateViewScheduleEsDiagnosis(ViewScheduleEsDiagnosis);

            if (dbViewScheduleEsDiagnosis == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewScheduleEsDiagnosis.TbViewScheduleEsDiagnosisName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteViewScheduleEsDiagnosis(ViewScheduleEsDiagnosis ViewScheduleEsDiagnosis)
        {            
            (bool status, string message) = await _ViewScheduleEsDiagnosisService.DeleteViewScheduleEsDiagnosis(ViewScheduleEsDiagnosis);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleEsDiagnosis);
        }
    }
}
