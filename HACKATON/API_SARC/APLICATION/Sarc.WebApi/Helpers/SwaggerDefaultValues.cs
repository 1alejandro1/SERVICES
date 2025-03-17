using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Sarc.WebApi.Helpers
{
    public class SwaggerDefaultValues : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var apiDescription = context.ApiDescription;
            operation.Deprecated |= apiDescription.IsDeprecated();

            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            foreach (var parameter in operation.Parameters)
            {
                var description = apiDescription.ParameterDescriptions.First(
                    pd => pd.Name == parameter.Name);

                parameter.Description ??= description.ModelMetadata.Description;

                if (parameter.Schema.Default == null && description.DefaultValue != null)
                    parameter.Schema.Default = new OpenApiString(description.DefaultValue.ToString());

                parameter.Required |= description.IsRequired;
            }

            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = "CorrelationId",
                In = ParameterLocation.Header,
                Description = "Identificador unico de la peticion",
                Required = true
            });

            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = "Channel",
                In = ParameterLocation.Header,
                Description = "channel generado por el Api de Autorización de Arquitectura",
                Required = true
            });

            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = "PublicToken",
                In = ParameterLocation.Header,
                Description = "PublicToken generado por el Api de Autorización de Arquitectura",
                Required = true
            });

            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = "AppUserId",
                In = ParameterLocation.Header,
                Description = "AppUserId generado por el Api de Autorización de Arquitectura",
                Required = true
            });
        }
    }
}
