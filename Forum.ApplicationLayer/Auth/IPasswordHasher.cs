namespace Forum.ApplicationLayer.Auth
{
    /// <summary>
    /// Defines mechanism for hashing and verifying passwords.
    /// </summary>
    public interface IPasswordHasher
    {
        /// <summary>
        /// Hashes given using some hashing algorithm password.
        /// </summary>
        /// <param name="password"> Password to hash. </param>
        /// <returns> Hashed password. </returns>
        string Hash(string password);

        /// <summary>
        /// Verifies password with hash.
        /// </summary>
        /// <param name="hash"> Hash of password. </param>
        /// <param name="password"> Password to verify. </param>
        /// <returns> Is password matches hash. </returns>
        bool Verify(string hash, string password);
    }
}
