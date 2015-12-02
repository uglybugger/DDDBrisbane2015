using System;

namespace DemoWebApp.Core.Domain
{
    public class Customer : IAggregateRoot
    {
        protected Customer(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Name { get; protected set; }
        public Guid Id { get; protected set; }

        public static Customer SignUp(Guid id, string name)
        {
            return new Customer(id, name);
        }
    }
}