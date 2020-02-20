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
    public class ProductService_UpdateShould
    {
        private readonly ProductService _productService;
        
        // TODO: COLOCAR NUMA CLASSE BASE
        public ProductService_UpdateShould()
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
                new DbContextOptionsBuilder<TestItLabDbContext>().UseInMemoryDatabase("update_test")
            .Options);
            productDbContextMock.CreateDbSetMock(x => x.Products, products);

            var productContentMock = new ProductRepository(productDbContextMock.Object);

            _productService = new ProductService(productContentMock);
        }

        [Fact]
        public async Task UpdateAsync_InputEntity_NotToThrow()
        {
            // Arrange
            var productMock = new Product
            {
                Id = 1,
                Name = "FooBar",
                Value = 13m,
                Type = testeItLab.domain.Models.Enums.EProductType.car,
                Sex = true
            };

            // Act
            var exception = await Record.ExceptionAsync(() => _productService.UpdateAsync(1, productMock));

            // Assert
            Assert.Null(exception);
        }
    }
}
