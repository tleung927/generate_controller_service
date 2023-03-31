using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Forms2Controller : ControllerBase
    {
        private readonly IForms2Service _Forms2Service;

        public Forms2Controller(IForms2Service Forms2Service)
        {
            _Forms2Service = Forms2Service;
        }

        [HttpGet]
        public async Task<IActionResult> GetForms2List(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Forms2s = await _Forms2Service.GetForms2ListByValue(offset, limit, val);

            if (Forms2s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Forms2s in database");
            }

            return StatusCode(StatusCodes.Status200OK, Forms2s);
        }

        [HttpGet]
        public async Task<IActionResult> GetForms2List(string Forms2_name)
        {
            var Forms2s = await _Forms2Service.GetForms2List(Forms2_name);

            if (Forms2s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Forms2 found for uci: {Forms2_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Forms2s);
        }

        [HttpGet]
        public async Task<IActionResult> GetForms2(string Forms2_name)
        {
            var Forms2s = await _Forms2Service.GetForms2(Forms2_name);

            if (Forms2s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Forms2 found for uci: {Forms2_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Forms2s);
        }

        [HttpPost]
        public async Task<ActionResult<Forms2>> AddForms2(Forms2 Forms2)
        {
            var dbForms2 = await _Forms2Service.AddForms2(Forms2);

            if (dbForms2 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Forms2.TbForms2Name} could not be added."
                );
            }

            return CreatedAtAction("GetForms2", new { uci = Forms2.TbForms2Name }, Forms2);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateForms2(Forms2 Forms2)
        {           
            Forms2 dbForms2 = await _Forms2Service.UpdateForms2(Forms2);

            if (dbForms2 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Forms2.TbForms2Name} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteForms2(Forms2 Forms2)
        {            
            (bool status, string message) = await _Forms2Service.DeleteForms2(Forms2);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Forms2);
        }
    }
}
