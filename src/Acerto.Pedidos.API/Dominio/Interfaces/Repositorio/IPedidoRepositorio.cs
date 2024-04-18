using Acerto.Pedidos.API.Dominio.Entidades;

namespace Acerto.Pedidos.API.Dominio.Interfaces.Repositorio
{
    public interface IPedidoRepositorio
    {
        public Task Novo(Pedido pedido);
        public Task<Pedido> PorId(Guid id);
        public Task<IEnumerable<Pedido>> Todos();

    }
}