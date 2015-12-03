using System.Linq;
using DemoWebApp.Core.Domain.MinionAggregate;
using DemoWebApp.Core.Domain.SuperVillainAggregate;
using DemoWebApp.Core.Infrastructure;

namespace DemoWebApp.Core.DevelopmentStubs
{
    /// <summary>
    /// This is a completely fake DB context. It mimics EF in the way that it will expose a list of "potentially changed"
    /// entities so that we can scan through them for domain events. It's not thread-safe so please don't use this anywhere
    /// other than in demo code. -andrewh
    /// </summary>
    public class FakeDbContext : IFakeDbContext
    {
        private readonly IRepository<Minion> _minionRepository;
        private readonly IRepository<SuperVillain> _superVillainRepository;

        public FakeDbContext(IRepository<SuperVillain> superVillainRepository, IRepository<Minion> minionRepository)
        {
            _superVillainRepository = superVillainRepository;
            _minionRepository = minionRepository;
        }

        public AggregateRoot[] GetChangedEntities()
        {
            return new AggregateRoot[0]
                .Union(_superVillainRepository.GetAll())
                .Union(_minionRepository.GetAll())
                .ToArray();
        }

        public void SaveChanges()
        {
            // This is a fake context. What do you expect? :)
        }
    }
}