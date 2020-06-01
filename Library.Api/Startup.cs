using AutoMapper;
using FluentValidation.AspNetCore;
using Library.Api.Validators;
using Library.Core;
using Library.Core.Abstract.Managers;
using Library.Core.Abstract.Repositories;
using Library.Core.Concrete.Managers;
using Library.Core.Concrete.Repositories;
using Library.Core.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Library.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public IWebHostEnvironment Environment { get; set; }

        public Startup(IWebHostEnvironment environment)
        {
            Environment = environment;
            var builder = new ConfigurationBuilder()
                .SetBasePath(Environment.ContentRootPath)
                .AddJsonFile("appsettings.json",optional:true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.EnvironmentName}", optional:true);
            if (Environment.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
            
        }
       
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddDbContext<ApplicationDbContext>(o =>
                o.UseMySql(Configuration["Data:ConnectionString"], mySqlOptions =>
                    mySqlOptions.ServerVersion(Configuration["Data:ServerVersion"])));

            services.AddScoped<DbContext, ApplicationDbContext>();
            services.AddScoped<ICoffeeRepository, CoffeeRepository>();
            services.AddScoped<ICoffeeManager, CoffeeManager>();
            services.AddScoped<IProviderRepository, ProviderRepository>();
            services.AddScoped<IProviderManager, ProviderManager>();
            services.AddScoped<IOriginCountryRepository, OriginCountryRepository>();
            services.AddScoped<IOriginCountryManager, OriginCountryManager>();
           
            services.AddAutoMapper(typeof(Startup).Assembly);
            services.AddControllers()
                .AddFluentValidation(o =>
            {
                o.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                o.RegisterValidatorsFromAssemblyContaining<CoffeeRequestValidator>();
                o.RegisterValidatorsFromAssemblyContaining<ProviderRequestValidator>();
                o.RegisterValidatorsFromAssemblyContaining<OriginCountryRequestValidator>();
            });
            
        }

     
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}