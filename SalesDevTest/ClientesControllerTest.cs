using SalesDev.API.Context;
using SalesDev.API.Controllers;
using SalesDev.API.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SalesDevTest
{
    public class ClientesControllerTest
    {
        private readonly Mock<DbSet<Cliente>> _mockSet;
        private readonly Mock<SalesDevDbContext> _mockContext;
        private readonly Cliente _cliente;

        public ClientesControllerTest()
        {
            _mockSet = new Mock<DbSet<Cliente>>();
            _mockContext = new Mock<SalesDevDbContext>();
            _cliente = new Cliente { Id = 1, NomeCompleto = "Johnny Cash", DataNascimento = new DateTime(1948 - 01 - 01), Email = "eduardo@gmail.com", DataCadastro = new DateTime(2021 - 03 - 01), Ativo = true  };

            _mockContext.Setup(m => m.Clientes).Returns(_mockSet.Object);

            _mockContext.Setup(m => m.Clientes.FindAsync(1))
                .ReturnsAsync(_cliente);


            _mockContext.Setup(m => m.SetModifiedCliente(_cliente));

            _mockContext.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);
        }

        [Fact]
        public async Task Obter_Cliente()
        {
            var service = new ClientesController(_mockContext.Object);

            await service.ObterCliente(1);

            _mockSet.Verify(m => m.FindAsync(1),
                Times.Once());
        }

        [Fact]
        public async Task Atualizar_Cliente()
        {
            var service = new ClientesController(_mockContext.Object);

            await service.Atualizar(1, _cliente);

            _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()),
                Times.Once());
        }

        [Fact]
        public async Task Adicionar_Cliente()
        {
            var service = new ClientesController(_mockContext.Object);
            await service.Cadastrar(_cliente);

            _mockSet.Verify(x => x.Add(_cliente), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()),
                Times.Once());
        }
    }
}
