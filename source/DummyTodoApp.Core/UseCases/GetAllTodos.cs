using DummyTodoApp.Core.Boundaries.GetTodosByOwner;
using DummyTodoApp.Core.Repositories;
using System.Threading.Tasks;

namespace DummyTodoApp.Core.UseCases
{
    public sealed class GetAllTodos : IUseCase
    {
        readonly ITodoRepository repository;
        readonly IOutputHandler outputHandler;

        public GetAllTodos(
            ITodoRepository repo,
            IOutputHandler handler)
        {
            repository = repo;
            outputHandler = handler;
        }

        public Task Execute(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                outputHandler.NotifyError("Owner does not exist");
                return;
            }

            var todoList = repository.Get(input);
            outputHandler.Handle(new Output(todoList.Result));            
        }
    }
}
