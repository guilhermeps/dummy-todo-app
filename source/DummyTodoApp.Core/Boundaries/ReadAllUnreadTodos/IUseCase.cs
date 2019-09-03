using System.Threading.Tasks;

namespace DummyTodoApp.Core.Boundaries.ReadAllUnreadTodos
{
    public interface IUseCase
    {
         Task Execute();
    }
}