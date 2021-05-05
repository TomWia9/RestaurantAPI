using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace RestaurantAPI.Models.Auth
{
    public class Role : IdentityRole<Guid> 
    {
    }
}
