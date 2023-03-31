using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RtemplateController : ControllerBase
    {
        private readonly IRtemplateService _RtemplateService;

        public RtemplateController(IRtemplateService RtemplateService)
        {
            _RtemplateService = RtemplateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRtemplateList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Rtemplates = await _RtemplateService.GetRtemplateListByValue(offset, limit, val);

            if (Rtemplates == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Rtemplates in database");
            }

            return StatusCode(StatusCodes.Status200OK, Rtemplates);
        }

        [HttpGet]
        public async Task<IActionResult> GetRtemplateList(string Rtemplate_name)
        {
            var Rtemplates = await _RtemplateService.GetRtemplateList(Rtemplate_name);

            if (Rtemplates == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Rtemplate found for uci: {Rtemplate_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Rtemplates);
        }

        [HttpGet]
        public async Task<IActionResult> GetRtemplate(string Rtemplate_name)
        {
            var Rtemplates = await _RtemplateService.GetRtemplate(Rtemplate_name);

            if (Rtemplates == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Rtemplate found for uci: {Rtemplate_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Rtemplates);
        }

        [HttpPost]
        public async Task<ActionResult<Rtemplate>> AddRtemplate(Rtemplate Rtemplate)
        {
            var dbRtemplate = await _RtemplateService.AddRtemplate(Rtemplate);

            if (dbRtemplate == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Rtemplate.TbRtemplateName} could not be added."
                );
            }

            return CreatedAtAction("GetRtemplate", new { uci = Rtemplate.TbRtemplateName }, Rtemplate);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRtemplate(Rtemplate Rtemplate)
        {           
            Rtemplate dbRtemplate = await _RtemplateService.UpdateRtemplate(Rtemplate);

            if (dbRtemplate == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Rtemplate.TbRtemplateName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRtemplate(Rtemplate Rtemplate)
        {            
            (bool status, string message) = await _RtemplateService.DeleteRtemplate(Rtemplate);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Rtemplate);
        }
    }
}
