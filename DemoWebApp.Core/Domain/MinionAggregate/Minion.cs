using System;
using System.Collections.Generic;
using DemoWebApp.Core.Domain.SuperVillainAggregate;
using DemoWebApp.Core.Infrastructure;

namespace DemoWebApp.Core.Domain.MinionAggregate
{
    public class Minion : AggregateRoot
    {
        private Minion(Guid id, string name) : base(id)
        {
            Name = name;
        }

        public string Name { get; protected set; }
        public SuperVillain IsLoyalTo { get; protected set; }

        public IEnumerable<string> CannotRejoice()
        {
            // Are you kidding me? Minions can always rejoice!
            yield break;
        }

        public void Rejoice()
        {
            Guard.Against(CannotRejoice, "This minion cannot rejoice.");

            // Hooray!

            DomainEvents.Raise(new MinionRejoicedEvent(this));
        }

        internal IEnumerable<string> CannotChangeLoyaltyTo(SuperVillain superVillain)
        {
            if (IsLoyalTo != null && IsLoyalTo != superVillain) yield return "This minion is already loyal to another supervillain.";
        }

        internal void ChangeLoyaltyTo(SuperVillain superVillain)
        {
            if (IsLoyalTo == superVillain) return;
            Guard.Against(() => CannotChangeLoyaltyTo(superVillain), "This minion cannot change loyalty to this supervillain.");

            IsLoyalTo = superVillain;

            DomainEvents.Raise(new MinionChangedLoyaltyToSuperVillainEvent(this, superVillain));
        }

        public void Smack(Minion minion)
        {
            // ... and so on...
        }
    }
}