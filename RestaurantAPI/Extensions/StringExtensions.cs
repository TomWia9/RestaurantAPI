using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Extensions
{
    public static class StringExtensions
    {
        public static string FirstCharToUpper(this string value)
        {
            return value switch
            {
                null => throw new ArgumentNullException(nameof(value)),
                "" => throw new ArgumentException($"{nameof(value)} cannot be empty", nameof(value)),
                _ => value.First().ToString().ToUpper() + value[1..]
            };
        }
    }
}
