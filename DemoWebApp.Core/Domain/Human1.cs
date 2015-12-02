namespace DemoWebApp.Core.Domain
{
    public class Human1
    {
        public bool CanAquirePet()
        {
            return false;
        }

        public void AquirePet(Pet pet)
        {
            if (!CanAquirePet()) throw new DomainException("This human may not acquire a pet.");
        }
    }
}