namespace Acerto.Pedidos.API.Infra.Http
{

    public record ProdutoHttp(Guid Id, string Nome, string Descricao, decimal Preco, bool Ativo);
}
