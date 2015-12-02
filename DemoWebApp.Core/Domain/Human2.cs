using System.Collections.Generic;
using System.Linq;

namespace DemoWebApp.Core.Domain
{
    public class Human2
    {
        public IEnumerable<string> CanAcquirePet()
        {
            yield return "I'm allergic to pets, dopey";
        }

        public void AcquirePet(Pet pet)
        {
            var reasons = CanAcquirePet().ToArray();
            if (reasons.Any()) throw new DomainException("This human may not acquire a pet").WithReasons(reasons);

            //Pets.Add(pet);
        }
    }
}