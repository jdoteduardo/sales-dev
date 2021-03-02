using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using SalesDev.API.Models;
using System.Net;
using SalesDev.API.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace SalesDev.API.Controllers
{
    [Route("api/clientes")]
    public class ClientesController : ControllerBase
    {
        private readonly SalesDevDbContext _dbContext;
        public ClientesController(SalesDevDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // api/clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> ObterClientes()
        {
            var clientes = await _dbContext.Clientes.ToListAsync();

            if (clientes == null)
            {
                return NotFound();
            }

            return clientes;
        }

        // api/clientes/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> ObterCliente(int id)
        {
            var cliente = await _dbContext.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        // api/clientes
        [HttpPost]
        public async Task<ActionResult<Cliente>> Cadastrar([FromBody] Cliente cliente)
        {
            _dbContext.Clientes.Add(cliente);

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        // api/clientes/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return BadRequest();
            }

            _dbContext.SetModifiedCliente(cliente);

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Torna o cliente não ativo
        /// </summary>
        // api/clientes/1
        [HttpDelete("{id}")]
        public IActionResult Cancelar(int id)
        {
            var cliente = _dbContext.Clientes.SingleOrDefault(e => e.Id == id);

            if (cliente == null)
            {
                return NotFound();
            }

            cliente.Ativo = false;

            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}
