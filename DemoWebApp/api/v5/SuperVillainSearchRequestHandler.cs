using System.Linq;
using DemoWebApp.Core.Domain.SuperVillainAggregate;
using DemoWebApp.Core.Infrastructure;
using DemoWebApp.Core.Mediation;

namespace DemoWebApp.api.v5
{
    public class SuperVillainSearchRequestHandler : IHandleRequest<Search5Controller.SearchRequest, Search5Controller.SearchResponse>
    {
        private readonly IRepository<SuperVillain> _superVillainRepository;

        public SuperVillainSearchRequestHandler(IRepository<SuperVillain> superVillainRepository)
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