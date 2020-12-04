using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using NLog;
using TransportInfo.API.ExcepMiddleware;
using TransportInfo.Data.Repositories;
using TransportInfo.Domain.Contracts;
using TransportInfo.Domain.Entities;
using TransportInfo.LoggerService;
using TransportInfo.Data.Contexts;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Microsoft.AspNetCore.Http;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]

namespace TransportInfo.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {


            services.AddMvc(setupAction =>
            {
                setupAction.Filters.Add(
                    new ProducesResponseTypeAttribute(StatusCodes.Status200OK));
                setupAction.Filters.Add(
                    new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
                setupAction.Filters.Add(
                    new ProducesResponseTypeAttribute(StatusCodes.Status404NotFound));
                setupAction.Filters.Add(
                    new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));
                setupAction.Filters.Add(
                   new ProducesResponseTypeAttribute(StatusCodes.Status422UnprocessableEntity));
                setupAction.Filters.Add(
                    new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
                setupAction.Filters.Add(
                  new ProducesDefaultResponseTypeAttribute());

                setupAction.ReturnHttpNotAcceptable = true;

            });
            


            services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });

            services.Configure<RequestLocalizationOptions>(
                options =>
                {
                    var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("en-US"),
                        new CultureInfo("ka-GE")
                    };

                    options.DefaultRequestCulture = new RequestCulture(culture: "ka-GE", uiCulture: "ka-GE");
                    
                    options.SupportedCultures = supportedCultures;
                    options.SupportedUICultures = supportedCultures;

                });


            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("TransportInfoApiSpecs", new OpenApiInfo 
                { Title = "TransportInfo API",
                    Version = "v1",
                    Description = "An API to perform  operations for Transports",                   
                    Contact = new OpenApiContact
                    {
                        Name = "Besiki Gogisvanidze",
                        Email = "beso.bg@gmail.com",
                       
                    },
                    License = new OpenApiLicense
                    {
                        Name = "TransportInfo API LICX",                       
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                setupAction.IncludeXmlComments(xmlPath);
            });

            services.AddControllers();

            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddScoped<ISortHelper<Transport>, SortHelper<Transport>>();
            services.AddAutoMapper(typeof(Startup));
            services.AddSingleton<ILoggerManager, LoggerManager>();

            services.AddMvc()
               .AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<Startup>());


            services.AddDbContext<TransportInfoContext>(o =>
            {
                o.UseSqlServer(Configuration.GetConnectionString("TransportInfoDBConnectionString"));
            });
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var localizeOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(localizeOptions.Value);

            app.UseSwagger();

            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint("/swagger/TransportInfoApiSpecs/swagger.json", "TransportInfo API V1");
            });
            app.UseMiddleware<ExceptionMiddleware>();


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });



        }
    }
}
