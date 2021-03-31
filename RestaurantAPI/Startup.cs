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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data.Dto;
using RestaurantAPI.Extensions;
using RestaurantAPI.Models;
using RestaurantAPI.Models.Auth;
using RestaurantAPI.Repositories;
using RestaurantAPI.Services;
using RestaurantAPI.Settings;
using RestaurantAPI.Shared.Middleware;
using RestaurantAPI.Shared.Validators;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;

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
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<RestaurantSeeder>();

            services.AddScoped<ErrorHandlingMiddleware>();

            services.AddTransient<IValidator<RestaurantForCreationDto>, RestaurantForCreationValidator>();
            services.AddTransient<IValidator<RestaurantForUpdateDto>, RestaurantForUpdateValidator>();
            services.AddTransient<IValidator<DishForCreationDto>, DishForCreationValidator>();
            services.AddTransient<IValidator<DishForUpdateDto>, DishForUpdateValidator>();

            services.Configure<JwtSettings>(Configuration.GetSection("Jwt"));


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

            services.AddAuth(Configuration.GetSection("Jwt").Get<JwtSettings>());

            services.AddAutoMapper(GetType().Assembly);
            services.AddMvc().AddFluentValidation(options =>
            {
                options.ValidatorOptions.LanguageManager.Enabled = false;
            });

            services.AddControllers();

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc(
                    "RestaurantAPISepcification",
                    new OpenApiInfo()
                    {
                        Title = "RestaurantAPI",
                        Version = "1",
                        Description = "Through this API you can access restaurants and dishes",
                        Contact = new OpenApiContact()
                        {
                            Email = "tomaszwiatrowski9@gmail.com",
                            Name = "Tomasz Wiatrowski",
                            Url = new Uri("https://www.linkedin.com/in/tomasz-wiatrowski-279b00176/")
                        },
                        License = new OpenApiLicense()
                        {
                            Name = "MIT License",
                            Url = new Uri("https://opensource.org/licenses/MIT")
                        }
                    });

                OpenApiSecurityScheme securityDefinition = new()
                {
                    Name = "Bearer",
                    BearerFormat = "JWT",
                    Scheme = "bearer",
                    Description = "Specify the authorization token.",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                };
                setupAction.AddSecurityDefinition("jwt_auth", securityDefinition);

                OpenApiSecurityScheme securityScheme = new()
                {
                    Reference = new OpenApiReference()
                    {
                        Id = "jwt_auth",
                        Type = ReferenceType.SecurityScheme
                    }
                };
                OpenApiSecurityRequirement securityRequirements = new()
                {
                    {securityScheme, Array.Empty<string>() },
                };
                setupAction.AddSecurityRequirement(securityRequirements);

                //add xml comments
                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

                setupAction.IncludeXmlComments(xmlCommentsFullPath);

                setupAction.AddFluentValidationRules();

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

            app.UseAuth();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
