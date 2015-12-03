namespace DemoWebApp.Core.Mediation
{
    public interface IMediator
    {
        TResponse Request<TRequest, TResponse>(IRequest<TRequest, TResponse> request)
            where TRequest : IRequest<TRequest, TResponse>
            where TResponse : IResponse;

        void Publish<TEvent>(TEvent @event) where TEvent : IEvent;

        void Send<TCommand>(TCommand command) where TCommand : ICommand;
    }
}