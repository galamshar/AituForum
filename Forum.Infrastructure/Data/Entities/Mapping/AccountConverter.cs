using AutoMapper;
using Forum.Domain.AuthAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forum.Infrastructure.Data.Entities.Mapping
{
    class AccountConverter : ITypeConverter<AccountEntity, Account>, ITypeConverter<Account, AccountEntity>
    {
        public Account Convert(AccountEntity source, Account destination, ResolutionContext context)
        {
            var roles = new List<Role>();
            foreach (var role in source.Roles)
            {
                if (role.RoleId == 1)
                {
                    roles.Add(new StudentRole(role.RoleId, role.Role.Name));
                }
                else
                {
                    roles.Add(new AdminRole(role.RoleId, role.Role.Name));
                }
            }
            return new Account(source.Id, source.Login, source.Password, source.TopicCount, source.PostCount, source.Score, source.CreateOn, source.LastTime, roles);
        }

        public AccountEntity Convert(Account source, AccountEntity destination, ResolutionContext context)
        {
            var roles = new List<AccountRoleEntity>();
            foreach (var role in source.Roles)
            {
                roles.Add(new AccountRoleEntity { AccountId = source.Id, RoleId = role.Key });
            }
            return new AccountEntity
            {
                Id = source.Id,
                Login = source.Login,
                Password = source.Password,
                TopicCount = source.TopicCount,
                PostCount = source.PostCount,
                Score = source.Score,
                CreateOn = source.CreateOn,
                LastTime = source.LastTime,
                Roles = roles
            };
        }
    }
}
