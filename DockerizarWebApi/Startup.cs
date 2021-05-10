using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DockerizarWebApi.Repository;
using MediatR;
using DockerizarWebApi.Behavior;
using Microsoft.EntityFrameworkCore;

namespace DockerizarWebApi
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
            services.AddControllers();
            services.AddSwaggerGen(
                x =>
                {
                    x.SwaggerDoc("v1", new OpenApiInfo { Title = "CQRS_Test", Version = "Version 1" });
                    x.CustomSchemaIds(x => x.FullName);
                });
            services.AddSingleton<Repository.Repository>();
            services.AddMediatR(typeof(Startup).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            // Solo para desarrollo, nunca hacer esto en un ambiente productivo

            var server = Configuration["DBServer"] ?? "localhost";
            var port = Configuration["DBPort"] ?? "1433";
            var userId = Configuration["DBUser"] ?? "SA";
            var password = Configuration["DbPassword"] ?? "PaSSw0rd2021";
            var database = Configuration["DBDatabase"] ?? "TodoDB";

            services.AddDbContext<ApplicationDbContext.ApplicationDbContext>(options => 
                options.UseSqlServer($"Server={server},{port};Initial Catalog={database};User ID={userId};Password={password}"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplication1 v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
