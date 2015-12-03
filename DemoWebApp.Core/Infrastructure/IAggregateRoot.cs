using System;

namespace DemoWebApp.Core.Infrastructure
{
    public interface IAggregateRoot
    {
        Guid Id { get; }
    }
}