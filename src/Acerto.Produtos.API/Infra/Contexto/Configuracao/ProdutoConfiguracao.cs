using Acerto.Produtos.API.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Acerto.Produtos.API.Infra.Contexto.Configuracao
{
    public class ProdutoConfiguracao: IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("tb_produtos");

            builder.HasKey(x => x.Id);
    
            builder.Property(x => x.Nome)
            .HasColumnName("nome_produto")
            .HasMaxLength(200)
            .IsRequired();

            builder.Property(x=>x.Descricao)
            .HasColumnName("descricao")
            .HasMaxLength(1000)
            .IsRequired(false);

            builder.Property(x => x.Preco)
            .HasColumnName("preco")
            .IsRequired();

            builder.Property(x=>x.Ativo)
            .HasColumnName("ativo")
            .IsRequired();

        }
    }
}