using ClassifiedsBlazor.Data;
using ClassifiedsBlazor.Repository;
using ClassifiedsBlazor.Repository.Impl;
using ClassifiedsBlazor.Services;
using ClassifiedsBlazor.Services.Impl;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();


builder.Services.AddScoped<IAdvertService, AdvertService>();
builder.Services.AddScoped<IAdvertRepo, AdvertRepo>();
builder.Services.AddScoped(sp => new HttpClient());

//Mysql
var connectionString = builder.Configuration.GetSection("ConnectionStrings")["mysqlconnection"];
builder.Services.AddDbContext<ApplicationContext>
	(options => options.UseMySQL(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
