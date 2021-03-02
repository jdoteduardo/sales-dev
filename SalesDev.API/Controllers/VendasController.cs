using SalesDev.API.Context;
using SalesDev.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SalesDev.API.Controllers
{
    [Route("api/vendas")]
    public class VendasController : ControllerBase
    {
        private readonly SalesDevDbContext _dbContext;
        public VendasController(SalesDevDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // api/clientes/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Venda>> ObterVenda(int id)
        {
            var venda = await _dbContext
                .Vendas
                .Include(e => e.Cliente)
                .Include(e => e.Produto)
                .SingleOrDefaultAsync(e => e.Id == id);

            if (venda == null)
            {
                return NotFound();
            }

            return venda;
        }

        /// <summary>
        /// Adiciona uma venda desde que o cliente e produto esteja ativo
        /// </summary>
        // api/clientes
        [HttpPost("clientes/{idCliente}/produtos/{idProduto}/vender")]
        public async Task<ActionResult<Venda>> Cadastrar(int idCliente, int idProduto, [FromBody] Venda venda)
        {
            var cliente = _dbContext.Clientes.SingleOrDefault(e => e.Id == idCliente);
            var produto = _dbContext.Produtos.SingleOrDefault(e => e.Id == idProduto);

            if (cliente.Ativo && produto.Ativo)
            {
                _dbContext.Vendas.Add(venda);
                await _dbContext.SaveChangesAsync();

                return venda;
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
