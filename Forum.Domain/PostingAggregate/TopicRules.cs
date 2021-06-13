using Forum.Domain.AuthAggregate;

using System.Collections.Generic;
using System.Linq;

namespace Forum.Domain.PostingAggregate
{
    public class TopicRules
    {
        private readonly ISet<Role> _allowedToRead;
        private readonly ISet<Role> _allowedToWrite;

        public TopicRules(
            IEnumerable<Role> allowedToRead,
            IEnumerable<Role> allowedToWrite,
            bool canOwnSubtopics)
        {
            _allowedToRead = new HashSet<Role>(allowedToRead);
            _allowedToWrite = new HashSet<Role>(allowedToWrite);
            CanOwnSubtopics = canOwnSubtopics;
        }

        public bool CanOwnSubtopics { get; set; }
        public bool CanOwnPosts => !CanOwnSubtopics;

        public IEnumerable<Role> RolesAllowedToRead => _allowedToRead;
        public bool IsRestrictedForRead => RolesAllowedToRead.Any();

        public IEnumerable<Role> RolesAllowedToWrite => _allowedToWrite;
        public bool IsRestrictedForWrite => RolesAllowedToWrite.Any();

        public bool AllowReadTo(Role role)
        {
            return _allowedToRead.Add(role);
        }

        public bool DisallowReadTo(Role role)
        {
            return _allowedToRead.Remove(role);
        }

        public bool AllowWriteTo(Role role)
        {
            return _allowedToWrite.Add(role);
        }

        public bool DisallowWriteTo(Role role)
        {
            return _allowedToWrite.Remove(role);
        }

        public static TopicRules Empty()
        {
            return new TopicRules(Enumerable.Empty<Role>(), Enumerable.Empty<Role>(), true);
        }
    }
}
