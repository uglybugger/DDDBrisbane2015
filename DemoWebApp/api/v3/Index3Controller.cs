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
        private readonly IRepository<Core.Domain.Customer> _customerRepository;

        public Index3Controller(IRepository<Core.Domain.Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [Route("api/v3/Customer")]
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

        [Route("api/v3/Customer/{id}")]
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

        [Route("api/v3/Customer")]
        public HttpResponseMessage Post([FromBody] CustomerDto customerDto)
        {
            var customer = Core.Domain.Customer.SignUp(customerDto.Id, customerDto.Name);
            _customerRepository.Add(customer);
            return Request.CreateResponse(HttpStatusCode.OK, "");
        }
    }
}