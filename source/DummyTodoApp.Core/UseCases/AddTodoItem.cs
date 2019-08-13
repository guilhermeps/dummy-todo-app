using DummyTodoApp.Core.Boundaries.AddTodo;
using DummyTodoApp.Core.Domain;
using DummyTodoApp.Core.Repositories;
using DummyTodoApp.Core.UseCases.AddTodo.Boundaries;
using System.Threading.Tasks;

namespace DummyTodoApp.Core.UseCases
{
    public sealed class AddTodoItem : IUseCase
    {
        readonly ITodoRepository repository;
        readonly IOutputHandler outputHandler;

        public AddTodoItem(
            ITodoRepository repo,
            IOutputHandler handler)
        {
            repository = repo;
            outputHandler = handler;
        }

        public async Task Execute(Input input)
        {
            if (!input.IsValid())
            {
                outputHandler.NotifyError("Todo object is not valid.");
                return;
            }

            await repository.Add(new Todo(input.Description, input.Owner));
            outputHandler.Handle(new Output
            {
                Description = input.Description,
                Owner = input.Owner,
                Done = false
            });
        }
    }
}
