using Microsoft.EntityFrameworkCore;
using testeItLab.domain.Models;
using testItLab.infra.Data.Mappings;

namespace testItLab.infra.Data.Context
{
    public class TestItLabDbContext : DbContext
    {
        public DbContext Instance => this;

        public virtual DbSet<Product> Products { get; set; }

        public TestItLabDbContext(DbContextOptions<TestItLabDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductMapping());
        }
    }
}
