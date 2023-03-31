using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Cder08LabelController : ControllerBase
    {
        private readonly ICder08LabelService _Cder08LabelService;

        public Cder08LabelController(ICder08LabelService Cder08LabelService)
        {
            _Cder08LabelService = Cder08LabelService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCder08LabelList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Cder08Labels = await _Cder08LabelService.GetCder08LabelListByValue(offset, limit, val);

            if (Cder08Labels == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Cder08Labels in database");
            }

            return StatusCode(StatusCodes.Status200OK, Cder08Labels);
        }

        [HttpGet]
        public async Task<IActionResult> GetCder08LabelList(string Cder08Label_name)
        {
            var Cder08Labels = await _Cder08LabelService.GetCder08LabelList(Cder08Label_name);

            if (Cder08Labels == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Cder08Label found for uci: {Cder08Label_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Cder08Labels);
        }

        [HttpGet]
        public async Task<IActionResult> GetCder08Label(string Cder08Label_name)
        {
            var Cder08Labels = await _Cder08LabelService.GetCder08Label(Cder08Label_name);

            if (Cder08Labels == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Cder08Label found for uci: {Cder08Label_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Cder08Labels);
        }

        [HttpPost]
        public async Task<ActionResult<Cder08Label>> AddCder08Label(Cder08Label Cder08Label)
        {
            var dbCder08Label = await _Cder08LabelService.AddCder08Label(Cder08Label);

            if (dbCder08Label == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Cder08Label.TbCder08LabelName} could not be added."
                );
            }

            return CreatedAtAction("GetCder08Label", new { uci = Cder08Label.TbCder08LabelName }, Cder08Label);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCder08Label(Cder08Label Cder08Label)
        {           
            Cder08Label dbCder08Label = await _Cder08LabelService.UpdateCder08Label(Cder08Label);

            if (dbCder08Label == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Cder08Label.TbCder08LabelName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCder08Label(Cder08Label Cder08Label)
        {            
            (bool status, string message) = await _Cder08LabelService.DeleteCder08Label(Cder08Label);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Cder08Label);
        }
    }
}
