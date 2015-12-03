using System.Linq;
using DemoWebApp.Core.Domain.SuperVillainAggregate;
using DemoWebApp.Core.Infrastructure;
using DemoWebApp.Core.Mediation;
using ThirdDrawer.Extensions.CollectionExtensionMethods;

namespace DemoWebApp.Core.EventHandlers.WhenASuperVillainSignsUp
{
    public class AllTheOtherSuperVillainsShouldTakeNote : IHandleEvent<SuperVillainSignedUpEvent>
    {
        private readonly IRepository<SuperVillain> _superVillainRepository;

        public AllTheOtherSuperVillainsShouldTakeNote(IRepository<SuperVillain> superVillainRepository)
        {
            _superVillainRepository = superVillainRepository;
        }

        public void Handle(SuperVillainSignedUpEvent domainEvent)
        {
            var allOtherSuperVillains = _superVillainRepository
                .GetAll()
                .Where(sv => sv.Id != domainEvent.SuperVillain.Id)
                .ToArray();

            allOtherSuperVillains
                .Do(sv => sv.TakeNoteOfNewSuperVillain(domainEvent.SuperVillain))
                .Done();
        }
    }
}