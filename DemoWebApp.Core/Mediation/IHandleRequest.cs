namespace DemoWebApp.Core.Mediation
{
    public interface IHandleRequest<TRequest, TResponse>
        where TRequest : IRequest<TRequest, TResponse>
        where TResponse : IResponse
    {
        TResponse Handle(TRequest request);
    }
}