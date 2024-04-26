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

namespace DataAccessLayer.Concrate
{
    public class Context : IdentityDbContext<AppUser, AppRole, int>
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var configuration = builder.Build();

            optionsBuilder.UseSqlServer(configuration["ConnectionStrings:Default"]);
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
        public DbSet<PatientOperationImg> patientOperationImgs{ get; set; }
    }
}
