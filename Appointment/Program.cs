using Appointment;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services); // calling ConfigureServices method

var app = builder.Build();

startup.Configure(app, builder.Environment);