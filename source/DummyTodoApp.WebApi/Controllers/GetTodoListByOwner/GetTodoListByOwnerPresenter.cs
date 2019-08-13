using System.Collections.Generic;
using DummyTodoApp.Core.Boundaries.GetTodosByOwner;
using DummyTodoApp.WebApi.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace DummyTodoApp.WebApi.Controllers.GetTodoListByOwner
{
    public sealed class GetTodoListByOwnerPresenter : IOutputHandler
    {
        public IActionResult ViewModel { get; private set; }

        public void Handle(Output output)
        {
            IList<TodoVo> todoList = new List<TodoVo>();
            foreach (var todo in output.TodoList)
                todoList.Add(ToTodoValueObject(todo));
            
            ViewModel = new OkObjectResult(todoList);
        }

        public void NotifyError(string message) => ViewModel = new BadRequestObjectResult(message);

        private TodoVo ToTodoValueObject(OutputItem output) 
        {
            if (output.IsValid())
                return new TodoVo 
                {
                    Description = output.Description,
                    Done = output.Done,
                    Owner = output.Owner
                };
            else return null;
        }
    }
}