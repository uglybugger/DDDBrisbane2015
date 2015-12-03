using System;
using Autofac;
using Autofac.Features.OwnedInstances;
using DemoWebApp.Core.Domain.SuperVillainAggregate;
using DemoWebApp.Core.Infrastructure;

namespace DemoWebApp.DevelopmentStubs
{
    public class DemoSuperVillains : IStartable
    {
        private readonly IRepository<SuperVillain> _superVillainRepository;
        private readonly Func<Owned<IUnitOfWork>> _unitOfWorkFunc;

        public DemoSuperVillains(IRepository<SuperVillain> superVillainRepository, Func<Owned<IUnitOfWork>> unitOfWorkFunc)
        {
            _superVillainRepository = superVillainRepository;
            _unitOfWorkFunc = unitOfWorkFunc;
        }

        public void Start()
        {
            using (var unitOfWork = _unitOfWorkFunc())
            {
                var feloniusGru = SuperVillain.SignUp(Guid.NewGuid(), "Felonius Gru");
                _superVillainRepository.Add(feloniusGru);

                var margo = SuperVillain.SignUp(Guid.NewGuid(), "Margo Gru");
                _superVillainRepository.Add(margo);

                var edith = SuperVillain.SignUp(Guid.NewGuid(), "Edith Gru");
                _superVillainRepository.Add(edith);

                var agnes = SuperVillain.SignUp(Guid.NewGuid(), "Agnes Gru");
                _superVillainRepository.Add(agnes);

                unitOfWork.Value.Complete();
            }
        }
    }
}