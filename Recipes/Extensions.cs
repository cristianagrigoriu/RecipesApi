namespace Recipes
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;
    using Persistence;

    public static class Extensions
    {
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

        public static AuthenticationBuilder AddJwtAuthentication(this IServiceCollection serviceCollection, Func<IConfigurationSection> configuration)
        {
            var jwtSettings = ToJwtSettings(configuration);

            return serviceCollection.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(jwtSettings.BaseSecret)),
                        ValidateAudience = false,
                        ValidIssuer = jwtSettings.Issuer
                    };
                });
        }

        public static IServiceCollection AddJwtAuthorization(this IServiceCollection serviceCollection)
        {
            //ToDo configure swagger to be able to send token/request header
            return serviceCollection.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(
                        JwtBearerDefaults.AuthenticationScheme,
                        JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();
            });
        }

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

        private static JwtSettings ToJwtSettings(Func<IConfigurationSection> configuration)
        {
            return new JwtSettings
            {
                BaseSecret = configuration()["BaseSecret"],
                Issuer = configuration()["Issuer"],
                ExpiryTimeInMinutes = int.Parse(configuration()["ExpiryTimeInMinutes"])
            };
        }
    }
}
