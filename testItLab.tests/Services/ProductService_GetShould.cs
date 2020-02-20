using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testeItLab.domain.Interfaces;
using testeItLab.domain.Models;
using testeItLab.domain.Services;
using testItLab.infra.Data.Context;
using testItLab.infra.Data.Repositories;
using Xunit;

namespace testItLab.tests.Services
{
    public class ProductService_GetShould
    {
        private readonly ProductService _productService;

        public ProductService_GetShould()
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
                new DbContextOptionsBuilder<TestItLabDbContext>().UseInMemoryDatabase("get_test")
            .Options);
            productDbContextMock.CreateDbSetMock(x => x.Products, products);

            var productContentMock = new ProductRepository(productDbContextMock.Object);

            _productService = new ProductService(productContentMock);
        }

        [Fact]
        public async Task GetAsync_InputIs1_NotToThrow()
        {
            // Arrange and act
            var exception = await Record.ExceptionAsync(() => _productService.GetAsync(1));
            
            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public async Task GetAsync_InputIs2_Return()
        {
            // Arrange and act
            var actual = await _productService.GetAsync(2);
           
            // Assert
            Assert.Equal(2, actual.Id);
        }
    }
}
