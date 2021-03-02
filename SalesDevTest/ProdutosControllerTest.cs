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
    public class ProdutosControllerTest
    {
        private readonly Mock<DbSet<Produto>> _mockSet;
        private readonly Mock<SalesDevDbContext> _mockContext;
        private readonly Produto _produto;


        public ProdutosControllerTest()
        {
            _mockSet = new Mock<DbSet<Produto>>();
            _mockContext = new Mock<SalesDevDbContext>();
            _produto = new Produto { 
                Id = 1, 
                Descricao = "Notebook Gamer", 
                Preco = 5000.99M, 
                Ativo = true 
            };


            _mockContext.Setup(m => m.Produtos).Returns(_mockSet.Object);

            // Get One
            _mockContext.Setup(m => m.Produtos.FindAsync(1))
                .ReturnsAsync(_produto);

            // Put
            _mockContext.Setup(m => m.SetModifiedProduto(_produto));

            _mockContext.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);
        }

        [Fact]
        public async Task Obter_Produto()
        {
            var service = new ProdutosController(_mockContext.Object);

            await service.ObterProduto(1);

            _mockSet.Verify(m => m.FindAsync(1),
                Times.Once());
        }

        [Fact]
        public async Task Atualizar_Produto()
        {
            var service = new ProdutosController(_mockContext.Object);

            await service.Atualizar(1, _produto);

            _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()),
                Times.Once());
        }

        [Fact]
        public async Task Adicionar_Produto()
        {
            var service = new ProdutosController(_mockContext.Object);
            await service.Cadastrar(_produto);

            _mockSet.Verify(x => x.Add(_produto), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()),
                Times.Once());
        }
    }
}
