using System.Reflection;
using Acerto.Pedidos.API.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Acerto.Pedidos.API.Infra.Contexto
{
    public class PedidoContexto: DbContext
    {
        public PedidoContexto()
        {
            
        }
        public PedidoContexto(DbContextOptions<PedidoContexto> options): base(options)
        {
            
        }
         public DbSet<Pedido> Pedidos { get; set; }
         public DbSet<Produto> Produtos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("Server=localhost;Port=3306;Database=acertoPedidos;Uid=root;Pwd=password;");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            _ = modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}