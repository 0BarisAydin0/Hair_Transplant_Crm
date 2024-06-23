using DataAccessLayer.Concrate;
using EntityLayer;
using Microsoft.EntityFrameworkCore;

namespace PresentationLayer.Services
{
    public interface ISiteSettingsService
    {
        Task<Settings> GetSiteSettingsAsync();
    }

    public class SiteSettingsService : ISiteSettingsService
    {
        private readonly Context _dbContext;

        public SiteSettingsService(Context dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Settings> GetSiteSettingsAsync()
        {
            return await _dbContext.Settings.FirstOrDefaultAsync();
        }
    }
}
