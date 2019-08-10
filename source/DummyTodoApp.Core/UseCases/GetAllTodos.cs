using DummyTodoApp.Core.Domain;
using DummyTodoApp.Core.Repositories;
using System;
using System.Collections.Generic;

namespace DummyTodoApp.Core.UseCases
{
    public sealed class GetAllTodos : IUseCase<string, IList<Todo>>
    {
        readonly ITodoRepository repository;

        public GetAllTodos(ITodoRepository repo) => repository = repo;

        public IList<Todo> Execute(string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
                return repository.Get(input);
            else
                throw new Exception("Todos not found");
        }
    }
}
