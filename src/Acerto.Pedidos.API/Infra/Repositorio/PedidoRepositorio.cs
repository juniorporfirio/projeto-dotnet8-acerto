using Acerto.Pedidos.API.Dominio.Entidades;
using Acerto.Pedidos.API.Dominio.Interfaces.Repositorio;
using Acerto.Pedidos.API.Infra.Contexto;
using Microsoft.EntityFrameworkCore;

namespace Acerto.Pedidos.API.Infra.Repositorio
{
    public class PedidoRepositorio : IPedidoRepositorio
    {
        private readonly PedidoContexto _contexto;

        public PedidoRepositorio(PedidoContexto contexto)
        {
            _contexto = contexto;
        }
        public async Task<IEnumerable<Pedido>> Todos() => await _contexto.Pedidos.Include(i=>i.Produtos).AsNoTracking().ToListAsync();

        public async  Task Novo(Pedido pedido)
        {
           _contexto.Pedidos.Add(pedido);
           await _contexto.SaveChangesAsync();
        }

        public async Task<Pedido> PorId(Guid id) => await _contexto.Pedidos.FindAsync(id) ?? throw new ArgumentException("Pedido não encontrado");
    }
}