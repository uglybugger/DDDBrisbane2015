using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DemoWebApp.Core.Infrastructure;
using DemoWebApp.Core.Mediation;
using DemoWebApp.Mediation.MessageContracts;

namespace DemoWebApp.api.v5.SuperVillain
{
    public class Index5Controller : ApiController
    {
        private readonly IRepository<Core.Domain.SuperVillainAggregate.SuperVillain> _superVillainRepository;
        private readonly IMediator _mediator;

        public Index5Controller(IRepository<Core.Domain.SuperVillainAggregate.SuperVillain> superVillainRepository, IMediator mediator)
        {
            _superVillainRepository = superVillainRepository;
            _mediator = mediator;
        }

        [Route("api/v5/SuperVillain")]
        public HttpResponseMessage Get()
        {
            //FIXME this one hasn't been refactored from v4 yet
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

        [Route("api/v5/SuperVillain/{id}")]
        public HttpResponseMessage Get(Guid id)
        {
            //FIXME neither has this one
            if (id == Guid.Empty) return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Never heard of them.");

            var superVillain = _superVillainRepository.Get(id);
            var superVillainDto = new SuperVillainDto
            {
                Id = superVillain.Id,
                Name = superVillain.Name
            };

            return Request.CreateResponse(superVillainDto);
        }

        [Route("api/v5/SuperVillain")]
        public void Put([FromBody] SuperVillainDto superVillainDto)
        {
            var command = new SignUpSuperVillainCommand(superVillainDto);
            _mediator.Send(command);
        }
    }
}