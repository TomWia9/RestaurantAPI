using Application;
using Application.Common.Extensions;
using Application.Common.Middleware;
using Application.Common.Settings;
using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Api
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
            services.AddApplication();
            services.AddInfrastructure(Configuration);
            services.AddAuth(Configuration.GetSection("Jwt").Get<JwtSettings>());

            services.Configure<JwtSettings>(Configuration.GetSection("Jwt"));

            services.AddMvc(options =>
            {
                options.ReturnHttpNotAcceptable = true;
                options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
            }).AddFluentValidation(options => { options.ValidatorOptions.LanguageManager.Enabled = false; });

            services.AddCors(options =>
            {
                options.AddPolicy("AngularApp", builder =>
                {
                    builder.WithOrigins("http://localhost:4200").AllowAnyHeader()
                        .AllowAnyMethod().WithExposedHeaders("X-Pagination");
                });
            });

            services.AddControllers();
            services.UseSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, RestaurantSeeder seeder)
        {
            app.UseResponseCaching();
            app.UseStaticFiles();

            seeder.Seed();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(setupAction =>
                {
                    setupAction.SwaggerEndpoint("/swagger/RestaurantAPISepcification/swagger.json",
                        "RestaurantAPI");
                });

                app.UseSerilogRequestLogging();
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("AngularApp");

            app.UseAuth();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}