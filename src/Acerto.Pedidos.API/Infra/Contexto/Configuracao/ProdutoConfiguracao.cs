using Acerto.Pedidos.API.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Acerto.Pedidos.API.Infra.Contexto.Configuracao
{
    public class ProdutoConfiguracao : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(x => new{ x.Id, x.PedidoId});
            builder.ToTable("tb_produto");
            
            builder.Property(x => x.Preco)
           .HasColumnName("preco")
           .IsRequired();

            builder.Property(x => x.Quantidade)
           .HasColumnName("quantidade")
           .IsRequired();

            builder.Property(x => x.Nome)
           .HasColumnName("nome")
           .IsRequired();

           builder.Property(x => x.PedidoId)
           .HasColumnName("pedido_id")
           .IsRequired();

        }

    }
}