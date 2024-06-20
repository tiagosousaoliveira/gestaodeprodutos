using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Cafiguration
{
    public class ProdutoEntityTypeConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.CodigoProduto);

            builder.Property(p => p.DescricaoProduto).IsRequired();
            builder.Property(p => p.SituacaoProduto).IsRequired();
            builder.Property(p => p.DataFabricacao).IsRequired();
            builder.Property(p => p.DataValidade).IsRequired();
            builder.Property(p => p.CodigoFornecedor).IsRequired();
            builder.Property(p => p.DescricaoFornecedor).IsRequired();
            builder.Property(p => p.CnpjFornecedor).IsRequired();

            builder.Property(p => p.CodigoProduto)
                .ValueGeneratedOnAdd();
            builder.Property(p => p.CodigoProduto)
                  .UseIdentityColumn();
        }
    }
}
