using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ErrorViewModelController : ControllerBase
    {
        private readonly IErrorViewModelService _ErrorViewModelService;

        public ErrorViewModelController(IErrorViewModelService ErrorViewModelService)
        {
            _ErrorViewModelService = ErrorViewModelService;
        }

        [HttpGet]
        public async Task<IActionResult> GetErrorViewModelList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ErrorViewModels = await _ErrorViewModelService.GetErrorViewModelListByValue(offset, limit, val);

            if (ErrorViewModels == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ErrorViewModels in database");
            }

            return StatusCode(StatusCodes.Status200OK, ErrorViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> GetErrorViewModelList(string ErrorViewModel_name)
        {
            var ErrorViewModels = await _ErrorViewModelService.GetErrorViewModelList(ErrorViewModel_name);

            if (ErrorViewModels == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ErrorViewModel found for uci: {ErrorViewModel_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ErrorViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> GetErrorViewModel(string ErrorViewModel_name)
        {
            var ErrorViewModels = await _ErrorViewModelService.GetErrorViewModel(ErrorViewModel_name);

            if (ErrorViewModels == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ErrorViewModel found for uci: {ErrorViewModel_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ErrorViewModels);
        }

        [HttpPost]
        public async Task<ActionResult<ErrorViewModel>> AddErrorViewModel(ErrorViewModel ErrorViewModel)
        {
            var dbErrorViewModel = await _ErrorViewModelService.AddErrorViewModel(ErrorViewModel);

            if (dbErrorViewModel == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ErrorViewModel.TbErrorViewModelName} could not be added."
                );
            }

            return CreatedAtAction("GetErrorViewModel", new { uci = ErrorViewModel.TbErrorViewModelName }, ErrorViewModel);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateErrorViewModel(ErrorViewModel ErrorViewModel)
        {           
            ErrorViewModel dbErrorViewModel = await _ErrorViewModelService.UpdateErrorViewModel(ErrorViewModel);

            if (dbErrorViewModel == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ErrorViewModel.TbErrorViewModelName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteErrorViewModel(ErrorViewModel ErrorViewModel)
        {            
            (bool status, string message) = await _ErrorViewModelService.DeleteErrorViewModel(ErrorViewModel);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ErrorViewModel);
        }
    }
}
