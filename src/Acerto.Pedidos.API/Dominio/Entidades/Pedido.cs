using Acerto.Pedidos.API.Dominio.Enum;

namespace Acerto.Pedidos.API.Dominio.Entidades;

public class Pedido
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime DataCriacao { get; set;}
    public ICollection<Produto> Produtos { get;set; } = [];
    public decimal ValorTotal { get; set; } = 0;
    public PedidoStatus Status {get;set;} = PedidoStatus.Novo;
    public void AdicionaProduto(Produto produto) => Produtos.Add(produto);
}