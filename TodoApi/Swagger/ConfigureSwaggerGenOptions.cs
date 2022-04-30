using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TodoApi.Swagger
{
    internal class ConfigureSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
    {
        public void Configure(SwaggerGenOptions options)
        {
            options.SwaggerDoc("v1", new OpenApiInfo {Title = "Todo API", Version = "v1"});

            foreach (var fileName in Directory.GetFiles(AppContext.BaseDirectory, "*.xml"))
            {
                options.IncludeXmlComments(fileName, includeControllerXmlComments: true);
            }
        }
    }
}