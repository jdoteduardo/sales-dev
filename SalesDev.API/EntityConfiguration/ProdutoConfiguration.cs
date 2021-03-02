using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesDev.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesDev.API.EntityConfiguration
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
            public void Configure(EntityTypeBuilder<Produto> builder)
            {
                builder.HasKey(e => e.Id);
                builder.Property(p => p.Descricao).HasColumnType("varchar(200)").IsRequired();
                builder.Property(p => p.Preco).HasPrecision(10, 2).IsRequired();
                builder.Property(p => p.Ativo).IsRequired();
            }
    }
}
