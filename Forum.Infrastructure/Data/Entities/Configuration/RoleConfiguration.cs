using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Infrastructure.Data.Entities.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.HasKey(role => role.Id);
            builder.Property(role => role.Id).ValueGeneratedNever();

            builder.HasIndex(role => role.Name).IsUnique();
            builder.Property(role => role.Name).IsRequired();
        }
    }
}
