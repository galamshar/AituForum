using Microsoft.IdentityModel.Tokens;

using System;
using System.Text;

namespace Forum.Infrastructure.Auth
{
    public class AuthOptions
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public TimeSpan LifeTime { get; set; }

        public SymmetricSecurityKey SymmetricKey => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
    }
}
