using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DemoWebApp.Core;
using DemoWebApp.Core.Domain;

namespace DemoWebApp.api.v2
{
    public class Customer2Controller : ApiController
    {
        private readonly IRepository<Customer> _customerRepository;

        public Customer2Controller(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [Route("api/v2/Customer")]
        public HttpResponseMessage Get()
        {
            var customers = _customerRepository.GetAll();
            var dtos = customers
                .Select(c => new CustomerDto
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToArray();

            return Request.CreateResponse(HttpStatusCode.OK, dtos);
        }

        [Route("api/v2/Customer/{id}")]
        public HttpResponseMessage Get(Guid id)
        {
            if (id == Guid.Empty) return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Never heard of them.");

            var customer = _customerRepository.Get(id);
            var customerDto = new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name
            };

            return Request.CreateResponse(customerDto);
        }

        [HttpGet]
        [Route("api/v2/Customer/Search/{name}")]
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

        [Route("api/v2/Customer")]
        public HttpResponseMessage Post([FromBody] CustomerDto customerDto)
        {
            var customer = Customer.SignUp(customerDto.Id, customerDto.Name);
            _customerRepository.Add(customer);
            return Request.CreateResponse(HttpStatusCode.OK, "");
        }
    }
}