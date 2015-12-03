using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Http;
using DemoWebApp.Core;
using DemoWebApp.Core.Domain;
using DemoWebApp.Core.Domain.SuperVillainAggregate;
using DemoWebApp.Core.Infrastructure;

namespace DemoWebApp.api.v5
{
    public class Search5Controller : ApiController
    {
        private readonly IRepository<SuperVillain> _superVillainRepository;

        public Search5Controller(IRepository<SuperVillain> superVillainRepository)
        {
            _superVillainRepository = superVillainRepository;
        }

        [HttpGet]
        [Route("api/v5/SuperVillain/Search/{name}")]
        public SuperVillainDto[] Search([FromUri] SearchDto searchCriteria)
        {
            var superVillains = _superVillainRepository.GetAll()
                .Where(c => c.Name.ToLowerInvariant().Contains(searchCriteria.Name.ToLowerInvariant()))
                .ToArray();

            var dtos = superVillains
                .Select(c => new SuperVillainDto
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToArray();

            return dtos;
        }

        public class SearchDto
        {
            [Required]
            public string Name { get; set; }
        }
    }
}