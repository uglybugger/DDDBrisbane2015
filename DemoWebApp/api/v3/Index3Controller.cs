using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DemoWebApp.Core;

namespace DemoWebApp.api.v3
{
    public class Index3Controller : ApiController
    {
        private readonly IRepository<Core.Domain.SuperVillain> _superVillainRepository;

        public Index3Controller(IRepository<Core.Domain.SuperVillain> superVillainRepository)
        {
            _superVillainRepository = superVillainRepository;
        }

        [Route("api/v3/SuperVillain")]
        public HttpResponseMessage Get()
        {
            var superVillains = _superVillainRepository.GetAll();
            var dtos = superVillains
                .Select(c => new SuperVillainDto
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToArray();

            return Request.CreateResponse(HttpStatusCode.OK, dtos);
        }

        [Route("api/v3/SuperVillain/{id}")]
        public HttpResponseMessage Get(Guid id)
        {
            if (id == Guid.Empty) return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Never heard of them.");

            var superVillain = _superVillainRepository.Get(id);
            var superVillainDto = new SuperVillainDto
            {
                Id = superVillain.Id,
                Name = superVillain.Name
            };

            return Request.CreateResponse(superVillainDto);
        }

        [Route("api/v3/SuperVillain")]
        public HttpResponseMessage Post([FromBody] SuperVillainDto superVillainDto)
        {
            var superVillain = Core.Domain.SuperVillain.SignUp(superVillainDto.Id, superVillainDto.Name);
            _superVillainRepository.Add(superVillain);
            return Request.CreateResponse(HttpStatusCode.OK, "");
        }
    }
}