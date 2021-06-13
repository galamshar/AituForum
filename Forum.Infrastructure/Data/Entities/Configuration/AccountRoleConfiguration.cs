using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Infrastructure.Data.Entities.Configuration
{
    public class AccountRoleConfiguration : IEntityTypeConfiguration<AccountRoleEntity>
    {
        public void Configure(EntityTypeBuilder<AccountRoleEntity> builder)
        {
            builder.HasKey(accountRole => new { accountRole.AccountId, accountRole.RoleId });
        }
    }
}
