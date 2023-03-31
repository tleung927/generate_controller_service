using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FinancialController : ControllerBase
    {
        private readonly IFinancialService _FinancialService;

        public FinancialController(IFinancialService FinancialService)
        {
            _FinancialService = FinancialService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFinancialList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Financials = await _FinancialService.GetFinancialListByValue(offset, limit, val);

            if (Financials == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Financials in database");
            }

            return StatusCode(StatusCodes.Status200OK, Financials);
        }

        [HttpGet]
        public async Task<IActionResult> GetFinancialList(string Financial_name)
        {
            var Financials = await _FinancialService.GetFinancialList(Financial_name);

            if (Financials == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Financial found for uci: {Financial_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Financials);
        }

        [HttpGet]
        public async Task<IActionResult> GetFinancial(string Financial_name)
        {
            var Financials = await _FinancialService.GetFinancial(Financial_name);

            if (Financials == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Financial found for uci: {Financial_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Financials);
        }

        [HttpPost]
        public async Task<ActionResult<Financial>> AddFinancial(Financial Financial)
        {
            var dbFinancial = await _FinancialService.AddFinancial(Financial);

            if (dbFinancial == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Financial.TbFinancialName} could not be added."
                );
            }

            return CreatedAtAction("GetFinancial", new { uci = Financial.TbFinancialName }, Financial);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFinancial(Financial Financial)
        {           
            Financial dbFinancial = await _FinancialService.UpdateFinancial(Financial);

            if (dbFinancial == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Financial.TbFinancialName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFinancial(Financial Financial)
        {            
            (bool status, string message) = await _FinancialService.DeleteFinancial(Financial);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Financial);
        }
    }
}
