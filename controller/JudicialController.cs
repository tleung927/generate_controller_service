using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JudicialController : ControllerBase
    {
        private readonly IJudicialService _JudicialService;

        public JudicialController(IJudicialService JudicialService)
        {
            _JudicialService = JudicialService;
        }

        [HttpGet]
        public async Task<IActionResult> GetJudicialList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Judicials = await _JudicialService.GetJudicialListByValue(offset, limit, val);

            if (Judicials == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Judicials in database");
            }

            return StatusCode(StatusCodes.Status200OK, Judicials);
        }

        [HttpGet]
        public async Task<IActionResult> GetJudicialList(string Judicial_name)
        {
            var Judicials = await _JudicialService.GetJudicialList(Judicial_name);

            if (Judicials == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Judicial found for uci: {Judicial_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Judicials);
        }

        [HttpGet]
        public async Task<IActionResult> GetJudicial(string Judicial_name)
        {
            var Judicials = await _JudicialService.GetJudicial(Judicial_name);

            if (Judicials == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Judicial found for uci: {Judicial_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Judicials);
        }

        [HttpPost]
        public async Task<ActionResult<Judicial>> AddJudicial(Judicial Judicial)
        {
            var dbJudicial = await _JudicialService.AddJudicial(Judicial);

            if (dbJudicial == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Judicial.TbJudicialName} could not be added."
                );
            }

            return CreatedAtAction("GetJudicial", new { uci = Judicial.TbJudicialName }, Judicial);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateJudicial(Judicial Judicial)
        {           
            Judicial dbJudicial = await _JudicialService.UpdateJudicial(Judicial);

            if (dbJudicial == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Judicial.TbJudicialName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteJudicial(Judicial Judicial)
        {            
            (bool status, string message) = await _JudicialService.DeleteJudicial(Judicial);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Judicial);
        }
    }
}
