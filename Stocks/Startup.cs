﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Stocks.Controllers.Reference.WeatherForecast;

namespace Stocks
{
    public class Startup
    {
        private readonly Assembly controllerAssembly = typeof(WeatherForecastController).Assembly;
        private const string OpenApiPath = "/openapi/v1/openapi.json";
        private const string OpenApiDocumentName = "openapi";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddApplicationPart(controllerAssembly)
                .AddControllersAsServices();

            services.AddOpenApiDocument(config =>
            {
                config.DocumentName = OpenApiDocumentName;
            }); // registers a OpenAPI v3.0 document with the name "v1" (default)
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ConfigureNSwag(app); // Do this before UseRouting();

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            // app.UseAuthorization();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
        }

        private void ConfigureNSwag(IApplicationBuilder app)
        {
            // Serves the registered OpenAPI documents
            app.UseOpenApi(config =>
            {
                config.DocumentName = OpenApiDocumentName;
                config.Path = OpenApiPath;
            });

            // Serves the Redoc UI to view the OpenAPI documents
            app.UseReDoc(config =>
            {
                config.Path = "/redoc";
                config.DocumentPath = OpenApiPath;
            });
        }
    }
}
