using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ICder08CommentService
    {
        // Cder08Comments Services
        Task<List<Cder08Comment>> GetCder08CommentListByValue(int offset, int limit, string val); // GET All Cder08Commentss
        Task<Cder08Comment> GetCder08Comment(string Cder08Comment_name); // GET Single Cder08Comments        
        Task<List<Cder08Comment>> GetCder08CommentList(string Cder08Comment_name); // GET List Cder08Comments        
        Task<Cder08Comment> AddCder08Comment(Cder08Comment Cder08Comment); // POST New Cder08Comments
        Task<Cder08Comment> UpdateCder08Comment(Cder08Comment Cder08Comment); // PUT Cder08Comments
        Task<(bool, string)> DeleteCder08Comment(Cder08Comment Cder08Comment); // DELETE Cder08Comments
    }

    public class Cder08CommentService : ICder08CommentService
    {
        private readonly XixsrvContext _db;

        public Cder08CommentService(XixsrvContext db)
        {
            _db = db;
        }

        #region Cder08Comments

        public async Task<List<Cder08Comment>> GetCder08CommentListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Cder08Comments.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Cder08Comment> GetCder08Comment(string Cder08Comment_id)
        {
            try
            {
                return await _db.Cder08Comments.FindAsync(Cder08Comment_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Cder08Comment>> GetCder08CommentList(string Cder08Comment_id)
        {
            try
            {
                return await _db.Cder08Comments
                    .Where(i => i.Cder08CommentId == Cder08Comment_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Cder08Comment> AddCder08Comment(Cder08Comment Cder08Comment)
        {
            try
            {
                await _db.Cder08Comments.AddAsync(Cder08Comment);
                await _db.SaveChangesAsync();
                return await _db.Cder08Comments.FindAsync(Cder08Comment.Cder08CommentId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Cder08Comment> UpdateCder08Comment(Cder08Comment Cder08Comment)
        {
            try
            {
                _db.Entry(Cder08Comment).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Cder08Comment;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteCder08Comment(Cder08Comment Cder08Comment)
        {
            try
            {
                var dbCder08Comment = await _db.Cder08Comments.FindAsync(Cder08Comment.Cder08CommentId);

                if (dbCder08Comment == null)
                {
                    return (false, "Cder08Comment could not be found");
                }

                _db.Cder08Comments.Remove(Cder08Comment);
                await _db.SaveChangesAsync();

                return (true, "Cder08Comment got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Cder08Comments
    }
}
