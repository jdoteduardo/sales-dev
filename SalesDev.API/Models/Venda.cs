using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SalesDev.API.Models
{
    public class Venda
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataVenda { get; set; }
        public decimal Total
        {
            get
            {
                var preco = this.Produto.Preco;
                var quantidade = this.Quantidade;
                var total = preco * quantidade;
                return total;
            }
            
        }

        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; }
        public int IdProduto { get; set; }
        public Produto Produto { get; set; }
    }
}
