namespace Domain.Requests
{
    /// <summary>
    ///     User login request with Email and password fields
    /// </summary>
    public class UserLoginRequest
    {
        /// <summary>
        ///     User's email address
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     User's password
        /// </summary>
        public string Password { get; set; }
    }
}