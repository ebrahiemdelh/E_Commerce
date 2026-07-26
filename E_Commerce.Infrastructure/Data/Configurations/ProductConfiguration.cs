



namespace E_Commerce.Infrastructure.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Description)
                .HasMaxLength(500);

            builder.Property(p => p.PictureUrl)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.HasOne(p => p.ProductBrand)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.BrandId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.ProductType)
                .WithMany(t => t.Products)
                .HasForeignKey(p => p.TypeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
