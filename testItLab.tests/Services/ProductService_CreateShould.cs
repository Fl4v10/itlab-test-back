using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Threading.Tasks;
using testeItLab.domain.Models;
using testeItLab.domain.Services;
using testItLab.infra.Data.Context;
using testItLab.infra.Data.Repositories;
using Xunit;

namespace testItLab.tests.Services
{
    public class ProductService_CreateShould
    {
        private readonly ProductService _productService;

        public ProductService_CreateShould()
        {
            var productMock = Mock.Of<Product>();
            productMock.Id = 1;

            var product1Mock = Mock.Of<Product>();
            product1Mock.Id = 2;

            var products = new [] {
                productMock,
                product1Mock
            };

            var productDbContextMock = new DbContextMock<TestItLabDbContext>(
                new DbContextOptionsBuilder<TestItLabDbContext>().UseInMemoryDatabase("create_test")
            .Options);
            productDbContextMock.CreateDbSetMock(x => x.Products, products);

            var productContentMock = new ProductRepository(productDbContextMock.Object);

            _productService = new ProductService(productContentMock);
        }

        // TODO: ESCREVER MAIS CASOS DE TESTE
        [Fact]
        public async Task CreateAsync_InputMock_NotToThrow()
        {
            var product = new Product()
            {
                Name = "Go",
                Value = 13m,
                Type = testeItLab.domain.Models.Enums.EProductType.animal,
                Sex = true,
            };

            var exception = await Record.ExceptionAsync(() => _productService.CreateAsync(product));
            Assert.Null(exception);
        }
    }
}
