namespace E_Commerce.Infrastructure.Data.Configurations
{
    internal class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(dm => dm.ShortName)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(dm => dm.Description)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(dm => dm.DeliveryTime)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(dm => dm.Price)
                .IsRequired()
                .HasColumnType("decimal(10,2)");
        }
    }
}
