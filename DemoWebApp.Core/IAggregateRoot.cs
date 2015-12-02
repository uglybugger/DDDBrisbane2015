using System;

namespace DemoWebApp.Core
{
    public interface IAggregateRoot
    {
        Guid Id { get; }
    }
}