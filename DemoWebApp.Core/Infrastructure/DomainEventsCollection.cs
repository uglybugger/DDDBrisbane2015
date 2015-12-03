using System.Collections.Generic;

namespace DemoWebApp.Core.Infrastructure
{
    public class DomainEventsCollection
    {
        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
        private readonly object _mutex = new object();

        public void Raise<T>(T domainEvent) where T : IDomainEvent
        {
            lock (_mutex)
            {
                _domainEvents.Add(domainEvent);
            }
        }

        public IDomainEvent[] GetAndClear()
        {
            lock (_mutex)
            {
                var domainEvents = _domainEvents.ToArray();
                _domainEvents.Clear();
                return domainEvents;
            }
        }
    }
}