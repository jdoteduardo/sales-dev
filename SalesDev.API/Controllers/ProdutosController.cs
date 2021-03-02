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
    [Route("api/produtos")]
    public class ProdutosController : ControllerBase
    {
        private readonly SalesDevDbContext _dbContext;
        public ProdutosController(SalesDevDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // api/produtos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> ObterProdutos()
        {
            var produtos = await _dbContext.Produtos.ToListAsync();

            if (produtos == null)
            {
                return NotFound();
            }

            return produtos;
        }

        // api/produtos/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> ObterProduto(int id)
        {
            var produto = await _dbContext.Produtos.FindAsync(id);

            if (produto == null)
            {
                return NotFound();
            }

            return produto;
        }

        // api/produtos
        [HttpPost]
        public async Task<ActionResult<Produto>> Cadastrar([FromBody] Produto produto)
        {
            _dbContext.Produtos.Add(produto);

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        // api/produtos/1
        [HttpPut("{id}")]
        public async Task<ActionResult> Atualizar(int id, [FromBody] Produto produto)
        {
            if (id != produto.Id)
            {
                return BadRequest();
            }

            _dbContext.SetModifiedProduto(produto);

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Torna o produto não ativo
        /// </summary>
        // api/produtos/1
        [HttpDelete("{id}")]
        public async Task<ActionResult<Produto>> Cancelar(int id)
        {
            var produto = _dbContext.Produtos.SingleOrDefault(e => e.Id == id);

            if (produto == null)
            {
                return NotFound();
            }

            produto.Ativo = false;

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
