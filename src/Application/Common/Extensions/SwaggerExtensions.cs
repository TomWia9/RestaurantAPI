using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Application.Common.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection UseSwagger(this IServiceCollection services)
        {
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
                    { securityScheme, Array.Empty<string>() },
                };
                setupAction.AddSecurityRequirement(securityRequirements);

                //Collect all referenced projects output XML document file paths  
                var currentAssembly = Assembly.GetExecutingAssembly();
                var xmlDocs = currentAssembly.GetReferencedAssemblies()
                    .Union(new[] { currentAssembly.GetName() })
                    .Select(a => Path.Combine(Path.GetDirectoryName(currentAssembly.Location) ?? string.Empty, $"{a.Name}.xml"))
                    .Where(File.Exists).ToList();

                foreach (var d in xmlDocs)
                {
                    setupAction.IncludeXmlComments(d);
                }

                //setupAction.AddFluentValidationRules();

            });

            return services;
        }
    }
}
