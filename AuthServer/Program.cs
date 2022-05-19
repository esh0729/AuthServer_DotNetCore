using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using AuthServer;

var builder = WebApplication.CreateBuilder(args);

var configBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
IConfiguration configuration = configBuilder.Build();
builder.Services.Configure<AppConfig>(configuration);
configuration.GetSection("AppConfig").Bind(AppConfig.instance);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<UserDbContext>(opt => opt.UseSqlServer(AppConfig.instance.DbConnectionString));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.Urls.Add("http://172.22.74.12:" + AppConfig.instance.ServicePort);
	app.Urls.Add("http://localhost:" + AppConfig.instance.ServicePort);
}

// Configure the HTTP request pipeline.
//app.UseHsts();
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
