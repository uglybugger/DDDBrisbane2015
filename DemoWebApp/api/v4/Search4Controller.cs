using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DemoWebApp.Core;
using DemoWebApp.Core.Domain;
using DemoWebApp.Core.Domain.SuperVillainAggregate;
using DemoWebApp.Core.Infrastructure;

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
        [ModelValidation]
        public HttpResponseMessage Search([FromUri] SearchDto searchCriteria)
        {
            if (!ModelState.IsValid) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad request");

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
            [Required]
            public string Name { get; set; }
        }
    }
}