using Acerto.Produtos.API.Dominio.Configuracoes;
using Acerto.Produtos.API.Dominio.Entidades;
using Acerto.Produtos.API.Dominio.Interfaces.Infra.Autenticacao;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Acerto.Produtos.API.Apresentacao.Endpoints.Autenticacao
{
    //TODO: Ajustar Autenticação para testes unitários e realizar os testes
    //TODO: Alterar a classe para utilizar router ao invés de Webapplications

    public static class AutenticacaoEndpoint
    {
        public static RouteGroupBuilder MapAutenticacaoEndopoints(this RouteGroupBuilder router)
        {
            router.MapPost("/login", Autenticar).AllowAnonymous().Produces(200);

            return router;
        }

        public static Results<UnauthorizedHttpResult, Ok<BearerToken>, ValidationProblem> Autenticar(Usuario usuario, IValidator<Usuario> validator, ITokenServico tokenServico)
        {
            var valido = validator.Validate(usuario);

            if (!valido.IsValid)
                return TypedResults.ValidationProblem(valido.ToDictionary());

            if (usuario.NomeUsuario == Configuracao.Usuario && usuario.Senha == Configuracao.Senha)
            {
                var token = tokenServico.GerarToken(usuario);

                var bearer = new BearerToken { NomeUsuario = usuario.NomeUsuario, Token = token };

                return TypedResults.Ok(bearer);
            }

            return TypedResults.Unauthorized();
        }

    }
}