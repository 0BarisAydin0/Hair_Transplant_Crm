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

namespace DataAccessLayer.Concrate
{
     public class Context : IdentityDbContext<AppUser, AppRole, int>
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public Context(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
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

                    var commandText = "SELECT ConnectionString FROM databaseConfigs WHERE Email = @Email";
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
        private string GetUserEmail()
        {
            var httpContext = new HttpContextAccessor().HttpContext;
            var userEmail = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            //var userEmail = httpContext?.User?.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
            return userEmail;
        }
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
        public DbSet<DatabaseConfig> databaseConfigs { get; set; }
    }
}
