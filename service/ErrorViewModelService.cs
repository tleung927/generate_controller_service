using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IErrorViewModelService
    {
        // ErrorViewModels Services
        Task<List<ErrorViewModel>> GetErrorViewModelListByValue(int offset, int limit, string val); // GET All ErrorViewModelss
        Task<ErrorViewModel> GetErrorViewModel(string ErrorViewModel_name); // GET Single ErrorViewModels        
        Task<List<ErrorViewModel>> GetErrorViewModelList(string ErrorViewModel_name); // GET List ErrorViewModels        
        Task<ErrorViewModel> AddErrorViewModel(ErrorViewModel ErrorViewModel); // POST New ErrorViewModels
        Task<ErrorViewModel> UpdateErrorViewModel(ErrorViewModel ErrorViewModel); // PUT ErrorViewModels
        Task<(bool, string)> DeleteErrorViewModel(ErrorViewModel ErrorViewModel); // DELETE ErrorViewModels
    }

    public class ErrorViewModelService : IErrorViewModelService
    {
        private readonly XixsrvContext _db;

        public ErrorViewModelService(XixsrvContext db)
        {
            _db = db;
        }

        #region ErrorViewModels

        public async Task<List<ErrorViewModel>> GetErrorViewModelListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ErrorViewModels.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ErrorViewModel> GetErrorViewModel(string ErrorViewModel_id)
        {
            try
            {
                return await _db.ErrorViewModels.FindAsync(ErrorViewModel_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ErrorViewModel>> GetErrorViewModelList(string ErrorViewModel_id)
        {
            try
            {
                return await _db.ErrorViewModels
                    .Where(i => i.ErrorViewModelId == ErrorViewModel_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ErrorViewModel> AddErrorViewModel(ErrorViewModel ErrorViewModel)
        {
            try
            {
                await _db.ErrorViewModels.AddAsync(ErrorViewModel);
                await _db.SaveChangesAsync();
                return await _db.ErrorViewModels.FindAsync(ErrorViewModel.ErrorViewModelId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ErrorViewModel> UpdateErrorViewModel(ErrorViewModel ErrorViewModel)
        {
            try
            {
                _db.Entry(ErrorViewModel).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ErrorViewModel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteErrorViewModel(ErrorViewModel ErrorViewModel)
        {
            try
            {
                var dbErrorViewModel = await _db.ErrorViewModels.FindAsync(ErrorViewModel.ErrorViewModelId);

                if (dbErrorViewModel == null)
                {
                    return (false, "ErrorViewModel could not be found");
                }

                _db.ErrorViewModels.Remove(ErrorViewModel);
                await _db.SaveChangesAsync();

                return (true, "ErrorViewModel got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ErrorViewModels
    }
}
