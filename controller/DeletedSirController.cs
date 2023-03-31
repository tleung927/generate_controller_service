using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeletedSirController : ControllerBase
    {
        private readonly IDeletedSirService _DeletedSirService;

        public DeletedSirController(IDeletedSirService DeletedSirService)
        {
            _DeletedSirService = DeletedSirService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDeletedSirList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var DeletedSirs = await _DeletedSirService.GetDeletedSirListByValue(offset, limit, val);

            if (DeletedSirs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No DeletedSirs in database");
            }

            return StatusCode(StatusCodes.Status200OK, DeletedSirs);
        }

        [HttpGet]
        public async Task<IActionResult> GetDeletedSirList(string DeletedSir_name)
        {
            var DeletedSirs = await _DeletedSirService.GetDeletedSirList(DeletedSir_name);

            if (DeletedSirs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No DeletedSir found for uci: {DeletedSir_name}");
            }

            return StatusCode(StatusCodes.Status200OK, DeletedSirs);
        }

        [HttpGet]
        public async Task<IActionResult> GetDeletedSir(string DeletedSir_name)
        {
            var DeletedSirs = await _DeletedSirService.GetDeletedSir(DeletedSir_name);

            if (DeletedSirs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No DeletedSir found for uci: {DeletedSir_name}");
            }

            return StatusCode(StatusCodes.Status200OK, DeletedSirs);
        }

        [HttpPost]
        public async Task<ActionResult<DeletedSir>> AddDeletedSir(DeletedSir DeletedSir)
        {
            var dbDeletedSir = await _DeletedSirService.AddDeletedSir(DeletedSir);

            if (dbDeletedSir == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{DeletedSir.TbDeletedSirName} could not be added."
                );
            }

            return CreatedAtAction("GetDeletedSir", new { uci = DeletedSir.TbDeletedSirName }, DeletedSir);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDeletedSir(DeletedSir DeletedSir)
        {           
            DeletedSir dbDeletedSir = await _DeletedSirService.UpdateDeletedSir(DeletedSir);

            if (dbDeletedSir == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{DeletedSir.TbDeletedSirName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDeletedSir(DeletedSir DeletedSir)
        {            
            (bool status, string message) = await _DeletedSirService.DeleteDeletedSir(DeletedSir);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, DeletedSir);
        }
    }
}
