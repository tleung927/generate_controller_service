using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DrugController : ControllerBase
    {
        private readonly IDrugService _DrugService;

        public DrugController(IDrugService DrugService)
        {
            _DrugService = DrugService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDrugList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Drugs = await _DrugService.GetDrugListByValue(offset, limit, val);

            if (Drugs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Drugs in database");
            }

            return StatusCode(StatusCodes.Status200OK, Drugs);
        }

        [HttpGet]
        public async Task<IActionResult> GetDrugList(string Drug_name)
        {
            var Drugs = await _DrugService.GetDrugList(Drug_name);

            if (Drugs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Drug found for uci: {Drug_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Drugs);
        }

        [HttpGet]
        public async Task<IActionResult> GetDrug(string Drug_name)
        {
            var Drugs = await _DrugService.GetDrug(Drug_name);

            if (Drugs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Drug found for uci: {Drug_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Drugs);
        }

        [HttpPost]
        public async Task<ActionResult<Drug>> AddDrug(Drug Drug)
        {
            var dbDrug = await _DrugService.AddDrug(Drug);

            if (dbDrug == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Drug.TbDrugName} could not be added."
                );
            }

            return CreatedAtAction("GetDrug", new { uci = Drug.TbDrugName }, Drug);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDrug(Drug Drug)
        {           
            Drug dbDrug = await _DrugService.UpdateDrug(Drug);

            if (dbDrug == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Drug.TbDrugName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDrug(Drug Drug)
        {            
            (bool status, string message) = await _DrugService.DeleteDrug(Drug);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Drug);
        }
    }
}
