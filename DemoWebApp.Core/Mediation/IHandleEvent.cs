namespace DemoWebApp.Core.Mediation
{
    public interface IHandleEvent<T> where T : IEvent
    {
        void Handle(T domainEvent);
    }
}