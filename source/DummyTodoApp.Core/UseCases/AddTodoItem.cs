using DummyTodoApp.Core.Domain;
using DummyTodoApp.Core.Repositories;
using System;

namespace DummyTodoApp.Core.UseCases
{
    public sealed class AddTodoItem : IUseCase<Todo>
    {
        readonly ITodoRepository repository;

        public AddTodoItem(ITodoRepository repo) => repository = repo;

        public void Execute(Todo input)
        {
            if (input.IsValid())
                repository.Add(input);
            else
                throw new Exception("Invalid todo");
        }
    }
}
