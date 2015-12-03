using DemoWebApp.Core.Domain.SuperVillainAggregate;
using DemoWebApp.Core.Infrastructure;
using DemoWebApp.Core.Mediation;
using ThirdDrawer.Extensions.CollectionExtensionMethods;

namespace DemoWebApp.Core.EventHandlers.WhenASuperVillainAcquiresAMinion
{
    public class AllTheirMinionsShouldRejoice : IHandleEvent<SuperVillainAcquiredMinionEvent>
    {
        public void Handle(SuperVillainAcquiredMinionEvent domainEvent)
        {
            domainEvent.SuperVillain.Minions
                .Do(minion => minion.Rejoice())
                .Done();
        }
    }
}