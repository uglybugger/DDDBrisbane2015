using DemoWebApp.api.v5;
using DemoWebApp.Core.Mediation;

namespace DemoWebApp.Mediation.MessageContracts
{
    public class SignUpSuperVillainCommand: ICommand
    {
        private readonly SuperVillainDto _superVillainDto;

        public SignUpSuperVillainCommand(SuperVillainDto superVillainDto)
        {
            _superVillainDto = superVillainDto;
        }

        public SuperVillainDto SuperVillainDto
        {
            get { return _superVillainDto; }
        }
    }
}