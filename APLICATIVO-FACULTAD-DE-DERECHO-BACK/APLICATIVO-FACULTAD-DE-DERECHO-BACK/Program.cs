using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Context;
using Hangfire;
using Hangfire.PostgreSql;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.security;
using QuestPDF.Infrastructure;


AppContext.SetSwitch("System.Net.DontEnableSystemDefaultTlsVersions", false);
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
AppContext.SetSwitch("System.Net.DisableIPv6", true);

var builder = WebApplication.CreateBuilder(args);

QuestPDF.Settings.License = LicenseType.Community;
//
// ===================== SERVICIOS =====================
//

// Servicios externos (JWT, Email, etc.)
builder.Services.AddExternal(builder.Configuration);

// PostgreSQL
builder.Services.AddDbContext<ContextFacultadDerecho>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// Controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//
// ===================== JWT =====================
//
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
            ),

            ClockSkew = TimeSpan.Zero
        };
    });

//
// ===================== CORS =====================
//
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendDev", policy =>
        policy
            .WithOrigins(
                "http://localhost:3000",
                "http://localhost:5173",
                "http://localhost:4200"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
    );
});

//
// ===================== HANGFIRE =====================
//
builder.Services.AddHangfire(config =>
{
    config.UsePostgreSqlStorage(
        builder.Configuration.GetConnectionString("PostgresHangfire"),
        new PostgreSqlStorageOptions
        {
            SchemaName = "hangfire"
        });
});

builder.Services.AddHangfireServer();
builder.Services.AddScoped<HorarioPdfJob>();






var app = builder.Build();

//
// ===================== MIDDLEWARE =====================
//

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("FrontendDev");
}

// 🔐 ORDEN CRÍTICO
app.UseAuthentication();
app.UseAuthorization();

// ===================== HANGFIRE DASHBOARD =====================
app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = new[] { new HangfireAuthFilter() }
});

app.MapControllers();


app.MapControllers();

RecurringJob.AddOrUpdate<HorarioPdfJob>(
    "envio-horario-semanal",
    job => job.Ejecutar(),
    "6 6 * * 1", // Viernes 10:00 AM
    TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time") // Colombia
);


app.Run();
