using System.Text;
using Acerto.Produtos.API.Apresentacao.Endpoints.Autenticacao;
using Acerto.Produtos.API.Dominio.Configuracoes;
using Acerto.Produtos.API.Dominio.Interfaces.Infra.Autenticacao;
using Acerto.Produtos.API.Dominio.Interfaces.Infra.Repositorio;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Acerto.Produtos.API.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfra(this IServiceCollection services)
        {
            var connectionString = Environment.GetEnvironmentVariable("MYSQL_CONNECTION")!;

            services.AddDbContext<AcertoContext>(options => options.UseInMemoryDatabase("Acerto"));
            services.AddTransient<IProdutosRepository, ProdutoRepository>();
            services.AddTransient<ITokenServico, TokenServico>();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuracao.Chave)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddAuthorization();
            return services;

        }
    }
}