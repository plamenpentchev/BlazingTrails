using BlazingTrails.API.Persistence;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using System.Reflection;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<BlazingTrailsContext>( options => 
    options.UseSqlite(builder.Configuration.GetConnectionString("BlazingTrailsContext")?? "")
);

builder.Services
    .AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(Assembly.Load("BlazingTrails.Shared")));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseWebAssemblyDebugging();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseBlazorFrameworkFiles();

app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),@"Images")),
    RequestPath = new Microsoft.AspNetCore.Http.PathString("/Images")
}); 


app.UseRouting();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
