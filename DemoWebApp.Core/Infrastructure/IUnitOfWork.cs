namespace DemoWebApp.Core.Infrastructure
{
    public interface IUnitOfWork
    {
        void Complete();
        void Abandon();
    }
}