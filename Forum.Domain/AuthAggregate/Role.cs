using Forum.Domain.SeedWork;

namespace Forum.Domain.AuthAggregate
{
    public abstract class Role : Enumeration<Role, long>
    {
        public Role(long key, string name) : base(key, name)
        {
        }

        public static readonly Role Student = new StudentRole(1L, nameof(Student));
        public static readonly Role Admin = new AdminRole(2L, nameof(Admin));
    }

    public class StudentRole : Role
    {
        public StudentRole(long key, string name) : base(key, name)
        {
        }
    }

    public class AdminRole : Role
    {
        public AdminRole(long key, string name) : base(key, name)
        {
        }
    }
}
