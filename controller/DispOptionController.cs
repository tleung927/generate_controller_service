using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DispOptionController : ControllerBase
    {
        private readonly IDispOptionService _DispOptionService;

        public DispOptionController(IDispOptionService DispOptionService)
        {
            _DispOptionService = DispOptionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDispOptionList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var DispOptions = await _DispOptionService.GetDispOptionListByValue(offset, limit, val);

            if (DispOptions == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No DispOptions in database");
            }

            return StatusCode(StatusCodes.Status200OK, DispOptions);
        }

        [HttpGet]
        public async Task<IActionResult> GetDispOptionList(string DispOption_name)
        {
            var DispOptions = await _DispOptionService.GetDispOptionList(DispOption_name);

            if (DispOptions == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No DispOption found for uci: {DispOption_name}");
            }

            return StatusCode(StatusCodes.Status200OK, DispOptions);
        }

        [HttpGet]
        public async Task<IActionResult> GetDispOption(string DispOption_name)
        {
            var DispOptions = await _DispOptionService.GetDispOption(DispOption_name);

            if (DispOptions == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No DispOption found for uci: {DispOption_name}");
            }

            return StatusCode(StatusCodes.Status200OK, DispOptions);
        }

        [HttpPost]
        public async Task<ActionResult<DispOption>> AddDispOption(DispOption DispOption)
        {
            var dbDispOption = await _DispOptionService.AddDispOption(DispOption);

            if (dbDispOption == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{DispOption.TbDispOptionName} could not be added."
                );
            }

            return CreatedAtAction("GetDispOption", new { uci = DispOption.TbDispOptionName }, DispOption);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDispOption(DispOption DispOption)
        {           
            DispOption dbDispOption = await _DispOptionService.UpdateDispOption(DispOption);

            if (dbDispOption == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{DispOption.TbDispOptionName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDispOption(DispOption DispOption)
        {            
            (bool status, string message) = await _DispOptionService.DeleteDispOption(DispOption);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, DispOption);
        }
    }
}
