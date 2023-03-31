using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormIppplanItemController : ControllerBase
    {
        private readonly IFormIppplanItemService _FormIppplanItemService;

        public FormIppplanItemController(IFormIppplanItemService FormIppplanItemService)
        {
            _FormIppplanItemService = FormIppplanItemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIppplanItemList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormIppplanItems = await _FormIppplanItemService.GetFormIppplanItemListByValue(offset, limit, val);

            if (FormIppplanItems == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormIppplanItems in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormIppplanItems);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIppplanItemList(string FormIppplanItem_name)
        {
            var FormIppplanItems = await _FormIppplanItemService.GetFormIppplanItemList(FormIppplanItem_name);

            if (FormIppplanItems == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormIppplanItem found for uci: {FormIppplanItem_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormIppplanItems);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIppplanItem(string FormIppplanItem_name)
        {
            var FormIppplanItems = await _FormIppplanItemService.GetFormIppplanItem(FormIppplanItem_name);

            if (FormIppplanItems == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormIppplanItem found for uci: {FormIppplanItem_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormIppplanItems);
        }

        [HttpPost]
        public async Task<ActionResult<FormIppplanItem>> AddFormIppplanItem(FormIppplanItem FormIppplanItem)
        {
            var dbFormIppplanItem = await _FormIppplanItemService.AddFormIppplanItem(FormIppplanItem);

            if (dbFormIppplanItem == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormIppplanItem.TbFormIppplanItemName} could not be added."
                );
            }

            return CreatedAtAction("GetFormIppplanItem", new { uci = FormIppplanItem.TbFormIppplanItemName }, FormIppplanItem);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormIppplanItem(FormIppplanItem FormIppplanItem)
        {           
            FormIppplanItem dbFormIppplanItem = await _FormIppplanItemService.UpdateFormIppplanItem(FormIppplanItem);

            if (dbFormIppplanItem == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormIppplanItem.TbFormIppplanItemName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormIppplanItem(FormIppplanItem FormIppplanItem)
        {            
            (bool status, string message) = await _FormIppplanItemService.DeleteFormIppplanItem(FormIppplanItem);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormIppplanItem);
        }
    }
}
