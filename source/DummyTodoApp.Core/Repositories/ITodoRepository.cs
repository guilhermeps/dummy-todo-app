using DummyTodoApp.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DummyTodoApp.Core.Repositories
{
    public interface ITodoRepository
    {
        Task Add(Todo todo);

        Task<IList<Todo>> Get(string owner);

        Task<Todo> Get(Guid id);
    }
}
