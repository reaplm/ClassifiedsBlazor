using Amazon.CloudWatch;
using Amazon.Runtime;
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

//AWS SETUP
// Configures default options like AWS region - if you don't provide any, its taken from the AWS Fargate environment
var awsOptions = builder.Configuration.GetAWSOptions();
awsOptions.Credentials = new EnvironmentVariablesAWSCredentials();

var cred = awsOptions.Credentials.GetCredentials();
Console.WriteLine(cred);
builder.Services.AddDefaultAWSOptions(awsOptions);

// Makes the Amazon CloudWatch SDK available in the DI container
builder.Services.AddAWSService<IAmazonCloudWatch>();
builder.Services.AddScoped<AmazonCloudWatchClient>();

// Confifures the ASP.NET Core logging to write logs to the log group provided in your CDK code
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddAWSProvider();
    loggingBuilder.SetMinimumLevel(LogLevel.Debug);
});



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
