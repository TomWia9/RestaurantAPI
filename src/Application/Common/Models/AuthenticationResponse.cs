using System.Collections.Generic;

namespace Application.Common.Models
{
    /// <summary>
    ///     Authentication response with jwt token and list of authentication errors
    /// </summary>
    public class AuthenticationResponse
    {
        /// <summary>
        ///     User's authentication token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        ///     List of authentication errors
        /// </summary>
        public IEnumerable<string> ErrorMessages { get; set; } = new List<string>();
    }
}