using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DemoWebApp.Core;
using DemoWebApp.Core.Domain;

namespace DemoWebApp.api
{
    public class CustomerController : ApiController
    {
        private readonly IRepository<Customer> _customerRepository;

        public CustomerController(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // GET: api/Person
        public IEnumerable<CustomerDto> Get()
        {
            var customers = _customerRepository.GetAll();
            var dtos = customers
                .Select(c => new CustomerDto
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToArray();
            return dtos;
        }

        // GET: api/Person/5
        public CustomerDto Get(Guid id)
        {
            var customer = _customerRepository.Get(id);
            var customerDto = new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name
            };
            return customerDto;
        }

        // POST: api/Person
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Person/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Person/5
        public void Delete(int id)
        {
        }
    }
}