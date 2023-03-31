using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ITodoService
    {
        // Todos Services
        Task<List<Todo>> GetTodoListByValue(int offset, int limit, string val); // GET All Todoss
        Task<Todo> GetTodo(string Todo_name); // GET Single Todos        
        Task<List<Todo>> GetTodoList(string Todo_name); // GET List Todos        
        Task<Todo> AddTodo(Todo Todo); // POST New Todos
        Task<Todo> UpdateTodo(Todo Todo); // PUT Todos
        Task<(bool, string)> DeleteTodo(Todo Todo); // DELETE Todos
    }

    public class TodoService : ITodoService
    {
        private readonly XixsrvContext _db;

        public TodoService(XixsrvContext db)
        {
            _db = db;
        }

        #region Todos

        public async Task<List<Todo>> GetTodoListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Todos.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Todo> GetTodo(string Todo_id)
        {
            try
            {
                return await _db.Todos.FindAsync(Todo_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Todo>> GetTodoList(string Todo_id)
        {
            try
            {
                return await _db.Todos
                    .Where(i => i.TodoId == Todo_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Todo> AddTodo(Todo Todo)
        {
            try
            {
                await _db.Todos.AddAsync(Todo);
                await _db.SaveChangesAsync();
                return await _db.Todos.FindAsync(Todo.TodoId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Todo> UpdateTodo(Todo Todo)
        {
            try
            {
                _db.Entry(Todo).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Todo;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteTodo(Todo Todo)
        {
            try
            {
                var dbTodo = await _db.Todos.FindAsync(Todo.TodoId);

                if (dbTodo == null)
                {
                    return (false, "Todo could not be found");
                }

                _db.Todos.Remove(Todo);
                await _db.SaveChangesAsync();

                return (true, "Todo got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Todos
    }
}
