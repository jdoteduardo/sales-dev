using SalesDev.API.EntityConfiguration;
using SalesDev.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesDev.API.Context
{
    public class SalesDevDbContext : DbContext 
    {
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Produto> Produtos { get; set; }
        public virtual DbSet<Venda> Vendas { get; set; }

        public SalesDevDbContext() { }

        public virtual void SetModifiedProduto(Produto produto)
        {
            Entry(produto).Property(e => e.Ativo).IsModified = false;
            Entry(produto).Property(e => e.Preco).IsModified = true;
        }

        public virtual void SetModifiedCliente(Cliente cliente)
        {
            Entry(cliente).Property(e => e.DataCadastro).IsModified = false;
            Entry(cliente).Property(e => e.Ativo).IsModified = false;
        }
        public SalesDevDbContext(DbContextOptions<SalesDevDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new ProdutoConfiguration());
            builder.ApplyConfiguration(new ClienteConfiguration());
            builder.ApplyConfiguration(new VendaConfiguration());
        }
    }
}
