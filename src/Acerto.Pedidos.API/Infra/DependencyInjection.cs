using System.Text;
using Acerto.Pedidos.API.Dominio.Entidades;
using Acerto.Pedidos.API.Dominio.Interfaces.Fila;
using Acerto.Pedidos.API.Dominio.Interfaces.Repositorio;
using Acerto.Pedidos.API.Infra.Contexto;
using Acerto.Pedidos.API.Infra.Fila;
using Acerto.Pedidos.API.Infra.Http.Interfaces;
using Acerto.Pedidos.API.Infra.Repositorio;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Refit;

namespace Acerto.Pedidos.API.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuracao)
        {

            var connectionString = configuracao.GetConnectionString("AcertoPedidos")!.ToString();

            services.AddDbContext<PedidoContexto>(options => options.UseMySQL(connectionString));

            services.AddScoped<IPedidoRepositorio, PedidoRepositorio>();
            services.AddScoped<IFilaIntegracao, FilaIntegracaoRabbit>();

          

            var oauth = RestService.For<IAutenticacaoRest>("http://localhost:5000");

            services.AddRefitClient<IProdutoRest>(
                   new RefitSettings
                   {
                       AuthorizationHeaderValueGetter = async (_, _) =>
                       {
                           var token = await oauth.Autenticar(new Http.UsuarioHttp { NomeUsuario = "acerto", Senha = "acerto_pass", Papel = "admin" });
                           return token.Token;
                       }
                   })
                       .ConfigureHttpClient(config =>
                       {
                           config.BaseAddress = new Uri("http://localhost:5000");
                       });

            return services;
        }
    }
}