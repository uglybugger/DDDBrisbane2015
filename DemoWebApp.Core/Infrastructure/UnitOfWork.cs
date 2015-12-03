using System;
using System.Linq;
using DemoWebApp.Core.DevelopmentStubs;
using DemoWebApp.Core.Mediation;
using Serilog;
using ThirdDrawer.Extensions.CollectionExtensionMethods;

namespace DemoWebApp.Core.Infrastructure
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly IFakeDbContext _dbContext;
        private readonly ILogger _logger;
        private readonly IMediator _mediator;
        private bool _isAbandoned;
        private bool _isCompleted;

        public UnitOfWork(IFakeDbContext dbContext, IMediator mediator, ILogger logger)
        {
            _dbContext = dbContext;
            _mediator = mediator;
            _logger = logger;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public void Complete()
        {
            if (_isCompleted) throw new InvalidOperationException("Unit of work is has already been completed");
            if (_isAbandoned) throw new InvalidOperationException("Unit of work is has already been abandoned");

            DispatchDomainEvents();

            _isCompleted = true;
        }

        public void Abandon()
        {
            if (_isCompleted) throw new InvalidOperationException("Unit of work is has already been completed");
            if (_isAbandoned) throw new InvalidOperationException("Unit of work is has already been abandoned");

            _isAbandoned = true;
        }

        private void DispatchDomainEvents()
        {
            while (true)
            {
                var domainEventsThisPass = _dbContext
                    .GetChangedEntities()
                    .SelectMany(e => e.DomainEvents.GetAndClear())
                    .ToArray();

                if (domainEventsThisPass.None()) break;

                domainEventsThisPass
                    .Do(e => _logger.Information(e.ToString())) // we should probably destructure this a bit better :)
                    .Do(_mediator.Publish)
                    .Done();
            }
        }

        private void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (_isCompleted) return;
            if (_isAbandoned) return;
            throw new InvalidOperationException("A unit of work must be either completed or abandoned before it is disposed.");
        }
    }
}