using Forum.Infrastructure.Data.Entities;
using Forum.Infrastructure.Data.Entities.Configuration;

using Microsoft.EntityFrameworkCore;

namespace Forum.Infrastructure.Data
{
    public class ForumContext : DbContext
    {
        public ForumContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AccountEntity> Accounts { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<AccountRoleEntity> AccountRoles { get; set; }
        public DbSet<TopicEntity> Topics { get; set; }
        public DbSet<TopicRestrictionEntity> TopicRestrictions { get; set; }
        public DbSet<PostEntity> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new PostConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new AccountRoleConfiguration());
            modelBuilder.ApplyConfiguration(new TopicConfiguration());
            modelBuilder.ApplyConfiguration(new TopicRestrictionConfiguration());
        }
    }
}
