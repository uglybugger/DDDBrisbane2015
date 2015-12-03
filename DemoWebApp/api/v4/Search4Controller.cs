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
        private readonly IRepository<Customer> _customerRepository;

        public Search4Controller(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        [Route("api/v4/Customer/Search/{name}")]
        public HttpResponseMessage Search([FromUri] SearchDto searchCriteria)
        {
            var customers = _customerRepository.GetAll()
                .Where(c => c.Name.ToLowerInvariant().Contains(searchCriteria.Name.ToLowerInvariant()))
                .ToArray();

            var dtos = customers
                .Select(c => new CustomerDto
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