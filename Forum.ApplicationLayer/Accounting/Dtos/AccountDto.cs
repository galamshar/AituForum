using Forum.Domain.AuthAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.ApplicationLayer.Accounting.Dtos
{
    public class AccountDto
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public int TopicCount { get; set; }
        public int PostCount { get; set; }
        public int Score { get; set; }
        public DateTimeOffset CreateOn { get; set; }
        public DateTimeOffset LastTime { get; set; }
        public IEnumerable<Role> Roles { get; set; }
    }
}
