using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IImageService
    {
        // Images Services
        Task<List<Image>> GetImageListByValue(int offset, int limit, string val); // GET All Imagess
        Task<Image> GetImage(string Image_name); // GET Single Images        
        Task<List<Image>> GetImageList(string Image_name); // GET List Images        
        Task<Image> AddImage(Image Image); // POST New Images
        Task<Image> UpdateImage(Image Image); // PUT Images
        Task<(bool, string)> DeleteImage(Image Image); // DELETE Images
    }

    public class ImageService : IImageService
    {
        private readonly XixsrvContext _db;

        public ImageService(XixsrvContext db)
        {
            _db = db;
        }

        #region Images

        public async Task<List<Image>> GetImageListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Images.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Image> GetImage(string Image_id)
        {
            try
            {
                return await _db.Images.FindAsync(Image_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Image>> GetImageList(string Image_id)
        {
            try
            {
                return await _db.Images
                    .Where(i => i.ImageId == Image_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Image> AddImage(Image Image)
        {
            try
            {
                await _db.Images.AddAsync(Image);
                await _db.SaveChangesAsync();
                return await _db.Images.FindAsync(Image.ImageId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Image> UpdateImage(Image Image)
        {
            try
            {
                _db.Entry(Image).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Image;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteImage(Image Image)
        {
            try
            {
                var dbImage = await _db.Images.FindAsync(Image.ImageId);

                if (dbImage == null)
                {
                    return (false, "Image could not be found");
                }

                _db.Images.Remove(Image);
                await _db.SaveChangesAsync();

                return (true, "Image got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Images
    }
}
