using testeItLab.domain.Interfaces;
using testeItLab.domain.Models;
using testItLab.infra.Data.Context;

namespace testItLab.infra.Data.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(TestItLabDbContext context) : base(context)
        {
        }
    }
}
