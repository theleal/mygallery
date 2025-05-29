using APIGallery.Context;
using APIGallery.Interfaces;
using APIGallery.Models;
using APIGallery.Repositorios;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Serviços
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DB e configurações
builder.Services.AddDbContext<ContextProject>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<IObraRepository, ObraRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost5500",
        policy =>
        {
            policy.WithOrigins("http://127.0.0.1:5500")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});


var app = builder.Build();

// CORS deve vir logo aqui
app.UseCors("AllowLocalhost5500");

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// HTTPS e auth
app.UseHttpsRedirection();
app.UseAuthorization();

// Controllers
app.MapControllers();

app.Run();
