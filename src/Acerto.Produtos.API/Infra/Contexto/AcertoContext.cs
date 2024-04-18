
using System.Reflection;
using Acerto.Produtos.API.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Acerto.Produtos.API.Infra
{
    public class AcertoContext(DbContextOptions<AcertoContext> options) : DbContext(options)
    {
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            _ = modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}