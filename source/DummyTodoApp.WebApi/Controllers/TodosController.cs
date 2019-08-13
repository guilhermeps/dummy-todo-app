using DummyTodoApp.Core.Boundaries.AddTodo;
using DummyTodoApp.Core.Domain;
using DummyTodoApp.Core.UseCases;
using DummyTodoApp.Core.UseCases.AddTodo.Boundaries;
using DummyTodoApp.WebApi.Presenter;
using DummyTodoApp.WebApi.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DummyTodoApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        readonly AddAccountPresenter addAccountPresenter;

        public TodosController(AddAccountPresenter presenter) => addAccountPresenter = presenter;

        [HttpPost]
        public async Task<IActionResult> Post(
            [FromServices] IUseCase useCase,
            [FromBody] TodoVo todo)
        {
            await useCase.Execute(new Input { Description = todo.Description, Owner = todo.Owner });
            return addAccountPresenter.ViewModel;
        }

        //[HttpGet]
        //public IActionResult Get(
        //    [FromServices] IUseCase<string, IList<Todo>> useCase,
        //    [FromQuery] string owner)
        //{
        //    var todos = useCase.Execute(owner);
        //    IList<TodoVo> todosVoList = new List<TodoVo>();
        //    todos.ToList().ForEach(t =>
        //    {
        //        todosVoList.Add(new TodoVo { Description = t.Description, Owner = t.Owner });
        //    });
        //    return Ok(todosVoList);
        //}

    }
}