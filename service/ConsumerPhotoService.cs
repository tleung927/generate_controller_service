using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IConsumerPhotoService
    {
        // ConsumerPhotos Services
        Task<List<ConsumerPhoto>> GetConsumerPhotoListByValue(int offset, int limit, string val); // GET All ConsumerPhotoss
        Task<ConsumerPhoto> GetConsumerPhoto(string ConsumerPhoto_name); // GET Single ConsumerPhotos        
        Task<List<ConsumerPhoto>> GetConsumerPhotoList(string ConsumerPhoto_name); // GET List ConsumerPhotos        
        Task<ConsumerPhoto> AddConsumerPhoto(ConsumerPhoto ConsumerPhoto); // POST New ConsumerPhotos
        Task<ConsumerPhoto> UpdateConsumerPhoto(ConsumerPhoto ConsumerPhoto); // PUT ConsumerPhotos
        Task<(bool, string)> DeleteConsumerPhoto(ConsumerPhoto ConsumerPhoto); // DELETE ConsumerPhotos
    }

    public class ConsumerPhotoService : IConsumerPhotoService
    {
        private readonly XixsrvContext _db;

        public ConsumerPhotoService(XixsrvContext db)
        {
            _db = db;
        }

        #region ConsumerPhotos

        public async Task<List<ConsumerPhoto>> GetConsumerPhotoListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ConsumerPhotos.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ConsumerPhoto> GetConsumerPhoto(string ConsumerPhoto_id)
        {
            try
            {
                return await _db.ConsumerPhotos.FindAsync(ConsumerPhoto_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ConsumerPhoto>> GetConsumerPhotoList(string ConsumerPhoto_id)
        {
            try
            {
                return await _db.ConsumerPhotos
                    .Where(i => i.ConsumerPhotoId == ConsumerPhoto_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ConsumerPhoto> AddConsumerPhoto(ConsumerPhoto ConsumerPhoto)
        {
            try
            {
                await _db.ConsumerPhotos.AddAsync(ConsumerPhoto);
                await _db.SaveChangesAsync();
                return await _db.ConsumerPhotos.FindAsync(ConsumerPhoto.ConsumerPhotoId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ConsumerPhoto> UpdateConsumerPhoto(ConsumerPhoto ConsumerPhoto)
        {
            try
            {
                _db.Entry(ConsumerPhoto).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ConsumerPhoto;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteConsumerPhoto(ConsumerPhoto ConsumerPhoto)
        {
            try
            {
                var dbConsumerPhoto = await _db.ConsumerPhotos.FindAsync(ConsumerPhoto.ConsumerPhotoId);

                if (dbConsumerPhoto == null)
                {
                    return (false, "ConsumerPhoto could not be found");
                }

                _db.ConsumerPhotos.Remove(ConsumerPhoto);
                await _db.SaveChangesAsync();

                return (true, "ConsumerPhoto got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ConsumerPhotos
    }
}
