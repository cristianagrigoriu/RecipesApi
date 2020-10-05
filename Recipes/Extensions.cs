namespace Recipes
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;

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
                    //ToDo make path absolute
                    c.IncludeXmlComments(@"C:\Users\cgrigori\source\repos\RecipesApi\Recipes\Recipes.xml");
                    //ToDo make enums aappear as text
                    c.DescribeAllEnumsAsStrings();
                }
            );
        }
    }
}
