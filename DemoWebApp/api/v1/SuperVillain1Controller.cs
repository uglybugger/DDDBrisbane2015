﻿using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DemoWebApp.Core;
using DemoWebApp.Core.Domain;
using DemoWebApp.Core.Domain.SuperVillainAggregate;
using DemoWebApp.Core.Infrastructure;

namespace DemoWebApp.api.v1
{
    public class SuperVillain1Controller : ApiController
    {
        private readonly IRepository<SuperVillain> _superVillainRepository;

        public SuperVillain1Controller(IRepository<SuperVillain> superVillainRepository)
        {
            _superVillainRepository = superVillainRepository;
        }

        [Route("api/v1/SuperVillain")]
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

        [Route("api/v1/SuperVillain/{id}")]
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

        [HttpGet]
        [Route("api/v1/SuperVillain/Search/{name}")]
        public HttpResponseMessage Search(string name)
        {
            if (!ModelState.IsValid) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad request");

            var superVillains = _superVillainRepository.GetAll()
                .Where(c => c.Name.ToLowerInvariant().Contains(name.ToLowerInvariant()))
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

        [Route("api/v1/SuperVillain")]
        public HttpResponseMessage Post([FromBody] SuperVillainDto superVillainDto)
        {
            if (!ModelState.IsValid) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad request");

            var superVillain = SuperVillain.SignUp(superVillainDto.Id, superVillainDto.Name);
            _superVillainRepository.Add(superVillain);
            return Request.CreateResponse(HttpStatusCode.OK, "");
        }
    }
}