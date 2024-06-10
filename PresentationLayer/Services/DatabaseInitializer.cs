using DataAccessLayer.Concrate;
using EntityLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace PresentationLayer.Services
{
    public class DatabaseInitializer
    {
        private readonly IServiceProvider _serviceProvider;

        public DatabaseInitializer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task InitializeDatabaseAsync(string email)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<Context>();
                    context.Database.Migrate(); // Veritabanını güncelle (migrations uygula)

                    // Yeni kullanıcı ekleme işlemi
                    var userManager = services.GetRequiredService<UserManager<AppUser>>();

                    var existingUser = await userManager.FindByEmailAsync("admin@admin.com");
                    if (existingUser == null)
                    {
                        var newUser = new AppUser { UserName = "admin@admin.com", Email = "admin@admin.com" };
                        var result = await userManager.CreateAsync(newUser, "Aa12345*");
                        if (result.Succeeded)
                        {
                            // Kullanıcı başarıyla eklendi
                        }
                        else
                        {
                            // Kullanıcı eklenirken bir hata oluştu
                            foreach (var error in result.Errors)
                            {
                                // Hata mesajlarını loglama veya işleme alma
                            }
                        }
                    }
                    else
                    {
                        // Kullanıcı zaten mevcut
                    }
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<DatabaseInitializer>>();
                    logger.LogError(ex, "Veritabanı migration işlemi sırasında bir hata oluştu.");
                }
            }
        }
    }
}

