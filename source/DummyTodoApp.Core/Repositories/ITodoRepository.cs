using DummyTodoApp.Core.Domain;
using System;
using System.Collections.Generic;

namespace DummyTodoApp.Core.Repositories
{
    public interface ITodoRepository
    {
        void Add(Todo todo);

        IList<Todo> Get(string owner);

        Todo Get(Guid id);

        IList<Todo> GetAllUnreadTodos();

        void Update(Todo todo);

        void Remove(Guid id);
    }
}
