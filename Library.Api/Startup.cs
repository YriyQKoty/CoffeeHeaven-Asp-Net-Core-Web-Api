using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AutoMapper;
using FluentValidation.AspNetCore;
using Library.Api.Validators;
using Library.Core;
using Library.Core.Abstract.Managers;
using Library.Core.Abstract.Repositories;
using Library.Core.Concrete.Managers;
using Library.Core.Concrete.Models;
using Library.Core.Concrete.Repositories;
using Library.Core.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

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
            });

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            
            
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            
            services.AddAuthorization();
            
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(conf =>
                {
                    conf.RequireHttpsMetadata = false;
                    conf.SaveToken = true;
                    conf.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Configuration["Jwt:JwtIssuer"],
                        ValidAudience = Configuration["Jwt:JwtIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:JwtKey"])),
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "WebApi",
                    Description = "Coffee Heaven Web Api",
                });
                
                c.AddSecurityDefinition("Bearer", 
                    new OpenApiSecurityScheme{
                        Description = "JWT Authorization header using the Bearer scheme.",
                        Type = SecuritySchemeType.Http, 
                        Scheme = "bearer" 
                    });
            
                c.AddSecurityRequirement(new OpenApiSecurityRequirement{ 
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference{
                                Id = "Bearer", 
                                Type = ReferenceType.SecurityScheme
                            }
                        },new List<string>()
                    }
                });
                
            });


        }

     
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSwagger();
            
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                c.RoutePrefix = String.Empty;
            });

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}