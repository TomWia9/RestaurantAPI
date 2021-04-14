using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Data.Response
{
    public class AuthenticationResponse
    {
        public string Token { get; set; }
        public IEnumerable<string> ErrorMessages { get; set; } = new List<string>();
    }
}
