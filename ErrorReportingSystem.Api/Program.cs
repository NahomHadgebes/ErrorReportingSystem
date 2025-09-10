using System.Text;
using ErrorReportingSystem.Api.Seed;
using ErrorReportingSystem.Api.Services;                  // TokenService, JwtOptions
using ErrorReportingSystem.Application.Interfaces;
using ErrorReportingSystem.Application.Services;          // IErrorReportService, ICommentService (om de ligger här)
using ErrorReportingSystem.Domain.Interfaces;
using ErrorReportingSystem.Infrastructure;                // AppDbContext (om namnrummet stämmer)
using ErrorReportingSystem.Infrastructure.Data;
using ErrorReportingSystem.Infrastructure.Repositories;   // ErrorReportRepository, CommentRepository
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // ---- Databas ----
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // ---- DI (repositories & services) ----
        builder.Services.AddScoped<IErrorReportRepository, ErrorReportRepository>();
        builder.Services.AddScoped<IErrorReportService, ErrorReportService>();
        builder.Services.AddScoped<ICommentRepository, CommentRepository>();
        builder.Services.AddScoped<ICommentService, CommentService>();

        // ---- Controllers ----
        builder.Services.AddControllers();

        // ---- JWT / Auth ----
        // Bind "Jwt" från appsettings.json till JwtOptions + registrera TokenService
        builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));
        builder.Services.AddSingleton<ITokenService, TokenService>();

        var jwt = builder.Configuration.GetSection("Jwt");
        var keyBytes = Encoding.UTF8.GetBytes(jwt["Key"]!);

        builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                    ValidateIssuer = true,
                    ValidIssuer = jwt["Issuer"],
                    ValidateAudience = true,
                    ValidAudience = jwt["Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        builder.Services.AddAuthorization();

        // ---- Swagger ----
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "ErrorReportingSystem.Api", Version = "v1" });

            // JWT i Swagger (Authorize-knapp)
            var jwtScheme = new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Skriv: Bearer {ditt-token}",
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            };
            c.AddSecurityDefinition("Bearer", jwtScheme);
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { jwtScheme, Array.Empty<string>() }
            });
        });

        var conn = builder.Configuration.GetConnectionString("DefaultConnection");
        Console.WriteLine($"[DEBUG] ENV: {builder.Environment.EnvironmentName}");
        Console.WriteLine($"[DEBUG] DefaultConnection: {conn}");

        var app = builder.Build();

        // ---- Pipeline ----
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();   // <— viktigt: före Authorization
        app.UseAuthorization();

        app.MapControllers();

        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            try
            {
                db.Database.Migrate();
                DbSeeder.Seed(db);
            }
            catch (Exception ex)
            {
                Console.WriteLine("[SEED] Failed: " + ex.Message);
                throw;
            }
        }

        app.Run();
    }
}
