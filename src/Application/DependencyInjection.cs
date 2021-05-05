using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Application.Auth.Commands.Login;
using Application.Auth.Commands.SignUp;
using Application.Common.Extensions;
using Application.Common.Middleware;
using Application.Dishes.Commands.CreateDish;
using Application.Dishes.Commands.UpdateDish;
using Application.Dishes.Queries.GetDishes;
using Application.Restaurants.Commands.CreateRestaurant;
using Application.Restaurants.Commands.UpdateRestaurant;
using Application.Restaurants.Queries.GetRestaurants;
using Domain.Dto;
using Domain.Requests;
using Domain.ResourceParameters;
using Domain.Settings;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<ErrorHandlingMiddleware>();

            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));

            services.AddAuth(configuration.GetSection("Jwt").Get<JwtSettings>());
            services.UseSwagger();

            //services.AddTransient<IValidator<RestaurantForCreationDto>, RestaurantForCreationValidator>();
            //services.AddTransient<IValidator<RestaurantForUpdateDto>, RestaurantForUpdateValidator>();
            //services.AddTransient<IValidator<DishForCreationDto>, DishForCreationValidator>();
            //services.AddTransient<IValidator<DishForUpdateDto>, DishForUpdateValidator>();
            //services.AddTransient<IValidator<UserSignUpRequest>, UserSignUpRequestValidator>();
            //services.AddTransient<IValidator<UserLoginRequest>, UserLoginRequestValidator>();
            //services.AddTransient<IValidator<RestaurantsResourceParameters>, RestaurantQueryValidator>();
            //services.AddTransient<IValidator<DishesResourceParameters>, DishQueryValidator>();

            return services;
        }
    }
}
