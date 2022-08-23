using cb.api.Repository;
using cb.api.Repository.Impl;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddScoped<IAdvertService, AdvertService>();
builder.Services.AddScoped<IAdvertRepo, AdvertRepo>();


var baseUrl = builder.Configuration.GetValue<String>("BaseUrl");
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(baseUrl)
});

//Mysql from user secrets
//builder.Configuration.AddEnvironmentVariables().AddUserSecrets<Program>();
//var connectionString = builder.Configuration.GetConnectionString("mysqlconnection");

//Get connection string from env
builder.Configuration.AddEnvironmentVariables();
var connectionString = Environment.GetEnvironmentVariable("RDS_DB_CONN_STRING");
Console.WriteLine(connectionString);

//db connection
builder.Services.AddDbContext<ApplicationContext>(options => options.UseMySQL(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
