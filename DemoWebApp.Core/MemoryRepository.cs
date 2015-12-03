using System;
using System.Collections.Generic;
using System.Linq;

namespace DemoWebApp.Core
{
    public class MemoryRepository<T> : IRepository<T> where T : IAggregateRoot
    {
        private readonly Dictionary<Guid, T> _items = new Dictionary<Guid, T>();

        public void Add(T item)
        {
            _items.Add(item.Id, item);
        }

        public T Get(Guid id)
        {
            T item;
            if (_items.TryGetValue(id, out item)) return item;

            throw new EntityNotFoundException()
                .WithData("EntityType", typeof (T).FullName)
                .WithData("Id", id);
        }

        public T[] GetAll()
        {
            return _items.Values.ToArray();
        }
    }
}