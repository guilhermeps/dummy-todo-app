using DummyTodoApp.Core.UseCases.Boundaries;

namespace DummyTodoApp.Core.Boundaries.GetTodosByOwner
{
    public interface IOutputHandler : IErrorHandler
    {
        void Handle(Output output);
    }
}
