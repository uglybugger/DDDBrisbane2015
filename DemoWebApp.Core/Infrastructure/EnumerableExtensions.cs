using System;
using System.Collections.Generic;
using System.Linq;

namespace DemoWebApp.Core.Infrastructure
{
    public static class EnumerableExtensions
    {
        public static T Random<T>(this IEnumerable<T> source)
        {
            var random = new Random((int) DateTimeOffset.UtcNow.Ticks);
            var i = random.Next(source.Count() - 1);
            return source.ElementAt(i);
        }
    }
}