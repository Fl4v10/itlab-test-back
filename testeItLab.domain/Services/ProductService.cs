using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using testeItLab.domain.Exceptions;
using testeItLab.domain.Interfaces;
using testeItLab.domain.Interfaces.Services;
using testeItLab.domain.Models;
using testeItLab.domain.Validators;

namespace testeItLab.domain.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> CreateAsync(Product entity)
        {
            // TODO: TRANSFORMAR NUM MÉTODO DE EXTENSÃO. e.g. Entity.Validate(rules);
            ValidateEntity(entity);

            // TDO: REMOVER QUANDO ESTIVER USANDO BANCO REAL
            entity.RegisterAt = DateTime.UtcNow;

            await _productRepository.AddAsync(entity);

            return entity;
        }

        private static void ValidateEntity(Product entity)
        {
            var validator = new ProductValidator();
            var result = validator.Validate(entity);

            if (!result.IsValid)
            {
                var failures = "";
                foreach (var failure in result.Errors)
                {
                    failures = string.Concat(failures, $"\nProperty {failure.PropertyName} failed validation. Error was: {failure.ErrorMessage}.");
                }

                throw new Exception(failures);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var product = await GetAsync(id);
            if (product == null)
                throw new NotFoundEntityException<Product>();

            await _productRepository.DeleteAsync(product);
        }

        public Task<Product> GetAsync(int id) => GetList().FirstOrDefaultAsync(f => f.Id == id);

        public IQueryable<Product> GetList() => _productRepository.Query();

        public async Task<Product> UpdateAsync(int id, Product updateEntity)
        {
            var produt = await GetAsync(id);
            if (produt == null)
                throw new NotFoundEntityException<Product>();

            produt.UpdateData(updateEntity);
            await _productRepository.UpdateAsync(produt);
            return updateEntity;
        }
    }
}
