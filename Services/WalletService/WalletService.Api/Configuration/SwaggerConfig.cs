using System.Reflection;
using Microsoft.OpenApi.Models;
namespace WalletService.Api.Configuration
{
    public static class SwaggerConfig
    {
        public static WebApplicationBuilder AddSwaggerConfiguration(this WebApplicationBuilder builder)
        {
           builder.Services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Tycoon - Android Workers",
                    Description = DESCRIPTION,

                });

            });
            return builder;
        }

        public static WebApplication UseSwaggerConfiguration(this WebApplication app)
        {

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });

            return app;
        }
         public const string DESCRIPTION = @"";
    }
}
