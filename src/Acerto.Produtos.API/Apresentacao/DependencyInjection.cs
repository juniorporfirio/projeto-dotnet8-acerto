using Acerto.Produtos.API.Apresentacao.Endpoints;
using Acerto.Produtos.API.Apresentacao.Endpoints.Autenticacao;
using Acerto.Produtos.API.Apresentacao.Endpoints.Autenticacao.Validacao;
using Acerto.Produtos.API.Apresentacao.Endpoints.Validation;
using Acerto.Produtos.API.Dominio.Entidades;
using FluentValidation;

namespace Acerto.Produtos.API.Apresentacao
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApresentacao(this IServiceCollection services)
        {
            services.AddScoped<IValidator<Produto>, ProdutoValidator>();
            services.AddScoped<IValidator<Usuario>, AutenticacaoValidator>();

            return services;
        }
        public static WebApplication AddEndpoints(this WebApplication webApplication)
        {

            if (webApplication.Environment.IsDevelopment())
            {
                webApplication.UseSwagger();
                webApplication.UseSwaggerUI(swagger => swagger.SwaggerEndpoint("/swagger/v1/swagger.json", "Acerto Produtos API"));
            }
            
            webApplication.MapGroup("/produtos").MapProdutoEndpoints().WithTags("Produtos");
            webApplication.MapGroup("/autenticacao").MapAutenticacaoEndopoints().WithTags("Autenticacao");

            return webApplication;

        }

    }
}