namespace DemoWebApp.Core.Infrastructure
{
    public interface IHandleEvent<T> where T : IDomainEvent
    {
        void Handle(T domainEvent);
    }
}