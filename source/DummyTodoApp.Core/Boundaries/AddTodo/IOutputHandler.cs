using DummyTodoApp.Core.UseCases.AddTodo.Boundaries;
using DummyTodoApp.Core.UseCases.Boundaries;

namespace DummyTodoApp.Core.Boundaries.AddTodo
{
    public interface IOutputHandler : IErrorHandler
    {
        void Handle(Output output);
    }
}
