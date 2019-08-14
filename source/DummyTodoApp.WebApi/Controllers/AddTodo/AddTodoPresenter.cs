using DummyTodoApp.Core.Boundaries.AddTodo;
using DummyTodoApp.Core.UseCases.AddTodo.Boundaries;
using DummyTodoApp.WebApi.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace DummyTodoApp.WebApi.Controllers.AddTodo
{
    public sealed class AddTodoPresenter : IOutputHandler
    {
        public IActionResult ViewModel { get; private set; }

        public void Handle(Output output)
        {
            var todo = ToTodoValueObject(output);
            ViewModel = new CreatedResult($"/api/todos?owner={todo.Owner}", todo);
        }

        public void NotifyError(string errorMessage)
        {
            ViewModel = new BadRequestObjectResult(new { message = errorMessage });
        }

        private TodoVo ToTodoValueObject(Output output)
        {
            if (output.IsValid())
                return new TodoVo
                {
                    Description = output.Description,
                    Owner = output.Owner,
                    Done = output.Done
                };
            else return null;                
        }
    }
}
