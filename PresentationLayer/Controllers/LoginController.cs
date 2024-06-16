using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrate;
using DataAccessLayer.DTOs;
using DataAccessLayer.Services;
using EntityLayer;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PresentationLayer.Services;
using System.Net;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace PresentationLayer.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IServiceProvider _serviceProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Context _context;
        private readonly IConfiguration _configuration;
        private readonly IUserClaimsPrincipalFactory<AppUser> _claimsFactory;
        public LoginController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IEmailSender emailSender,
            IServiceProvider serviceProvider,
            IHttpContextAccessor httpContextAccessor,
            Context context,
            IConfiguration configuration,
            IUserClaimsPrincipalFactory<AppUser> claimsFactory) // IConfiguration ekleniyor
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _serviceProvider = serviceProvider;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _configuration = configuration; // IConfiguration enjeksiyonu yapılıyor
            _claimsFactory = claimsFactory;
        }



        public IActionResult Lockout()
        {
            return View();
        }


        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        //{
        //    returnUrl ??= Url.Content("~/");

        //    if (ModelState.IsValid)
        //    {
        //        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
        //        if (result.Succeeded)
        //        {
        //            return RedirectToAction("LoginCheck", "Home");
        //        }
        //        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        //        return View(model);
        //    }

        //    return View(model);
        //}


        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {

                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);
                    if (result.Succeeded)
                    {
                       
                        return RedirectToAction("Index", "Dashboard");
                    }
                    else if (result.IsLockedOut)
                    {
                        var lockoutEnd = await _userManager.GetLockoutEndDateAsync(user);
                        var remainingTime = lockoutEnd - DateTime.Now;
                        ViewBag.RemainingTime = remainingTime;
                        return View("Lockout");
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View(model);
        }

        //[HttpPost]
        //public async Task<IActionResult> Login(LoginViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // CustomerDatabaseConfigs tablosundan ilgili kullanıcıyı bul
        //        var customerDatabase = await _context.CustomerDatabaseConfigs.FirstOrDefaultAsync(c => c.Email == model.Email);

        //        if (customerDatabase != null)
        //        {
        //            // DbContextOptions'u modelden gelen bağlantı bilgisiyle oluştur
        //            var dbContextOptions = new DbContextOptionsBuilder<Context>()
        //                                        .UseSqlServer(customerDatabase.ConnectionString)
        //                                        .Options;
        //            var identityOptions = Options.Create(new IdentityOptions());
        //            var claimsFactory = new UserClaimsPrincipalFactory<AppUser>(_userManager, identityOptions);
        //            var signInManager = new SignInManager<AppUser>(_userManager, _httpContextAccessor, claimsFactory, null, null, null, null);

        //            // Yeni bir Context örneği oluşturarak yeni veritabanına bağlan
        //            using (var newContext = new Context(dbContextOptions, _httpContextAccessor, _configuration))
        //            {
        //                // Kullanıcıyı yeni veritabanı context'inde bul
        //                var user = await newContext.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

        //                if (user != null)
        //                {
        //                    await signInManager.SignInAsync(user, isPersistent: false); // isPersistent: false kalıcı oturum için
        //                    return RedirectToAction("Index", "Dashboard");

        //                }
        //                else
        //                {
        //                    // Kullanıcı giriş yapamaz
        //                }

        //                //if (user != null)
        //                //{
        //                //    var identityOptions = Options.Create(new IdentityOptions());

        //                //    // Create UserClaimsPrincipalFactory with the options
        //                //    var claimsFactory = new UserClaimsPrincipalFactory<AppUser>(_userManager, identityOptions);
        //                //    var signInManager = new SignInManager<AppUser>(_userManager, _httpContextAccessor, claimsFactory, null, null, null, null);

        //                //    // Kullanıcı adı ve şifreyle giriş denemesi yap
        //                //    var result = await signInManager.PasswordSignInAsync(user.UserName, model.Password, false, lockoutOnFailure: false);

        //                //    if (result.Succeeded)
        //                //    {
        //                //        // Giriş başarılı, kullanıcı ana sayfaya yönlendirilebilir
        //                //        return RedirectToAction("Index", "Home");
        //                //    }
        //                //    else
        //                //    {
        //                //        ModelState.AddModelError(string.Empty, "Invalid login attempt. Please check your credentials.");
        //                //    }
        //                //}
        //                //else
        //                //{
        //                //    ModelState.AddModelError(string.Empty, "User not found. Please check your credentials.");
        //                //}
        //            }
        //        }
        //        else
        //        {
        //            ModelState.AddModelError(string.Empty, "Invalid login attempt. Customer database not found.");
        //        }
        //    }

        //    // ModelState.IsValid false ise veya işlem başarısızsa, login sayfasını tekrar göster
        //    return View(model);
        //}




        public static class HashingHelper
        {
            public static string Hash(string input)
            {
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    // Girdiyi hashleyin
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                    // Hash'i bir string olarak formatlayın
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }
                    return builder.ToString();
                }
            }
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
                var email = model.Email;
                var databaseInitializer = new DatabaseInitializer(_serviceProvider);
                await databaseInitializer.InitializeDatabaseAsync(email);

                Random random = new Random();
                int code;
                code = random.Next(100000, 1000000);
                var user = new AppUser { UserName = model.Email, Email = model.Email, ConfirmCode = code };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false); // isPersistent: false -> tarayıcı kapandığında kullanıcının oturumu sonlanır
                    return RedirectToAction("Index", "Dashboard");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }




        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            // Diğer kimlik doğrulama şemalarından (external logins, cookie vb.) da çıkış yapabilirsiniz
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            return RedirectToAction("Login", "Login");
        }






        public IActionResult AddRoleToUser()
        {
            return View();
        }




        /*

        [HttpPost]
        public async Task<IActionResult> AddRoleToUser(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                // Kullanıcı bulunamadı, uygun bir hata mesajı gösterin
                return NotFound();
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                // Rol atama başarılı, gerekirse bir geri dönüş mesajı gösterin
                return Ok("Role added successfully");
            }
            else
            {
                // Rol atama başarısız, hata mesajlarını işleyin ve uygun bir hata mesajı gösterin
                return BadRequest(result.Errors);
            }
        }

        */



        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Kullanıcı yok veya e-posta onaylanmamışsa hata mesajı göster
                    return View("ForgotPasswordConfirmation");
                }

                // Şifre sıfırlama bağlantısı oluştur ve kullanıcıya e-posta ile gönder
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                await _emailSender.SendEmailAsync(model.Email, "Şifre Sıfırlama", $"Lütfen şifrenizi sıfırlamak için <a href='{callbackUrl}'>buraya tıklayınız</a>.");
                return View("ForgotPasswordConfirmation");
            }

            return View(model);
        }


    }

}

