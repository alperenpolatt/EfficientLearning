using System;
using AutoMapper;
using EfLearning.Api.EmailServices;
using EfLearning.Api.Installers;
using EfLearning.Business.Abstract;
using EfLearning.Business.Concrete;
using EfLearning.Data.Abstract;
using EfLearning.Data.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            inst.MvcInstaller(services, Configuration);
            inst.InstallSwagger(services, Configuration);



            services.AddScoped<IRefreshTokenDal, RefreshTokenDal>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICustomIdentityManager, CustomIdentityManager>();

            services.AddScoped<IUserService, UserManager>();
            services.AddScoped<IUserDal, UserDal>();

            services.AddScoped<IGivenClassroomService, GivenClassroomManager>();
            services.AddScoped<IGivenClassroomDal, GivenClassroomDal>();

            services.AddScoped<ICourseService, CourseManager>();
            services.AddScoped<ICourseDal, CourseDal>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddCors(opts =>
            {
                opts.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                SeedDatabase.Seed();
            }

            app.UseAuthentication();


            var swaggerOptions = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

            app.UseSwagger(option => { option.RouteTemplate = swaggerOptions.JsonRoute; });

            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint(swaggerOptions.UiEndpoint, swaggerOptions.Description);
            });
            app.UseMvc();

        }
    }
}
