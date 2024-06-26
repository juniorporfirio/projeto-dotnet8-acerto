using System.Text.Json.Serialization;
using Acerto.Pedidos.API.Apresentacao.Endpoints.Validacao;
using Acerto.Pedidos.API.Dominio.Entidades;
using FluentValidation;

namespace Acerto.Pedidos.API.Apresentacao
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApresentacao(this IServiceCollection services)
        {
             services.AddScoped<IValidator<Pedido>, PedidoValidator>();
            services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>{
                options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                });

            return services;
        }

         public static WebApplication AddEndpoints(this WebApplication webApplication)
        {
              if (webApplication.Environment.IsDevelopment())
            {
                webApplication.UseSwagger();
                webApplication.UseSwaggerUI(swagger => swagger.SwaggerEndpoint("/swagger/v1/swagger.json", "Acerto Pedidos API"));
            }
            
            webApplication.MapGroup("/pedidos").MapPedidoEndopoints().WithTags("Pedidos");

            return webApplication;
        }
    }
}