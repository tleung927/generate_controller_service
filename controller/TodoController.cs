using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _TodoService;

        public TodoController(ITodoService TodoService)
        {
            _TodoService = TodoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodoList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Todos = await _TodoService.GetTodoListByValue(offset, limit, val);

            if (Todos == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Todos in database");
            }

            return StatusCode(StatusCodes.Status200OK, Todos);
        }

        [HttpGet]
        public async Task<IActionResult> GetTodoList(string Todo_name)
        {
            var Todos = await _TodoService.GetTodoList(Todo_name);

            if (Todos == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Todo found for uci: {Todo_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Todos);
        }

        [HttpGet]
        public async Task<IActionResult> GetTodo(string Todo_name)
        {
            var Todos = await _TodoService.GetTodo(Todo_name);

            if (Todos == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Todo found for uci: {Todo_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Todos);
        }

        [HttpPost]
        public async Task<ActionResult<Todo>> AddTodo(Todo Todo)
        {
            var dbTodo = await _TodoService.AddTodo(Todo);

            if (dbTodo == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Todo.TbTodoName} could not be added."
                );
            }

            return CreatedAtAction("GetTodo", new { uci = Todo.TbTodoName }, Todo);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTodo(Todo Todo)
        {           
            Todo dbTodo = await _TodoService.UpdateTodo(Todo);

            if (dbTodo == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Todo.TbTodoName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTodo(Todo Todo)
        {            
            (bool status, string message) = await _TodoService.DeleteTodo(Todo);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Todo);
        }
    }
}
