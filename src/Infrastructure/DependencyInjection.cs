using System;
using Application.Common.Interfaces;
using Infrastructure.Identity;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IRestaurantsRepository, RestaurantRepository>();
            services.AddScoped<IDishesRepository, DishesRepository>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<RestaurantSeeder>();

            services.AddDbContext<RestaurantDbContext>
                (options => options.UseSqlServer(configuration.GetConnectionString("RestaurantDbConnection")));

            services.AddIdentity<User, Role>(options =>
                {
                    options.Password.RequiredLength = 8;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1d);
                    options.Lockout.MaxFailedAccessAttempts = 5;
                })
                .AddEntityFrameworkStores<RestaurantDbContext>()
                .AddDefaultTokenProviders();


            return services;
        }
    }
}