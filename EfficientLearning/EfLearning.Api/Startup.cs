using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EfLearning.Api.EmailServices;
using EfLearning.Api.Installers;
using EfLearning.Business.Abstract;
using EfLearning.Business.Concrete;
using EfLearning.Data.Abstract;
using EfLearning.Data.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EfLearning.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var inst = new Installer();
            inst.InstallDb(services, Configuration);
            inst.InstallIdentity(services);
            inst.InstallJwt(services, Configuration);
            services.AddScoped<IRefreshTokenDal, RefreshTokenDal>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICustomIdentityManager, CustomIdentityManager>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddCors(opts =>
            {
                opts.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseCors();
            app.UseMvc();


        }
    }
}
