using Acerto.Produtos.API.Dominio.Interfaces.Infra.Repositorio;
using Acerto.Produtos.API.Dominio.Entidades;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Acerto.Produtos.API.Apresentacao.Endpoints
{
    public static class ProdutosEndpoint
    {
        public static RouteGroupBuilder  MapProdutoEndpoints(this RouteGroupBuilder  group)
        {
            group.MapGet("/", Todos).RequireAuthorization();
            group.MapGet("/{id}", PorId).RequireAuthorization();
            group.MapPost("/", Novo).RequireAuthorization();
            group.MapPut("/", Atualizar).RequireAuthorization();
            group.MapDelete("/{id}", Remover).RequireAuthorization();

            return group;
        }
        public static async Task<Ok<IEnumerable<Produto>>> Todos(IProdutosRepository repository)
        {
            var produto = await repository.Todos();
            return TypedResults.Ok(produto);
        }
        public static async Task<Results<Ok<Produto>,NotFound>> PorId(Guid id,IProdutosRepository repository)
        {
            var produto = await repository.PorId(id);

            if (produto is null)
                return TypedResults.NotFound();

            return TypedResults.Ok(produto);
        }
        public static async Task<Results<Created<Produto>, ValidationProblem>> Novo(Produto produto, IValidator<Produto> validator,  IProdutosRepository repository)
        {
            var valid = validator.Validate(produto);
            if (valid.IsValid)
            {
                await repository.AdicionarNovo(produto);
                return TypedResults.Created($"/produtos/{produto.Id}", produto);
            }

            return TypedResults.ValidationProblem(valid.ToDictionary());
        }
        public static async Task<Results<Created<Produto>,ValidationProblem, NotFound>> Atualizar(Produto produto, IValidator<Produto> validator, IProdutosRepository repository)
        {
            var valid = validator.Validate(produto);

            if (valid.IsValid)
            {
                var produtoBd = await repository.PorId(produto.Id);

                if (produtoBd is null) return TypedResults.NotFound();

                await repository.Atualizar(produto);

                return TypedResults.Created($"/produtos/{produto.Id}", produto);
            }
            return TypedResults.ValidationProblem(valid.ToDictionary());
        }
        public static async Task<Results<NoContent,NotFound>> Remover(Guid Id,  IProdutosRepository repository)
        {
            var produtoBd = await repository.PorId(Id);

            if (produtoBd is null) return TypedResults.NotFound();

            await repository.Remove(Id);
            return TypedResults.NoContent();
        }
    }
}