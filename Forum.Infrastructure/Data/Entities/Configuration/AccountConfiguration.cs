using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Infrastructure.Data.Entities.Configuration
{
    public class AccountConfiguration : IEntityTypeConfiguration<AccountEntity>
    {
        public void Configure(EntityTypeBuilder<AccountEntity> builder)
        {
            builder.HasKey(account => account.Id);

            builder.HasIndex(account => account.Login).IsUnique();
            builder.Property(account => account.Login).IsRequired();

            builder.Property(account => account.Password).IsRequired();
        }
    }
}
