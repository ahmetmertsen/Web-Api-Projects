using ECommerceAPI.Application.Features.Customers.Commands.Create;
using ECommerceAPI.Application.Mapping.CustomerMapper;
using ECommerceAPI.Application.Validators.CustomerValidator;
using ECommerceAPI.Infrastructure;
using ECommerceAPI.Persistence;
using ECommerceAPI.WebAPI.Middlewares;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ECommerceAPI.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "ECommerceAPI",
                    Version = "v1",
                });

                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "JWT Authorization header kullanımı: token"
                });

                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
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

            
            builder.Services.AddPersistenceService(builder.Configuration);
            builder.Services.AddInfrastructureServices(builder.Configuration);

            builder.Services.AddMediatR(cfg =>
                    cfg.RegisterServicesFromAssemblyContaining<CreateCustomerCommand>());

            builder.Services.AddValidatorsFromAssemblyContaining<CreateCustomerCommandValidator>();

            builder.Services.AddAutoMapper(typeof(CustomerProfile).Assembly);

            #region Authentication-Authorization
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        // Doğrulaması gereken değerler
                        ValidateAudience = true, //Oluşturulacak token değerini kimlerin/hangi originlerin/sitelerin kullanıcığını belirlediğimiz değerdir.
                        ValidateIssuer = true, // Oluşturulacak token değerini kimin dağıttını ifade edeceğimiz alanıdır.
                        ValidateLifetime = true, //Oluşturulan token değerinin süresini kontrol edecek olan doğrulamadır.
                        ValidateIssuerSigningKey = true, //Üretilecek token değerinin uygulamamıza ait bir değer olduğunu ifade eden security key verisinin doğrulanmasıdır.

                        ValidAudience = builder.Configuration["Token:Audience"],
                        ValidIssuer = builder.Configuration["Token:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
                        NameClaimType = JwtRegisteredClaimNames.UniqueName,
                        RoleClaimType = ClaimTypes.Role,
                        ClockSkew = TimeSpan.FromMinutes(1)
                    };
                });

            builder.Services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new Microsoft.AspNetCore.Authorization.AuthorizationPolicyBuilder(
                    JwtBearerDefaults.AuthenticationScheme
                )
                .RequireAuthenticatedUser()
                .Build();
            });

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                };

                options.Events.OnRedirectToAccessDenied = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    return Task.CompletedTask;
                };
            });
            #endregion


            var app = builder.Build();

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
