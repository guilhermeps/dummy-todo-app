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
            if (input != null && input.IsValid())
            {
                await repository.Add(new Todo(input.Description, input.Owner));
                outputHandler.Handle(new Output
                {
                    Description = input.Description,
                    Owner = input.Owner
                });
                return;
            }

            outputHandler.NotifyError("Todo object is not valid.");
        }
    }
}
