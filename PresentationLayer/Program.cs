using BusinessLayer;
using BusinessLayer.Abstract;
using BusinessLayer.Concrate;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.Services;
using EntityLayer;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Build.Evaluation;
using Microsoft.EntityFrameworkCore;
using PresentationLayer.Controllers;
using PresentationLayer.Services;
using System.Configuration;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//proje bazında authorize
builder.Services.AddControllersWithViews(options =>
{
    var policy = new AuthorizationPolicyBuilder()
                     .RequireAuthenticatedUser()
                     .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
}).AddRazorRuntimeCompilation();


// Add services to the container.
//builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

// Fluent validation
builder.Services.AddFluentValidationAutoValidation(m =>
{
    m.DisableDataAnnotationsValidation = true;
}).AddFluentValidationClientsideAdapters()
  .AddValidatorsFromAssemblyContaining<PersonalAddValidation>();

// Türkçe karakter sorunu için
builder.Services.AddWebEncoders(o =>
{
    o.TextEncoderSettings = new System.Text.Encodings.Web.TextEncoderSettings(UnicodeRanges.All);
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Configuration.AddJsonFile("appsettings.json");

// Database context ve Identity ayarları
builder.Services.AddDbContext<Context>(options =>
{
    options.UseSqlServer(connectionString);
});
builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 4;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
    .AddEntityFrameworkStores<Context>()
    .AddRoleManager<RoleManager<AppRole>>()
    .AddSignInManager<SignInManager<AppUser>>()
    .AddUserManager<UserManager<AppUser>>()
    .AddDefaultTokenProviders()
    .AddErrorDescriber<CustomIdentityValidator>();

builder.Services.AddCustomServices(); // Dependency class'ınızın metodunu çalıştırdık

// EmailSend Dependency Injection
//builder.Configuration.AddJsonFile("appsettings.json");
var smtpSettings = builder.Configuration.GetSection("SmtpSettings").Get<SmtpSettings>();
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
builder.Services.AddTransient<IEmailSender, EmailSender>();

// Identity ayarları
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    // Diğer politikalar
});

// Authorization ayarları
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
        policy.RequireRole("Admin"));
    options.AddPolicy("ModeratorPolicy", policy =>
        policy.RequireRole("Moderator"));
    // Diğer roller buraya eklenebilir
});

// Application Cookie ayarları
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Login/Login";
    options.AccessDeniedPath = "/Login/AccessDenied";
    options.SlidingExpiration = true;
});
builder.Services.AddHttpContextAccessor();
//builder.Services.AddScoped<DatabaseInitializer>(); // auto migrate scope entegrasyon
builder.Services.AddScoped<UserManager<AppUser>>();
builder.Services.AddScoped<SignInManager<AppUser>>();
builder.Services.AddScoped<ISiteSettingsService, SiteSettingsService>();


builder.Services.AddDistributedMemoryCache(); // Oturum verilerini bellek içinde saklamak için
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Oturum zaman aşımı süresi
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.Run();
