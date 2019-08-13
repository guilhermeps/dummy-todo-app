namespace DummyTodoApp.Core.UseCases.Boundaries
{
    public interface IErrorHandler
    {
        void NotifyError(string message);
    }
}
