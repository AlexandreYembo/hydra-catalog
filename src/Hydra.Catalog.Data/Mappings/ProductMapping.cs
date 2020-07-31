using System;
using Hydra.Catalog.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hydra.Catalog.Data.Mappings
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(250");
            
            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(250");
            
            builder.Property(c => c.Description)
            .IsRequired()
            .HasColumnType("varchar(500)");

            builder.Property(c => c.Image)
            .IsRequired()
            .HasColumnType("varchar(250");

            //transform the attribute inside Dimension class in columns for Product table.
            builder.OwnsOne(c => c.Dimensions, cm => 
            {
                cm.Property(c => c.Height)
                    .HasColumnName("Height")
                    .HasColumnType("int");

                cm.Property(c => c.Length)
                    .HasColumnName("Length")
                    .HasColumnType("int");
                
                cm.Property(c => c.Width)
                    .HasColumnName("Width")
                    .HasColumnType("int");
            });

            builder.ToTable("Products");
        }
    }
}
