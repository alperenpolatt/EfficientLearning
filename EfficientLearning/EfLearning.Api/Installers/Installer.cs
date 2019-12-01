using EfLearning.Api.Security;
using EfLearning.Business;
using EfLearning.Core.Users;
using EfLearning.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using TokenOptions = EfLearning.Api.Security.TokenOptions;

namespace EfLearning.Api.Installers
{
    public class Installer
    {
        public void InstallDb(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EfContext>(options =>
             options.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));

           
        }
        public void InstallIdentity(IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>()
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
        public void InstallJwt(IServiceCollection services, IConfiguration configuration)
        {


            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Latest);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtSettings.SecurityKey)),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true
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
    }
}
