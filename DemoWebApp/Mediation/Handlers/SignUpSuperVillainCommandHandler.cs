using DemoWebApp.Core.Domain.SuperVillainAggregate;
using DemoWebApp.Core.Infrastructure;
using DemoWebApp.Core.Mediation;
using DemoWebApp.Mediation.MessageContracts;

namespace DemoWebApp.Mediation.Handlers
{
    public class SignUpSuperVillainCommandHandler : IHandleCommand<SignUpSuperVillainCommand>
    {
        private readonly IRepository<SuperVillain> _superVillainRepository;

        public SignUpSuperVillainCommandHandler(IRepository<SuperVillain> superVillainRepository)
        {
            _superVillainRepository = superVillainRepository;
        }

        public void Handle(SignUpSuperVillainCommand command)
        {
            var superVillainDto = command.SuperVillainDto;
            var superVillain = SuperVillain.SignUp(superVillainDto.Id, superVillainDto.Name);
            _superVillainRepository.Add(superVillain);
        }
    }
}