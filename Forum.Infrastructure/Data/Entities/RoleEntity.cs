using System.Collections.Generic;

namespace Forum.Infrastructure.Data.Entities
{
    public class RoleEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public ICollection<AccountRoleEntity> Accounts { get; set; }
    }
}
