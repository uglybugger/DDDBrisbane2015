using DemoWebApp.Core.Infrastructure;

namespace DemoWebApp.Core.Domain.SuperVillainAggregate
{
    public class SuperVillainSignedUpEvent : IDomainEvent
    {
        private readonly SuperVillain _superVillain;

        public SuperVillainSignedUpEvent(SuperVillain superVillain)
        {
            _superVillain = superVillain;
        }

        public SuperVillain SuperVillain
        {
            get { return _superVillain; }
        }
    }
}