using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Context;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK;

var builder = WebApplication.CreateBuilder(args);

// Necesario para Render
builder.WebHost.UseUrls("http://0.0.0.0:" + (Environment.GetEnvironmentVariable("PORT") ?? "8080"));

builder.Services.AddExternal(builder.Configuration);

builder.Services.AddDbContext<ContextFacultadDerecho>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

app.UseCors("AllowFrontend");

app.UseAuthorization();
app.MapControllers();
app.Run();
