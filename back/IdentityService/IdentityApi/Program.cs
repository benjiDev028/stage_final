using Microsoft.EntityFrameworkCore;
using IdentityDataAccess;
using IdentityApi.Profiles;
using IdentityBusinessLogic.Profiles;
using IdentityDataAccess.Repositories.Interfaces;
using IdentityDataAccess.Repositories.Implementations;
using IdentityBusinessLogic.Services.Interfaces;
using IdentityBusinessLogic.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<UserContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
x => x.MigrationsAssembly("IdentityDataAccess")));

builder.Services.AddAutoMapper(typeof(ApplicationProfile), typeof(MappingProfile));

builder.Services.AddScoped<IRepository,UserRepository>()
    .AddScoped<IUserService,UserService>()
    .AddScoped<IUserAuthenticate, UserAuthenticate>()
    .AddScoped<ICredentialsService, CredentialsService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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
