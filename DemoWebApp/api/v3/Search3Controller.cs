﻿using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DemoWebApp.Core;
using DemoWebApp.Core.Domain.SuperVillainAggregate;
using DemoWebApp.Core.Infrastructure;

namespace DemoWebApp.api.v3
{
    public class Search3Controller: ApiController
    {
        private readonly IRepository<SuperVillain> _superVillainRepository;

        public Search3Controller(IRepository<SuperVillain> superVillainRepository)
        {
            _superVillainRepository = superVillainRepository;
        }

        [HttpGet]
        [Route("api/v3/SuperVillain/Search/{name}")]
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
            public string Name { get; set; }
        }
    }
}