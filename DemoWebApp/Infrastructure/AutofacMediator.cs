using System.Collections.Generic;
using Autofac;
using DemoWebApp.Core.Mediation;
using ThirdDrawer.Extensions.CollectionExtensionMethods;

namespace DemoWebApp.Infrastructure
{
    public class AutofacMediator : IMediator
    {
        private readonly ILifetimeScope _lifetimeScope;

        public AutofacMediator(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        public TResponse Request<TRequest, TResponse>(IRequest<TRequest, TResponse> request)
            where TRequest : IRequest<TRequest, TResponse>
            where TResponse : IResponse
        {
            var handler = _lifetimeScope.Resolve<IHandleRequest<TRequest, TResponse>>();
            var response = handler.Handle((TRequest) request);
            return response;
        }

        public void Publish<TEvent>(TEvent @event) where TEvent : IEvent
        {
            var handlers = _lifetimeScope.Resolve<IEnumerable<IHandleEvent<TEvent>>>();
            handlers
                .Do(h => h.Handle(@event))
                .Done();
        }

        public void Send<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handler = _lifetimeScope.Resolve<IHandleCommand<TCommand>>();
            handler.Handle(command);
        }
    }
}