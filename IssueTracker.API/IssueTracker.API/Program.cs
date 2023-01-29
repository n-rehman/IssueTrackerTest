using IssueTracker.Data;
using IssueTracker.Shared.Security;
using IssueTracker.Business.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using IssueTracker.API.Helpers;
using System.Text.Json.Serialization;
using IssueTracker.Business.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();/*

.AddJsonOptions(options =>
{
	options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});
*/
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IDataSeedIssueTracker, IssueTrackerSeedService>();
builder.Services.AddScoped(typeof(ITicketHandlerService), typeof(TicketHandlerService));
builder.Services.AddScoped(typeof(IUsersHandlerService), typeof(UserHandlerService));


builder.Services.AddCors();

builder.Services.AddDbContext<IssueTrackerDbContext>(opt =>
	opt.UseInMemoryDatabase("IssueTrackerDB"));

//register serilog
var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();


builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	app.SeedData();
	app.UseDeveloperExceptionPage();
}
else
{
	//security middleware
	app.UseMiddleware<ApiKeyMiddleware>();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



var CORSOriginURL = app.Configuration.GetValue(typeof(string), "CorsOriginUrl");

if (CORSOriginURL == null)
	CORSOriginURL = "http://localhost:3000"; 

app.UseCors(options =>
	 options.WithOrigins(CORSOriginURL.ToString())
			.AllowAnyHeader()
			.AllowAnyMethod());


app.Run();
