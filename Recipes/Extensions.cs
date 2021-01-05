namespace Recipes
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;

    public static class Extensions
    {
        public static IApplicationBuilder ConfigureExceptionHandler(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";

                    //ToDo return json with message
                    await context.Response.WriteAsync("Something went wrong");
                });
            });
        }

        public static IServiceCollection ConfigureSwagger(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Version = "v1",
                        Title = "My Recipes",
                        Description = "All time family favourites"
                    });
                    c.IncludeXmlComments("Recipes.xml");
                    c.DescribeAllEnumsAsStrings();

                    c.AddSecurityDefinition(
                        "Bearer",
                        new OpenApiSecurityScheme
                        {
                            Description =
                                "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                            Name = "Authorization",
                            In = ParameterLocation.Header,
                            Type = SecuritySchemeType.ApiKey
                        });

                    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                                Scheme = "oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header,

                            },
                            new List<string>()
                        }
                    });
                }
            );
        }
    }
}
