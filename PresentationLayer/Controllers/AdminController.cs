using DataAccessLayer.Concrate;
using DataAccessLayer.DTOs;
using DataAccessLayer.Services;
using EntityLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PresentationLayer.Services;
using System.Security.AccessControl;
using System.Xml.Linq;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace PresentationLayer.Controllers
{
    [AllowAnonymous]
    public class AdminController : Controller
    {


        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IServiceProvider _serviceProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Context _context;
        private readonly IConfiguration _configuration;

        public AdminController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IEmailSender emailSender,
            IServiceProvider serviceProvider,
            IHttpContextAccessor httpContextAccessor,
            Context context,
            IConfiguration configuration) // IConfiguration ekleniyor
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _serviceProvider = serviceProvider;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _configuration = configuration; // IConfiguration enjeksiyonu yapılıyor
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                CustomerDatabaseConfigs customerDatabase = new CustomerDatabaseConfigs
                {
                    Email = model.Email,
                    DbName = model.DbName,
                    ConnectionString = model.ConnectionString,
                    CustomerNumber = model.CustomerNumber,
                    Note = model.Note
                };

                _context.Add(customerDatabase);
                await _context.SaveChangesAsync(); // Asenkron olarak kaydet

                if (string.IsNullOrWhiteSpace(model.DbName))
                {
                    throw new ArgumentException("Veritabanı adı boş olamaz.", nameof(model.DbName));
                }

                var createDbQuery = $"CREATE DATABASE [{model.DbName}]";

                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = createDbQuery;
                    _context.Database.OpenConnection();
                    await command.ExecuteNonQueryAsync();
                    _context.Database.CloseConnection();
                }

                var customerDatabaseconn = await _context.CustomerDatabaseConfigs.FirstOrDefaultAsync(c => c.Email == model.Email);          

                if (customerDatabaseconn != null)
                {
                    string connectionString = customerDatabaseconn.ConnectionString;
                    string sqlFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "script.sql");

                    if (System.IO.File.Exists(sqlFilePath))
                    {
                        string sqlScript = await System.IO.File.ReadAllTextAsync(sqlFilePath);
                        await ExecuteSqlScript(connectionString, sqlScript);


                        var user = new AppUser { UserName = model.Email, Email = model.Email };
                        var result = await _userManager.CreateAsync(user, model.Password);
                        if (result.Succeeded)
                        {
                            await _signInManager.SignInAsync(user, isPersistent: false); // isPersistent: false -> tarayıcı kapandığında kullanıcının oturumu                                                                  sonlanır
                            return RedirectToAction("Index", "Dashboard");
                        }
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }                        
                    }
                    else
                    {
                        return NotFound("SQL file not found.");
                    }


                }
               


                // DbContextOptions'u modelden gelen bağlantı bilgisiyle oluştur
                //var dbContextOptions = new DbContextOptionsBuilder<Context>()
                //                            .UseSqlServer(model.ConnectionString)
                //                            .Options;

                //// Yeni Context örneği oluştur
                //using (var context = new Context(dbContextOptions, _httpContextAccessor, _configuration))
                //{
                //    // Veritabanı migration işlemini gerçekleştir
                //    context.Database.Migrate();

                //    // Yeni kullanıcı ekleme işlemi                   
                //}


            }

            // ModelState.IsValid false ise veya kullanıcı zaten mevcutsa, Register sayfasını tekrar göster
            return View(model);
        }


        private async Task ExecuteSqlScript(string connectionString, string sqlScript)
        {
            // Split the script based on "GO" keyword, which should be on its own line
            var sqlStatements = System.Text.RegularExpressions.Regex.Split(sqlScript, @"^\s*GO\s*$", System.Text.RegularExpressions.RegexOptions.Multiline | System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                foreach (var statement in sqlStatements)
                {
                    // Trim and check if the statement is not empty or whitespace
                    var trimmedStatement = statement.Trim();
                    if (!string.IsNullOrWhiteSpace(trimmedStatement))
                    {
                        using (var command = new SqlCommand(trimmedStatement, connection))
                        {
                            try
                            {
                                await command.ExecuteNonQueryAsync();
                            }
                            catch (SqlException ex)
                            {
                                // Log or handle the exception as needed
                                throw new Exception($"An error occurred while executing the SQL script: {ex.Message}", ex);
                            }
                        }
                    }
                }
            }
        }


    }
}
