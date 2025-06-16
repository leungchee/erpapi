using System.Text;
using ERPAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Data;
using System.Data.SqlClient;
using ERPAPI.Data;

namespace ERPAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Configure JWT Authentication
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                        ValidAudience = builder.Configuration["JwtSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]!))
                    };
                });

            // Configure Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = builder.Configuration["SwaggerSettings:Title"],
                    Version = builder.Configuration["SwaggerSettings:Version"],
                    Description = builder.Configuration["SwaggerSettings:Description"]
                });

                // 启用 Swagger 注解
                c.EnableAnnotations();

                // Add JWT Authentication to Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            // Register services
            builder.Services.AddHttpClient();
            
            // 注册数据库连接
            builder.Services.AddScoped<IDbConnection>(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                return new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            });

            // 注册仓储
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();

            builder.Services.AddScoped<JwtService>();
            builder.Services.AddScoped<PasswordService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<TokenProvider>();
            builder.Services.AddScoped<ILogisticsService, LogisticsService>();
            builder.Services.AddScoped<IMaterialUsageService, MaterialUsageService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
