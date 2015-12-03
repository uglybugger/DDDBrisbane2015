using System;

namespace DemoWebApp.Core.Infrastructure
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        void Add(T item);
        T Get(Guid id);
        T[] GetAll();
    }
}