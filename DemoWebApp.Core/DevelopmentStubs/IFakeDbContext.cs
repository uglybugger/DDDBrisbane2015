using DemoWebApp.Core.Infrastructure;

namespace DemoWebApp.Core.DevelopmentStubs
{
    public interface IFakeDbContext
    {
        AggregateRoot[] GetChangedEntities();
        void SaveChanges();
    }
}