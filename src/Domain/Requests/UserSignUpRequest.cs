namespace Domain.Requests
{
    /// <summary>
    ///     User sign up request with Email, FirstName, LastName, Password and ConfirmPassword fields
    /// </summary>
    public class UserSignUpRequest
    {
        /// <summary>
        ///     User's email address
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     User's first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///     User's last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///     User's password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        ///     Confirmation of user's password
        /// </summary>
        public string ConfirmPassword { get; set; }
    }
}