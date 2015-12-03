using System;
using System.Linq;
using DemoWebApp.Core.Domain.MinionAggregate;
using DemoWebApp.Core.Infrastructure;
using Serilog;
using ThirdDrawer.Extensions.CollectionExtensionMethods;

namespace DemoWebApp.Core.EventHandlers.WhenAMinionRejoices
{
    public class AnotherMinionShouldSmackIt : IHandleEvent<MinionRejoicedEvent>
    {
        public void Handle(MinionRejoicedEvent domainEvent)
        {
            var minion = domainEvent.Minion;
            var superVillain = minion.IsLoyalTo;

            if (superVillain == null)
            {
                Log.Information("Minion is not currently loyal to any supervillain so it has no friends to smack it.");
                return;
            }

            var otherMinionsBelongingToThisSuperVillain = superVillain
                .Minions
                .Except(new[] {minion})
                .ToArray();

            if (otherMinionsBelongingToThisSuperVillain.None())
            {
                Log.Information("This minion is loyal to a supervillain who has no other minions to smack him/her.");
                return;
            }

            var randomOtherMinion = otherMinionsBelongingToThisSuperVillain.Random();
            randomOtherMinion.Smack(minion);
        }
    }
}