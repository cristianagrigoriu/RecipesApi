using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Recipes
{
    using Domain;
    using Microsoft.Extensions.DependencyInjection;
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

            services
                .ConfigureSwagger()
                .AddAutoMapper(typeof(Startup))
                .Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"))
                .Configure<JwtSettings>(Configuration.GetSection("JwtSettings"))
                .AddTransient<IRecipesRepository, RecipesCouchRepository>()
                .AddTransient<IIngredientsRepository, IngredientsRepository>()
                .AddTransient<IUserRepository, UserCouchRepository>()
                .AddTransient<ITokenProvider, JwtTokenProvider>()
                .AddTransient<IHashGenerator, Sha256HashGenerator>()
                .AddHttpContextAccessor()
                .AddTransient(serviceProvider =>
                {
                    var currentLanguage = serviceProvider.GetService<IHttpContextAccessor>().HttpContext.Request
                        .Headers["Language"];

                    return new LanguageService(currentLanguage);
                });

            //ToDo check auth with api key, too
            services.AddJwtAuthentication(() => this.Configuration.GetSection("JwtSettings"));
            services.AddJwtAuthorization();
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
