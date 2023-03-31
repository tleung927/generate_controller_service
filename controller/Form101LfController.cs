using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Form101LfController : ControllerBase
    {
        private readonly IForm101LfService _Form101LfService;

        public Form101LfController(IForm101LfService Form101LfService)
        {
            _Form101LfService = Form101LfService;
        }

        [HttpGet]
        public async Task<IActionResult> GetForm101LfList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Form101Lfs = await _Form101LfService.GetForm101LfListByValue(offset, limit, val);

            if (Form101Lfs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Form101Lfs in database");
            }

            return StatusCode(StatusCodes.Status200OK, Form101Lfs);
        }

        [HttpGet]
        public async Task<IActionResult> GetForm101LfList(string Form101Lf_name)
        {
            var Form101Lfs = await _Form101LfService.GetForm101LfList(Form101Lf_name);

            if (Form101Lfs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Form101Lf found for uci: {Form101Lf_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Form101Lfs);
        }

        [HttpGet]
        public async Task<IActionResult> GetForm101Lf(string Form101Lf_name)
        {
            var Form101Lfs = await _Form101LfService.GetForm101Lf(Form101Lf_name);

            if (Form101Lfs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Form101Lf found for uci: {Form101Lf_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Form101Lfs);
        }

        [HttpPost]
        public async Task<ActionResult<Form101Lf>> AddForm101Lf(Form101Lf Form101Lf)
        {
            var dbForm101Lf = await _Form101LfService.AddForm101Lf(Form101Lf);

            if (dbForm101Lf == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Form101Lf.TbForm101LfName} could not be added."
                );
            }

            return CreatedAtAction("GetForm101Lf", new { uci = Form101Lf.TbForm101LfName }, Form101Lf);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateForm101Lf(Form101Lf Form101Lf)
        {           
            Form101Lf dbForm101Lf = await _Form101LfService.UpdateForm101Lf(Form101Lf);

            if (dbForm101Lf == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Form101Lf.TbForm101LfName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteForm101Lf(Form101Lf Form101Lf)
        {            
            (bool status, string message) = await _Form101LfService.DeleteForm101Lf(Form101Lf);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Form101Lf);
        }
    }
}
