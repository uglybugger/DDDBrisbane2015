using System;

namespace DemoWebApp.Core
{
    public static class ExceptionExtensions
    {
        public static TException WithReasons<TException>(this TException e, params string[] reasons) where TException : Exception
        {
            e.Data["Reasons"] = reasons;
            return e;
        }
    }
}