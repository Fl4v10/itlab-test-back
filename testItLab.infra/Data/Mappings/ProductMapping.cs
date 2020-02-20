using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using testeItLab.domain.Models;
using testeItLab.domain.Models.Enums;

namespace testItLab.infra.Data.Mappings
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd();

            builder.Property(c => c.Name).IsRequired();

            builder.Property(c => c.Value).IsRequired();

            builder.Property(c => c.RegisterAt)
            .HasDefaultValueSql("GetUtcDate()");

            builder.Property(c => c.Type)
                .HasDefaultValue(EProductType.animal);
        }
    }
}
