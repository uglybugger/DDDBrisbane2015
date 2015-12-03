using System;
using System.Collections.Generic;
using System.Linq;
using DemoWebApp.Core.Domain.MinionAggregate;
using DemoWebApp.Core.Infrastructure;

namespace DemoWebApp.Core.Domain.SuperVillainAggregate
{
    public class SuperVillain : AggregateRoot
    {
        protected SuperVillain()
        {
        }

        private SuperVillain(Guid id, string name) : base(id)
        {
            Name = name;
            Minions = new HashSet<Minion>();

            DomainEvents.Raise(new SuperVillainSignedUpEvent(this));
        }

        public string Name { get; protected set; }
        public ICollection<Minion> Minions { get; protected set; }

        public static SuperVillain SignUp(Guid id, string name)
        {
            return new SuperVillain(id, name);
        }

        public IEnumerable<string> CannotAcquireMinion(Minion minion)
        {
            if (Minions.Select(m => m.Name).Contains(minion.Name)) yield return "This supervillain already owns a minion of that name";
        }

        public void AcquireMinion(Minion minion)
        {
            Guard.Against(() => CannotAcquireMinion(minion), "This supervillain cannot acquire that minion.");

            minion.ChangeLoyaltyTo(this);
            Minions.Add(minion);

            DomainEvents.Raise(new SuperVillainAcquiredMinionEvent(this, minion));
        }

        public void TakeNoteOfNewSuperVillain(SuperVillain superVillain)
        {
            // Meh. He/she is a wimp. Nothing to see here.
        }
    }
}