using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Form101McController : ControllerBase
    {
        private readonly IForm101McService _Form101McService;

        public Form101McController(IForm101McService Form101McService)
        {
            _Form101McService = Form101McService;
        }

        [HttpGet]
        public async Task<IActionResult> GetForm101McList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Form101Mcs = await _Form101McService.GetForm101McListByValue(offset, limit, val);

            if (Form101Mcs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Form101Mcs in database");
            }

            return StatusCode(StatusCodes.Status200OK, Form101Mcs);
        }

        [HttpGet]
        public async Task<IActionResult> GetForm101McList(string Form101Mc_name)
        {
            var Form101Mcs = await _Form101McService.GetForm101McList(Form101Mc_name);

            if (Form101Mcs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Form101Mc found for uci: {Form101Mc_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Form101Mcs);
        }

        [HttpGet]
        public async Task<IActionResult> GetForm101Mc(string Form101Mc_name)
        {
            var Form101Mcs = await _Form101McService.GetForm101Mc(Form101Mc_name);

            if (Form101Mcs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Form101Mc found for uci: {Form101Mc_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Form101Mcs);
        }

        [HttpPost]
        public async Task<ActionResult<Form101Mc>> AddForm101Mc(Form101Mc Form101Mc)
        {
            var dbForm101Mc = await _Form101McService.AddForm101Mc(Form101Mc);

            if (dbForm101Mc == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Form101Mc.TbForm101McName} could not be added."
                );
            }

            return CreatedAtAction("GetForm101Mc", new { uci = Form101Mc.TbForm101McName }, Form101Mc);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateForm101Mc(Form101Mc Form101Mc)
        {           
            Form101Mc dbForm101Mc = await _Form101McService.UpdateForm101Mc(Form101Mc);

            if (dbForm101Mc == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Form101Mc.TbForm101McName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteForm101Mc(Form101Mc Form101Mc)
        {            
            (bool status, string message) = await _Form101McService.DeleteForm101Mc(Form101Mc);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Form101Mc);
        }
    }
}
