using System;
using Autofac;
using DemoWebApp.Core;
using DemoWebApp.Core.Domain;

namespace DemoWebApp.DevelopmentStubs
{
    public class DemoSuperVillains : IStartable
    {
        private readonly IRepository<SuperVillain> _superVillainRepository;

        public DemoSuperVillains(IRepository<SuperVillain> superVillainRepository)
        {
            _superVillainRepository = superVillainRepository;
        }

        public void Start()
        {
            var feloniusGru = SuperVillain.SignUp(Guid.NewGuid(), "Felonius Gru");
            _superVillainRepository.Add(feloniusGru);

            var margo = SuperVillain.SignUp(Guid.NewGuid(), "Margo Gru");
            _superVillainRepository.Add(margo);

            var edith = SuperVillain.SignUp(Guid.NewGuid(), "Edith Gru");
            _superVillainRepository.Add(edith);

            var agnes = SuperVillain.SignUp(Guid.NewGuid(), "Agnes Gru");
            _superVillainRepository.Add(agnes);
        }
    }
}