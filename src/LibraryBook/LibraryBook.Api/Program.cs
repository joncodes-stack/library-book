using LibraryBook.Api.Configuration;
using LibraryBook.EF.Context;
using LibraryBook.Ioc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
.AddEnvironmentVariables();


// Add services to the container.

builder.Services.AddControllers();

// ConfigureServices

builder.Services.AddDbContext<LibraryBookContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.JwtConfiguration(builder.Configuration);

builder.Services.ResolveDependencies();

var app = builder.Build();

 app.UseSwagger();
 app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();

app.Run();
