using DemoWebApp.Core.Domain.MinionAggregate;
using DemoWebApp.Core.Infrastructure;

namespace DemoWebApp.Core.Domain.SuperVillainAggregate
{
    public class SuperVillainAcquiredMinionEvent : IDomainEvent
    {
        private readonly Minion _minion;
        private readonly SuperVillain _superVillain;

        public SuperVillainAcquiredMinionEvent(SuperVillain superVillain, Minion minion)
        {
            _superVillain = superVillain;
            _minion = minion;
        }

        public SuperVillain SuperVillain
        {
            get { return _superVillain; }
        }

        public Minion Minion
        {
            get { return _minion; }
        }
    }
}