using System.Threading.Tasks;
using DummyTodoApp.Core.Boundaries.ReadAllUnreadTodos;
using DummyTodoApp.Core.Repositories;

namespace DummyTodoApp.Core.UseCases
{
    public sealed class ReadAllUnreadTodos : IUseCase
    {
        readonly ITodoRepository todoRepository;

        public ReadAllUnreadTodos(ITodoRepository repo) => todoRepository = repo;

        public async Task Execute()
        {
            var allUnreadTodos = await todoRepository.GetAllUnreadTodos();

            foreach (var todo in allUnreadTodos)
            {
                todo.Done = true;
                await todoRepository.Update(todo);
            }
        }
    }
}