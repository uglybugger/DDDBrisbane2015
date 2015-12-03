using System.Linq;
using DemoWebApp.api.v5;
using DemoWebApp.api.v5.SuperVillain;
using DemoWebApp.Core.Infrastructure;
using DemoWebApp.Core.Mediation;

namespace DemoWebApp.Mediation.Handlers
{
    public class SuperVillainSearchRequestHandler : IHandleRequest<Search5Controller.SearchRequest, Search5Controller.SearchResponse>
    {
        private readonly IRepository<api.v5.SuperVillain> _superVillainRepository;

        public SuperVillainSearchRequestHandler(IRepository<api.v5.SuperVillain> superVillainRepository)
        {
            _superVillainRepository = superVillainRepository;
        }

        public Search5Controller.SearchResponse Handle(Search5Controller.SearchRequest request)
        {
            var superVillains = _superVillainRepository.GetAll()
                .Where(c => c.Name.ToLowerInvariant().Contains(request.Name.ToLowerInvariant()))
                .ToArray();

            var dtos = superVillains
                .Select(c => new SuperVillainDto
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToArray();

            var response = new Search5Controller.SearchResponse
            {
                SuperVillains = dtos
            };

            return response;
        }
    }
}