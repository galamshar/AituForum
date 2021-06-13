using System;
using System.Collections.Generic;

namespace Forum.Infrastructure.Data.Entities
{
    public class AccountEntity
    {
        public long Id { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }
        public int TopicCount { get; set; }
        public int PostCount { get; set; }
        public int Score { get; set; }
        public DateTimeOffset CreateOn { get; set; }
        public DateTimeOffset LastTime { get; set; }
        public ICollection<AccountRoleEntity> Roles { get; set; }
    }
}
