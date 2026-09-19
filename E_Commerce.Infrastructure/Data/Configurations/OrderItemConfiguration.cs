

namespace E_Commerce.Infrastructure.Data.Configurations
{
    internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(oi => oi.ProductName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(oi => oi.PictureUrl)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(oi => oi.Price)
                .HasColumnType("decimal(10,2)")
                .IsRequired();
        }
    }
}
