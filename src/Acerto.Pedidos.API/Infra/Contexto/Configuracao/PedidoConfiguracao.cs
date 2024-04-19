using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acerto.Pedidos.API.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Acerto.Pedidos.API.Infra.Contexto.Configuracao
{
    public class PedidoConfiguracao : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("tb_pedido");
            builder.HasKey(x => x.Id);

                      
            builder.Property(x => x.DataCriacao)
           .HasColumnName("data_criacao")
           .IsRequired();

              builder.Property(x => x.Status)
           .HasColumnName("pedido_status")
           .IsRequired();

            builder.Property(x => x.ValorTotal)
          .HasColumnName("valor_total")
          .IsRequired();

           builder.HasMany(c => c.Produtos)
            .WithOne(e => e.Pedido)
             .IsRequired();



        }
    }
}