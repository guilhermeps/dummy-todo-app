using DummyTodoApp.Core.Domain;
using DummyTodoApp.Core.UseCases;
using DummyTodoApp.WebApi.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace DummyTodoApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(
            [FromServices] IUseCase<Todo> useCase,
            [FromBody] TodoVo todo)
        {
            Todo newTodo = new Todo(todo.Description, todo.Owner);
            useCase.Execute(newTodo);
            return new CreatedResult($"/api/todos?owner={todo.Owner}", todo);
        }

        [HttpGet]
        public IActionResult Get(
            [FromServices] IUseCase<string, IList<Todo>> useCase,
            [FromQuery] string owner)
        {
            var todos = useCase.Execute(owner);
            IList<TodoVo> todosVoList = new List<TodoVo>();
            todos.ToList().ForEach(t =>
            {
                todosVoList.Add(new TodoVo 
                { 
                    Description = t.Description, 
                    Owner = t.Owner,
                    Read = t.Read
                });
            });
            return Ok(todosVoList);
        }

    }
}