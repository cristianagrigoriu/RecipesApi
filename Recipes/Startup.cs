using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Recipes
{
    using System.Runtime.InteropServices.ComTypes;
    using Data;
    using Domain;
    using Microsoft.AspNetCore.Http;
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

            services.AddHttpContextAccessor();

            services.AddTransient(serviceProvider =>
            {
                var currentLanguage = serviceProvider.GetService<IHttpContextAccessor>().HttpContext.Request.Headers["Language"];

                return new LanguageService(currentLanguage);
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
