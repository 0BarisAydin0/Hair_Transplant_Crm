using EntityLayer;
using EntityLayer.Definition;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Org.BouncyCastle.Asn1.Ocsp;
using Microsoft.Extensions.Options;

namespace DataAccessLayer.Concrate
{
    public class Context : IdentityDbContext<AppUser, AppRole, int>
    {



        //#region DeveloperMigration
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
        //    var configuration = builder.Build();

        //    optionsBuilder.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);
        //}
        //#endregion

        #region LiveMigration
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        //public Context(IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(options)
        //{
        //    _configuration = configuration;
        //    _httpContextAccessor = httpContextAccessor;
        //}

        public Context(DbContextOptions<Context> options, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
     : base(options)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var connectionString = GetConnectionStringFromDatabase();
                optionsBuilder.UseSqlServer(connectionString);
            }
        }



        private string GetConnectionStringFromDatabase()
        {
            // Kullanıcının e-posta adresini al
            var userEmail = GetUserEmail();

            // Kullanıcının e-posta adresine bağlı bağlantı dizesini veritabanından al
            var connectionString = string.Empty;
            if (!string.IsNullOrEmpty(userEmail))
            {
                var initialConnectionString = _configuration.GetConnectionString("DefaultConnection");

                using (var connection = new SqlConnection(initialConnectionString))
                {
                    connection.Open();

                    var commandText = "SELECT ConnectionString FROM CustomerDatabaseConfigs WHERE Email = @Email";
                    var command = new SqlCommand(commandText, connection);
                    command.Parameters.AddWithValue("@Email", userEmail);
                    var result = command.ExecuteScalar();
                    connectionString = result != null ? result.ToString() : string.Empty;
                }
            }

            // Bağlantı dizesi boşsa, varsayılan bağlantı dizesini kullan
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = _configuration.GetConnectionString("DefaultConnection");
            }

            return connectionString;
        }
        public string GetEmailFromCookie()
        {
            if (_httpContextAccessor.HttpContext != null)
            {
                // HttpContext nesnesini kullanarak çerezden e-posta adresini okuyun
                string encodedEmail = _httpContextAccessor.HttpContext.Request.Cookies["Email"];
                //string decodedEmail = Uri.UnescapeDataString(encodedEmail);
                return encodedEmail; // Decoded email'i döndürün
            }
            return null;
        }
        private string GetUserEmail()
        {
            // Çerezden e-posta adresini al
            string email = GetEmailFromCookie();

            // HttpContext nesnesini kontrol et
            var httpContext = _httpContextAccessor?.HttpContext;

            if (httpContext == null)
            {
                return email ?? "admin@admin.com"; // Eğer HttpContext null ise çerezden alınan email'i veya varsayılan email'i döndür
            }

            // Kullanıcı e-postasını Claims'den al
            var userEmail = httpContext.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            // Kullanıcı e-postası null ise çerezden alınan email'i veya varsayılan email'i kullan
            if (string.IsNullOrEmpty(userEmail))
            {
                userEmail = email ?? "admin@admin.com";
            }

            return userEmail;
        }
        #endregion

        public DbSet<Settings> Settings { get; set; }
        public DbSet<Personal> Personals { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<ChronicProblems> ChronicProblems { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<InfectiousDisease> InfectiousDiseases { get; set; }
        public DbSet<ReminderDate> ReminderDates { get; set; }
        public DbSet<Scope> Scopes { get; set; }
        public DbSet<Technique> Techniques { get; set; }
        public DbSet<PatientOperationImg> patientOperationImgs { get; set; }
        public DbSet<CustomerDatabaseConfigs> CustomerDatabaseConfigs { get; set; }


    }
}
