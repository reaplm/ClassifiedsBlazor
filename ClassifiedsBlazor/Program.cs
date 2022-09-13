using Amazon.CloudWatch;
using Amazon.Runtime;
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

//-------------------------------AWS SETUP---------------------------------------------------------
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
builder.Services
    .AddSingleton(builder.Configuration)
    .AddLogging(loggingBuilder =>
    {
        loggingBuilder
        .AddConfiguration(builder.Configuration.GetSection("Logging"))
        .AddConsole()
        .AddDebug()
        .AddAWSProvider(builder.Configuration.GetAWSLoggingConfigSection().Config);
    });
//-------------------------------------------------------------------------------------------

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
