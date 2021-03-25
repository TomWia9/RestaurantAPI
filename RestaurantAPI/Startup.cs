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
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data.Dto;
using RestaurantAPI.Models;
using RestaurantAPI.Models.Auth;
using RestaurantAPI.Repositories;
using RestaurantAPI.Shared.Middleware;
using RestaurantAPI.Shared.Validators;
using Serilog;

namespace RestaurantAPI
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
            services.AddScoped<IRestaurantsRepository, RestaurantRepository>();
            services.AddScoped<IDishesRepository, DishesRepository>();
            services.AddScoped<RestaurantSeeder>();

            services.AddScoped<ErrorHandlingMiddleware>();

            services.AddTransient<IValidator<RestaurantForCreationDto>, RestaurantForCreationValidator>();
            services.AddTransient<IValidator<RestaurantForUpdateDto>, RestaurantForUpdateValidator>();
            services.AddTransient<IValidator<DishForCreationDto>, DishForCreationValidator>();
            services.AddTransient<IValidator<DishForUpdateDto>, DishForUpdateValidator>();


            services.AddDbContext<RestaurantDbContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("RestaurantDbConnection")));

            services.AddIdentity<User, Role>(options =>
                {
                    options.Password.RequiredLength = 8;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1d);
                    options.Lockout.MaxFailedAccessAttempts = 5;
                })
                .AddEntityFrameworkStores<RestaurantDbContext>()
                .AddDefaultTokenProviders();

            services.AddAutoMapper(GetType().Assembly);
            services.AddMvc().AddFluentValidation(options =>
            {
                options.ValidatorOptions.LanguageManager.Enabled = false;
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RestaurantAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, RestaurantSeeder seeder)
        {
            seeder.Seed();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RestaurantAPI v1"));

                app.UseSerilogRequestLogging();
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();

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
