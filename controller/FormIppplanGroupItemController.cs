using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormIppplanGroupItemController : ControllerBase
    {
        private readonly IFormIppplanGroupItemService _FormIppplanGroupItemService;

        public FormIppplanGroupItemController(IFormIppplanGroupItemService FormIppplanGroupItemService)
        {
            _FormIppplanGroupItemService = FormIppplanGroupItemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIppplanGroupItemList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormIppplanGroupItems = await _FormIppplanGroupItemService.GetFormIppplanGroupItemListByValue(offset, limit, val);

            if (FormIppplanGroupItems == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormIppplanGroupItems in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormIppplanGroupItems);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIppplanGroupItemList(string FormIppplanGroupItem_name)
        {
            var FormIppplanGroupItems = await _FormIppplanGroupItemService.GetFormIppplanGroupItemList(FormIppplanGroupItem_name);

            if (FormIppplanGroupItems == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormIppplanGroupItem found for uci: {FormIppplanGroupItem_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormIppplanGroupItems);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIppplanGroupItem(string FormIppplanGroupItem_name)
        {
            var FormIppplanGroupItems = await _FormIppplanGroupItemService.GetFormIppplanGroupItem(FormIppplanGroupItem_name);

            if (FormIppplanGroupItems == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormIppplanGroupItem found for uci: {FormIppplanGroupItem_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormIppplanGroupItems);
        }

        [HttpPost]
        public async Task<ActionResult<FormIppplanGroupItem>> AddFormIppplanGroupItem(FormIppplanGroupItem FormIppplanGroupItem)
        {
            var dbFormIppplanGroupItem = await _FormIppplanGroupItemService.AddFormIppplanGroupItem(FormIppplanGroupItem);

            if (dbFormIppplanGroupItem == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormIppplanGroupItem.TbFormIppplanGroupItemName} could not be added."
                );
            }

            return CreatedAtAction("GetFormIppplanGroupItem", new { uci = FormIppplanGroupItem.TbFormIppplanGroupItemName }, FormIppplanGroupItem);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormIppplanGroupItem(FormIppplanGroupItem FormIppplanGroupItem)
        {           
            FormIppplanGroupItem dbFormIppplanGroupItem = await _FormIppplanGroupItemService.UpdateFormIppplanGroupItem(FormIppplanGroupItem);

            if (dbFormIppplanGroupItem == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormIppplanGroupItem.TbFormIppplanGroupItemName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormIppplanGroupItem(FormIppplanGroupItem FormIppplanGroupItem)
        {            
            (bool status, string message) = await _FormIppplanGroupItemService.DeleteFormIppplanGroupItem(FormIppplanGroupItem);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormIppplanGroupItem);
        }
    }
}
