namespace DemoWebApp.Core.Mediation
{
    public interface IHandleCommand<TCommand> where TCommand:ICommand
    {
        void Handle(TCommand command);
    }
}