using BusinessLayer;
using BusinessLayer.Abstract;
using BusinessLayer.Concrate;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();


//fluent validator
builder.Services.AddFluentValidationAutoValidation(M =>
{
    M.DisableDataAnnotationsValidation = true;
}).AddFluentValidationClientsideAdapters()
  .AddValidatorsFromAssemblyContaining<PersonalAddValidation>();



//türkçe karakter sorunu için
builder.Services.AddWebEncoders(o => {
    o.TextEncoderSettings = new System.Text.Encodings.Web.TextEncoderSettings(UnicodeRanges.All);
});


builder.Services.AddDbContext<Context>();
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<Context>();

builder.Services.AddCustomServices(); //dependency classýmýnýn metodunu çalýþtýrdýk



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");



app.Run();
