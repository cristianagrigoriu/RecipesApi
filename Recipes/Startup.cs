using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Recipes
{
    using System;
    using Domain;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.IdentityModel.Tokens;
    using Persistence;

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

            services.ConfigureSwagger();

            services.AddAutoMapper(typeof(Startup));

            services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));

            services.AddTransient<IRecipesRepository, RecipesCouchRepository>();

            services.AddTransient<IIngredientsRepository, IngredientsRepository>();

            services.AddTransient<ITokenProvider, JwtTokenProvider>();

            services.AddHttpContextAccessor();

            services.AddTransient(serviceProvider =>
            {
                var currentLanguage = serviceProvider.GetService<IHttpContextAccessor>().HttpContext.Request
                    .Headers["Language"];

                return new LanguageService(currentLanguage);
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            //based on parameters set here, should do token validation
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    //ValidateIssuer = true,
                    //ValidateAudience = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String("ERMN05OPLoDvbTTa/QkqLNMI7cPLguaRyHzyg7n5qNBVjQmtBhz4SzYh4NBVCXi3KJHlSXKP+oi2+bXr6CUYTR==")),
                    //ValidAudience = "http://localhost:6600",
                    ValidateAudience = false,
                    ValidIssuer = "http://localhost:6600"
                    //ValidateIssuerSigningKey = true
                    //ValidIssuer = jwtSettings.Issuer,
                    //ValidAudience = jwtSettings.Audience,
                    //IssuerSigningKey = jwtSettings.SecurityKey,
                };
            });

            //ToDo configure swagger to be able to send token/request header

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme,
                        JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.ConfigureExceptionHandler();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI
            (
                c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Recipes API version 1")
            );
        }
    }
}
