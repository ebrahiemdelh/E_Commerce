
namespace E_Commerce.Infrastructure.Data.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasMany(o => o.Items)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.DeliveryMethod)
                .WithMany()
                .HasForeignKey(o => o.DeliveryMethodId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.OwnsOne(o => o.OrderAddress);

            builder.Property(o => o.PaymentStatus)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(o => o.UserEmail)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(o => o.SubTotal)
                .HasColumnType("decimal(10,2)")
                .IsRequired();
        }
    }
}
