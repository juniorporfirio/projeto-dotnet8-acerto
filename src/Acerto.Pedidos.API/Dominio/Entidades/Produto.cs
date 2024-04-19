namespace Acerto.Pedidos.API.Dominio.Entidades;
public class Produto{
    public Guid Id {get;set;} = Guid.NewGuid();
    public string Nome {get;set;}= string.Empty;
    public int Quantidade {get;set;} = 1;
    public decimal Preco {get;set;} = decimal.Zero;

    public Guid PedidoId {get;set;} 
    public Pedido Pedido { get; set; } = new();

    }
