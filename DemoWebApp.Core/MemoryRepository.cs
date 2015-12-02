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
            return _items[id];
        }

        public T[] GetAll()
        {
            return _items.Values.ToArray();
        }
    }
}