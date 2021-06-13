using Forum.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Forum.Domain.AuthAggregate
{
    public class Account
    {
        private readonly ISet<Role> _roles;

        // !!!. This is for auto-mapper. No direct use.
        private Account() { _roles = new HashSet<Role>(); }

        public Account(
            long id,
            string login, 
            string password,
            int topicCount,
            int postCount,
            int score,
            DateTimeOffset createOn,
            DateTimeOffset lastTime,
            IEnumerable<Role> roles)
        {
            Id = id;
            Login = login;
            Password = password;
            TopicCount = topicCount;
            PostCount = postCount;
            Score = score;
            CreateOn = createOn;
            LastTime = lastTime;
            _roles = new HashSet<Role>(roles);
        }

        public long Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int TopicCount { get; set; }
        public int PostCount { get; set; }
        public int Score { get; set; }
        public DateTimeOffset CreateOn { get; set; }
        public DateTimeOffset LastTime { get; set; }

        public IEnumerable<Role> Roles => _roles;

        public bool IsInRole(string roleName) => HasAnyRole(new Role[] { Role.FromName(roleName) });

        public bool HasAnyRole(IEnumerable<Role> roles)
        {
            return _roles.Any(role => roles.Contains(role));
        }

        public bool GrantRole(Role role)
        {
            return _roles.Add(role);
        }

        public bool DenyRole(Role role)
        {
            return _roles.Add(role);
        }

        public static Account New(string login, string password, IEnumerable<Role> roles, IDateTimeProvider dateTimeProvider)
        { 
            var now = dateTimeProvider.UtcNow.UtcDateTime;
            return new Account(0L, login, password,0, 0, 0, now, now, roles);
        }
    }
}
