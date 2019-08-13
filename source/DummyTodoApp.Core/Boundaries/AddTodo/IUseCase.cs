using DummyTodoApp.Core.UseCases.AddTodo.Boundaries;
using System.Threading.Tasks;

namespace DummyTodoApp.Core.Boundaries.AddTodo
{
    public interface IUseCase
    {
        Task Execute(Input input);
    }
}
