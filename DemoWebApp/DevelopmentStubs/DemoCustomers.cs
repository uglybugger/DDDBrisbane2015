using System;
using Autofac;
using DemoWebApp.Core;
using DemoWebApp.Core.Domain;

namespace DemoWebApp.DevelopmentStubs
{
    public class DemoCustomers : IStartable
    {
        private readonly IRepository<Customer> _customerRepository;

        public DemoCustomers(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public void Start()
        {
            var wilma = Customer.SignUp(Guid.NewGuid(), "Wilma Flintstone");
            _customerRepository.Add(wilma);

            var fred = Customer.SignUp(Guid.NewGuid(), "Fred Flintstone");
            _customerRepository.Add(fred);

            var betty = Customer.SignUp(Guid.NewGuid(), "Betty Rubble");
            _customerRepository.Add(betty);

            var barney = Customer.SignUp(Guid.NewGuid(), "Barney Rubble");
            _customerRepository.Add(barney);
        }
    }
}