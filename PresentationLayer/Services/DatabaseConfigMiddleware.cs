using DataAccessLayer.Concrate;
using EntityLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using PresentationLayer.Models;
using System.Configuration;

namespace PresentationLayer.Services
{
    public class DatabaseConfigMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly Context _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DatabaseConfigMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccessor)
        {
            _next = next;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task Invoke(HttpContext context, Context dbContext)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var userEmail = context.User.Identity.Name;
                var databaseConfig = dbContext.databaseConfigs.FirstOrDefault(c => c.Email == userEmail);

                if (databaseConfig != null)
                {
                    var connectionString = databaseConfig.ConnectionString;
                    // Bağlantı dizesini güncelle
                    context.Items["ConnectionString"] = connectionString;
                    connectionString = context.Items["ConnectionString"] as string;

                    // ConnectionString değerini consola yazdır
                    Console.WriteLine("ConnectionString: " + connectionString);
                }
                else
                {
                    // Kullanıcıya uygun bir hata mesajı gösterilebilir veya varsayılan bir bağlantı dizesi kullanılabilir
                }
            }
            else // Kimlik doğrulanmamışsa
            {
                // Varsayılan bağlantı dizesini appsettings.json'dan al
                var defaultConnectionString = dbContext.Database.GetConnectionString();
                // Bağlantı dizesini güncelle
                context.Items["ConnectionString"] = defaultConnectionString;

                var connectionString = context.Items["ConnectionString"] as string;

                // ConnectionString değerini consola yazdır
                Console.WriteLine("ConnectionString: " + connectionString);

            }

            await _next(context);
        }



    }
}


