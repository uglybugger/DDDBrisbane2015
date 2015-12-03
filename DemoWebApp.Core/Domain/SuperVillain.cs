using System;

namespace DemoWebApp.Core.Domain
{
    public class SuperVillain : IAggregateRoot
    {
        protected SuperVillain(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Name { get; protected set; }
        public Guid Id { get; protected set; }

        public static SuperVillain SignUp(Guid id, string name)
        {
            return new SuperVillain(id, name);
        }
    }
}