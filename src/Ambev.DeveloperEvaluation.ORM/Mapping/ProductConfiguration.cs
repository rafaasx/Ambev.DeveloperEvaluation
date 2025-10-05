using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Title).IsRequired();
        builder.HasIndex(p => p.Title);
        builder.Property(p => p.Price).HasColumnType("numeric(10,2)").IsRequired();

        builder.OwnsOne(p => p.Rating, rating =>
        {
            rating.Property(r => r.Rate)
                  .HasColumnName("RatingRate");

            rating.Property(r => r.Count)
                  .HasColumnName("RatingCount");
        });
    }
}
