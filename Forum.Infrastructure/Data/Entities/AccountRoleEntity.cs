using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Infrastructure.Data.Entities
{
    public class AccountRoleEntity
    {
        public long AccountId { get; set; }
        public AccountEntity Account { get; set; }

        public long RoleId { get; set; }
        public RoleEntity Role { get; set; }
    }
}
