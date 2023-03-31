using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IViewTrxFaceToFaceService
    {
        // ViewTrxFaceToFaces Services
        Task<List<ViewTrxFaceToFace>> GetViewTrxFaceToFaceListByValue(int offset, int limit, string val); // GET All ViewTrxFaceToFacess
        Task<ViewTrxFaceToFace> GetViewTrxFaceToFace(string ViewTrxFaceToFace_name); // GET Single ViewTrxFaceToFaces        
        Task<List<ViewTrxFaceToFace>> GetViewTrxFaceToFaceList(string ViewTrxFaceToFace_name); // GET List ViewTrxFaceToFaces        
        Task<ViewTrxFaceToFace> AddViewTrxFaceToFace(ViewTrxFaceToFace ViewTrxFaceToFace); // POST New ViewTrxFaceToFaces
        Task<ViewTrxFaceToFace> UpdateViewTrxFaceToFace(ViewTrxFaceToFace ViewTrxFaceToFace); // PUT ViewTrxFaceToFaces
        Task<(bool, string)> DeleteViewTrxFaceToFace(ViewTrxFaceToFace ViewTrxFaceToFace); // DELETE ViewTrxFaceToFaces
    }

    public class ViewTrxFaceToFaceService : IViewTrxFaceToFaceService
    {
        private readonly XixsrvContext _db;

        public ViewTrxFaceToFaceService(XixsrvContext db)
        {
            _db = db;
        }

        #region ViewTrxFaceToFaces

        public async Task<List<ViewTrxFaceToFace>> GetViewTrxFaceToFaceListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ViewTrxFaceToFaces.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ViewTrxFaceToFace> GetViewTrxFaceToFace(string ViewTrxFaceToFace_id)
        {
            try
            {
                return await _db.ViewTrxFaceToFaces.FindAsync(ViewTrxFaceToFace_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ViewTrxFaceToFace>> GetViewTrxFaceToFaceList(string ViewTrxFaceToFace_id)
        {
            try
            {
                return await _db.ViewTrxFaceToFaces
                    .Where(i => i.ViewTrxFaceToFaceId == ViewTrxFaceToFace_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ViewTrxFaceToFace> AddViewTrxFaceToFace(ViewTrxFaceToFace ViewTrxFaceToFace)
        {
            try
            {
                await _db.ViewTrxFaceToFaces.AddAsync(ViewTrxFaceToFace);
                await _db.SaveChangesAsync();
                return await _db.ViewTrxFaceToFaces.FindAsync(ViewTrxFaceToFace.ViewTrxFaceToFaceId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ViewTrxFaceToFace> UpdateViewTrxFaceToFace(ViewTrxFaceToFace ViewTrxFaceToFace)
        {
            try
            {
                _db.Entry(ViewTrxFaceToFace).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ViewTrxFaceToFace;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteViewTrxFaceToFace(ViewTrxFaceToFace ViewTrxFaceToFace)
        {
            try
            {
                var dbViewTrxFaceToFace = await _db.ViewTrxFaceToFaces.FindAsync(ViewTrxFaceToFace.ViewTrxFaceToFaceId);

                if (dbViewTrxFaceToFace == null)
                {
                    return (false, "ViewTrxFaceToFace could not be found");
                }

                _db.ViewTrxFaceToFaces.Remove(ViewTrxFaceToFace);
                await _db.SaveChangesAsync();

                return (true, "ViewTrxFaceToFace got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ViewTrxFaceToFaces
    }
}
