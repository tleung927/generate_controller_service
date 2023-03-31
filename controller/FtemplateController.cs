using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FtemplateController : ControllerBase
    {
        private readonly IFtemplateService _FtemplateService;

        public FtemplateController(IFtemplateService FtemplateService)
        {
            _FtemplateService = FtemplateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFtemplateList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Ftemplates = await _FtemplateService.GetFtemplateListByValue(offset, limit, val);

            if (Ftemplates == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Ftemplates in database");
            }

            return StatusCode(StatusCodes.Status200OK, Ftemplates);
        }

        [HttpGet]
        public async Task<IActionResult> GetFtemplateList(string Ftemplate_name)
        {
            var Ftemplates = await _FtemplateService.GetFtemplateList(Ftemplate_name);

            if (Ftemplates == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Ftemplate found for uci: {Ftemplate_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Ftemplates);
        }

        [HttpGet]
        public async Task<IActionResult> GetFtemplate(string Ftemplate_name)
        {
            var Ftemplates = await _FtemplateService.GetFtemplate(Ftemplate_name);

            if (Ftemplates == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Ftemplate found for uci: {Ftemplate_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Ftemplates);
        }

        [HttpPost]
        public async Task<ActionResult<Ftemplate>> AddFtemplate(Ftemplate Ftemplate)
        {
            var dbFtemplate = await _FtemplateService.AddFtemplate(Ftemplate);

            if (dbFtemplate == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Ftemplate.TbFtemplateName} could not be added."
                );
            }

            return CreatedAtAction("GetFtemplate", new { uci = Ftemplate.TbFtemplateName }, Ftemplate);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFtemplate(Ftemplate Ftemplate)
        {           
            Ftemplate dbFtemplate = await _FtemplateService.UpdateFtemplate(Ftemplate);

            if (dbFtemplate == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Ftemplate.TbFtemplateName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFtemplate(Ftemplate Ftemplate)
        {            
            (bool status, string message) = await _FtemplateService.DeleteFtemplate(Ftemplate);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Ftemplate);
        }
    }
}
