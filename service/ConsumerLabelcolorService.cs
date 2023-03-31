using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IConsumerLabelcolorService
    {
        // ConsumerLabelcolors Services
        Task<List<ConsumerLabelcolor>> GetConsumerLabelcolorListByValue(int offset, int limit, string val); // GET All ConsumerLabelcolorss
        Task<ConsumerLabelcolor> GetConsumerLabelcolor(string ConsumerLabelcolor_name); // GET Single ConsumerLabelcolors        
        Task<List<ConsumerLabelcolor>> GetConsumerLabelcolorList(string ConsumerLabelcolor_name); // GET List ConsumerLabelcolors        
        Task<ConsumerLabelcolor> AddConsumerLabelcolor(ConsumerLabelcolor ConsumerLabelcolor); // POST New ConsumerLabelcolors
        Task<ConsumerLabelcolor> UpdateConsumerLabelcolor(ConsumerLabelcolor ConsumerLabelcolor); // PUT ConsumerLabelcolors
        Task<(bool, string)> DeleteConsumerLabelcolor(ConsumerLabelcolor ConsumerLabelcolor); // DELETE ConsumerLabelcolors
    }

    public class ConsumerLabelcolorService : IConsumerLabelcolorService
    {
        private readonly XixsrvContext _db;

        public ConsumerLabelcolorService(XixsrvContext db)
        {
            _db = db;
        }

        #region ConsumerLabelcolors

        public async Task<List<ConsumerLabelcolor>> GetConsumerLabelcolorListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ConsumerLabelcolors.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ConsumerLabelcolor> GetConsumerLabelcolor(string ConsumerLabelcolor_id)
        {
            try
            {
                return await _db.ConsumerLabelcolors.FindAsync(ConsumerLabelcolor_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ConsumerLabelcolor>> GetConsumerLabelcolorList(string ConsumerLabelcolor_id)
        {
            try
            {
                return await _db.ConsumerLabelcolors
                    .Where(i => i.ConsumerLabelcolorId == ConsumerLabelcolor_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ConsumerLabelcolor> AddConsumerLabelcolor(ConsumerLabelcolor ConsumerLabelcolor)
        {
            try
            {
                await _db.ConsumerLabelcolors.AddAsync(ConsumerLabelcolor);
                await _db.SaveChangesAsync();
                return await _db.ConsumerLabelcolors.FindAsync(ConsumerLabelcolor.ConsumerLabelcolorId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ConsumerLabelcolor> UpdateConsumerLabelcolor(ConsumerLabelcolor ConsumerLabelcolor)
        {
            try
            {
                _db.Entry(ConsumerLabelcolor).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ConsumerLabelcolor;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteConsumerLabelcolor(ConsumerLabelcolor ConsumerLabelcolor)
        {
            try
            {
                var dbConsumerLabelcolor = await _db.ConsumerLabelcolors.FindAsync(ConsumerLabelcolor.ConsumerLabelcolorId);

                if (dbConsumerLabelcolor == null)
                {
                    return (false, "ConsumerLabelcolor could not be found");
                }

                _db.ConsumerLabelcolors.Remove(ConsumerLabelcolor);
                await _db.SaveChangesAsync();

                return (true, "ConsumerLabelcolor got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ConsumerLabelcolors
    }
}
