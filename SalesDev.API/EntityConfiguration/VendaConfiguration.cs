using SalesDev.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesDev.API.EntityConfiguration
{
    public class VendaConfiguration : IEntityTypeConfiguration<Venda>
    {
        public void Configure(EntityTypeBuilder<Venda> builder)
        {
            builder.HasKey(e => e.Id);

            builder
                .HasOne(i => i.Produto)
                .WithMany(e => e.Vendas)
                .HasForeignKey(i => i.IdProduto)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(i => i.Cliente)
                .WithMany(e => e.Vendas)
                .HasForeignKey(i => i.IdCliente)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
