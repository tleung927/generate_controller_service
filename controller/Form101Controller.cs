using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Form101Controller : ControllerBase
    {
        private readonly IForm101Service _Form101Service;

        public Form101Controller(IForm101Service Form101Service)
        {
            _Form101Service = Form101Service;
        }

        [HttpGet]
        public async Task<IActionResult> GetForm101List(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Form101s = await _Form101Service.GetForm101ListByValue(offset, limit, val);

            if (Form101s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Form101s in database");
            }

            return StatusCode(StatusCodes.Status200OK, Form101s);
        }

        [HttpGet]
        public async Task<IActionResult> GetForm101List(string Form101_name)
        {
            var Form101s = await _Form101Service.GetForm101List(Form101_name);

            if (Form101s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Form101 found for uci: {Form101_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Form101s);
        }

        [HttpGet]
        public async Task<IActionResult> GetForm101(string Form101_name)
        {
            var Form101s = await _Form101Service.GetForm101(Form101_name);

            if (Form101s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Form101 found for uci: {Form101_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Form101s);
        }

        [HttpPost]
        public async Task<ActionResult<Form101>> AddForm101(Form101 Form101)
        {
            var dbForm101 = await _Form101Service.AddForm101(Form101);

            if (dbForm101 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Form101.TbForm101Name} could not be added."
                );
            }

            return CreatedAtAction("GetForm101", new { uci = Form101.TbForm101Name }, Form101);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateForm101(Form101 Form101)
        {           
            Form101 dbForm101 = await _Form101Service.UpdateForm101(Form101);

            if (dbForm101 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Form101.TbForm101Name} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteForm101(Form101 Form101)
        {            
            (bool status, string message) = await _Form101Service.DeleteForm101(Form101);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Form101);
        }
    }
}
