using ClassifiedsBlazor.Services;
using ClassifiedsBlazor.Services.Impl;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

//DI
builder.Services.AddScoped<IAdvertService, AdvertService>();

//var baseUrl = builder.Configuration.GetValue<String>("BackendUrl");

//Get service name from env
builder.Configuration.AddEnvironmentVariables();
var baseUrl = Environment.GetEnvironmentVariable("BackendUrl");
Console.WriteLine("Printing BackendUrl... " + baseUrl);

builder.Services.AddScoped(sp => new HttpClient
{
	BaseAddress = new Uri(baseUrl)
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}
//use api controllers
app.MapControllers();
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
