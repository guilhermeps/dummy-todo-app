using System.Collections.Generic;
using System.Threading.Tasks;

namespace DummyTodoApp.Core.Boundaries.GetTodosByOwner
{
    public interface IUseCase
    {
        Task Execute(string input);
    }
}
