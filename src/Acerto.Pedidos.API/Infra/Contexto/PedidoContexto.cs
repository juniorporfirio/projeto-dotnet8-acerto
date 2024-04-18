using System.Reflection;
using Acerto.Pedidos.API.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Acerto.Pedidos.API.Infra.Contexto
{
    public class PedidoContexto(DbContextOptions<PedidoContexto> options) : DbContext(options)
    {
         public DbSet<Pedido> Pedidos { get; set; }
         public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            _ = modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}