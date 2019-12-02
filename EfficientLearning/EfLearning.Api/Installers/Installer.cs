using EfLearning.Api.Security;
using EfLearning.Business;
using EfLearning.Core.Users;
using EfLearning.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TokenOptions = EfLearning.Api.Security.TokenOptions;

namespace EfLearning.Api.Installers
{
    public class Installer
    {
        public void InstallDb(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EfContext>(options =>
               options.UseSqlServer(
                   configuration.GetConnectionString("IdentityConnection")));
            services.AddDefaultIdentity<AppUser>()
                .AddRoles<AppRole>()
                .AddEntityFrameworkStores<EfContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30);
                options.Lockout.AllowedForNewUsers = true;
                options.User.RequireUniqueEmail = false;
                options.SignIn.RequireConfirmedEmail = false;
            });
        }
        public void MvcInstaller(IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddMvc(options =>
                {
                    options.EnableEndpointRouting = false;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtSettings.SecurityKey)),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = true,
                ValidateLifetime = true,
            };

            services.AddSingleton(tokenValidationParameters);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = tokenValidationParameters;
                });

            services.AddSingleton(tokenValidationParameters);
            services.AddAuthorization(options =>
            {
                options.AddPolicy(CustomRoles.Admin, policy => policy.RequireRole(CustomRoles.Admin));
                options.AddPolicy(CustomRoles.Teacher, policy => policy.RequireRole(CustomRoles.Teacher));
                options.AddPolicy(CustomRoles.Student, policy => policy.RequireRole(CustomRoles.Student));
            });
        }
        public void InstallSwagger(IServiceCollection services, IConfiguration configuration)
        {

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "EFFICIENT LEARNING API", Version = "v1" });

                x.ExampleFilters();

                x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                x.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {new OpenApiSecurityScheme{Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }}, new List<string>()}
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                x.IncludeXmlComments(xmlPath);
            });

            services.AddSwaggerExamplesFromAssemblyOf<Startup>();

        }
    }
}
