using System;
using System.Collections.Generic;
using System.Linq;
using ThirdDrawer.Extensions.CollectionExtensionMethods;

namespace DemoWebApp.Core.Infrastructure
{
    public class Guard
    {
        public static void Against(Func<IEnumerable<string>> failureReasonsFunc, string message)
        {
            var reasons = failureReasonsFunc().ToArray();
            if (reasons.None()) return;

            throw new DomainException(message).WithReasons(reasons);
        }
    }
}