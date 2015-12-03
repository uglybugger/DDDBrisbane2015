using DemoWebApp.Core.Infrastructure;

namespace DemoWebApp.Core.Domain.MinionAggregate
{
    public class MinionRejoicedEvent : IDomainEvent
    {
        private readonly Minion _minion;

        public MinionRejoicedEvent(Minion minion)
        {
            _minion = minion;
        }

        public Minion Minion
        {
            get { return _minion; }
        }
    }
}