
using Acerto.Pedidos.API.Dominio.Entidades;
using Acerto.Pedidos.API.Dominio.Interfaces.Fila;
using Acerto.Pedidos.API.Dominio.Interfaces.Repositorio;
using Acerto.Pedidos.API.Infra.Http.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Acerto.Pedidos.API.Apresentacao
{
    public static class PedidoEndpoint
    {
         public static RouteGroupBuilder MapPedidoEndopoints(this RouteGroupBuilder router)
        {
            router.MapPost("/", Novo).Produces(200);
            router.MapGet("/", Todos).Produces(200);
            router.MapGet("/{id}", PorId).Produces(200);

            return router;
        }

        public static async Task<Results<Created<Pedido>, ValidationProblem>> Novo(
            Pedido pedido, 
            IValidator<Pedido> validator, 
            IProdutoRest produtoRest, 
            IPedidoRepositorio repositorio,
            IFilaIntegracao filaIntegracao)
        {
            var rules = validator.Validate(pedido);

            if(!rules.IsValid) return TypedResults.ValidationProblem(rules.ToDictionary());

            foreach(var produto in pedido.Produtos)
            {
                //TODO: Implementar cache para chamadas na API
                var dadosProduto = await produtoRest.Get(produto.Id);
                produto.Preco = dadosProduto.Preco;
                produto.Nome = dadosProduto.Nome;

            }
            pedido.ValorTotal = pedido.Produtos.Sum(x=>x.Quantidade * x.Preco);

            await repositorio.Novo(pedido);

            filaIntegracao.EnviarMensagem(pedido);

            return TypedResults.Created($"/pedidos/{pedido.Id}", pedido);
        }

         public static async Task<Results<Ok<Pedido>, NotFound>> PorId(Guid Id, IPedidoRepositorio repositorio)
        {
        
            var pedido = await repositorio.PorId(Id);
            
            if(pedido is null) return TypedResults.NotFound();

            return TypedResults.Ok(pedido);
        }

           public static async Task<Results<Ok<IEnumerable<Pedido>>, NotFound>> Todos(IPedidoRepositorio repositorio)
        {
        
            var pedido = await repositorio.Todos();
            
            if(pedido is null) return TypedResults.NotFound();

            return TypedResults.Ok(pedido);
        }
    }
}