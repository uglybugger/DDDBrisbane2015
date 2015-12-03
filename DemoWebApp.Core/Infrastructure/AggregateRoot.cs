using System;

namespace DemoWebApp.Core.Infrastructure
{
    public class AggregateRoot : IAggregateRoot
    {
        protected AggregateRoot()
        {
        }

        protected AggregateRoot(Guid id)
        {
            Id = id;
            DomainEvents = new DomainEventsCollection();
        }

        public DomainEventsCollection DomainEvents { get; private set; }
        public Guid Id { get; protected set; }
    }
}