using DemoWebApp.Core.Domain.SuperVillainAggregate;
using DemoWebApp.Core.Infrastructure;

namespace DemoWebApp.Core.Domain.MinionAggregate
{
    public class MinionChangedLoyaltyToSuperVillainEvent : IDomainEvent
    {
        private readonly Minion _minion;
        private readonly SuperVillain _superVillain;

        public MinionChangedLoyaltyToSuperVillainEvent(Minion minion, SuperVillain superVillain)
        {
            _minion = minion;
            _superVillain = superVillain;
        }

        public Minion Minion
        {
            get { return _minion; }
        }

        public SuperVillain SuperVillain
        {
            get { return _superVillain; }
        }
    }
}