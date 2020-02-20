using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Threading.Tasks;
using testeItLab.domain.Exceptions;
using testeItLab.domain.Models;
using testeItLab.domain.Services;
using testItLab.infra.Data.Context;
using testItLab.infra.Data.Repositories;
using Xunit;

namespace testItLab.tests.Services
{
    public class ProductService_DeleteShould
    {
        private readonly TestItLabDbContext _productDbContextMock;
        private readonly ProductService _productService;

        public ProductService_DeleteShould()
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
                new DbContextOptionsBuilder<TestItLabDbContext>().UseInMemoryDatabase("delete_test")
            .Options);
            productDbContextMock.CreateDbSetMock(x => x.Products, products);

            var productContentMock = new ProductRepository(productDbContextMock.Object);

            _productDbContextMock = productDbContextMock.Object;

            _productService = new ProductService(productContentMock);
        }

        [Fact]
        public async Task DeleteAsync_InputIs1_NotToThrow()
        {
            // Arrange and Act
            var exception = await Record.ExceptionAsync(() => _productService.DeleteAsync(1));
            
            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public async Task DeleteAsync_InputIs13_ToThrowNotFound()
        {
            //Arrange and Act
            var ex = await Assert.ThrowsAsync<NotFoundEntityException<Product>>(() => _productService.DeleteAsync(13));

            // Assert
            Assert.Equal("Entity not found!", ex.Message);
        }

        [Fact]
        public async Task DeleteAsync_InputIs2_Removes()
        {
            // Arrange
            await _productService.DeleteAsync(2);

            // Act
            var actual = await _productDbContextMock.Products.CountAsync();
            
            // Assert
            Assert.Equal(1, actual);
        }
    }
}
