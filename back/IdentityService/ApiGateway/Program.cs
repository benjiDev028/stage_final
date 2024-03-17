using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Values;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();
builder.Services.AddOcelot();

builder.Services.AddCors();

var app = builder.Build();






app.MapControllers();

app.UseCors(builder =>
{
    builder.WithOrigins("http://localhost:3000") //  l'URL de l application front-end
           .AllowAnyHeader()
           .AllowAnyMethod();
});

await app.UseOcelot();
app.Run();
