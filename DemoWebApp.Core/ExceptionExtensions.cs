using System;

namespace DemoWebApp.Core
{
    public static class ExceptionExtensions
    {
        public static TException WithReasons<TException>(this TException e, params string[] reasons) where TException : Exception
        {
            return e.WithData("Reasons", reasons);
        }

        public static TException WithData<TException>(this TException e, string key, object value) where TException : Exception
        {
            e.Data[key] = value;
            return e;
        }
    }
}