using Microsoft.EntityFrameworkCore;
using CommentDataAccess.Repositories;
using CommentDataAccess;
using CommentApi.Profiles;
using CommentBusinessLogic.Profiles;
using CommentDataAccess.Repositories.Interfaces;
using CommentDataAccess.Repositories.Implementations;
using CommentBusinessLogic.Services.Interfaces;
using CommentBusinessLogic.Services.Implementations;
using CommentApi.Configurations;
using Microsoft.Extensions.Configuration;
using MassTransit;
using Microsoft.Extensions.Options;





var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//context
builder.Services.AddDbContext<CommentContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
x => x.MigrationsAssembly("CommentDataAccess")));


builder.Services.AddAutoMapper(typeof(ApplicationProfile), typeof(MappingProfiles));

builder.Services.AddScoped<IRepository, CommentRepository>()
.AddScoped<ICommentService, commentService>();
builder.Services.AddOptions<RabbitMQConfiguration>().Bind(builder.Configuration.GetSection("RabbitMQ"));

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        var options = context.GetRequiredService<IOptions<RabbitMQConfiguration>>().Value;

        cfg.Host(options.Host, h =>
        {
            h.Username(options.Username);
            h.Password(options.Password);
        });
    });
});




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
