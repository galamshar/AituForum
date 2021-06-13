using Forum.ApplicationLayer.Auth;

namespace Forum.Infrastructure.Services
{
    /// <summary>
    /// I'm too lazy to implement or use real hashing algorithm.
    /// </summary>
    public class NullPasswordHasher : IPasswordHasher
    {
        public string Hash(string password) => password;
        public bool Verify(string hash, string password) => hash == password;
    }
}
