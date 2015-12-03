using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DemoWebApp.Core;
using DemoWebApp.Core.Domain;

namespace DemoWebApp.api.v4
{
    public class Search4Controller : ApiController
    {
        private readonly IRepository<SuperVillain> _superVillainRepository;

        public Search4Controller(IRepository<SuperVillain> superVillainRepository)
        {
            _superVillainRepository = superVillainRepository;
        }

        [HttpGet]
        [Route("api/v4/SuperVillain/Search/{name}")]
        public HttpResponseMessage Search([FromUri] SearchDto searchCriteria)
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

            return Request.CreateResponse(HttpStatusCode.OK, dtos);
        }

        public class SearchDto
        {
            public string Name { get; set; }
        }
    }
}