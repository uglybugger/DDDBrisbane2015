using System.ComponentModel.DataAnnotations;
using System.Web.Http;
using DemoWebApp.Core.Mediation;

namespace DemoWebApp.api.v5.SuperVillain
{
    public class Search5Controller : ApiController
    {
        private readonly IMediator _mediator;

        public Search5Controller(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("api/v5/SuperVillain/Search/{name}")]
        public SearchResponse Search([FromUri] SearchRequest searchRequest)
        {
            var result = _mediator.Request(searchRequest);
            return result;
        }

        public class SearchRequest : IRequest<SearchRequest, SearchResponse>
        {
            [Required]
            public string Name { get; set; }
        }

        public class SearchResponse : IResponse
        {
            [Required]
            public SuperVillainDto[] SuperVillains { get; set; }
        }
    }
}