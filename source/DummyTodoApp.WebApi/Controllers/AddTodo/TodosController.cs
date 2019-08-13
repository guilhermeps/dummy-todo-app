using DummyTodoApp.Core.Boundaries.AddTodo;
using DummyTodoApp.Core.UseCases.AddTodo.Boundaries;
using DummyTodoApp.WebApi.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DummyTodoApp.WebApi.Controllers.AddTodo
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        readonly AddTodoPresenter addTodoPresenter;

        public TodosController(AddTodoPresenter presenter) => addTodoPresenter = presenter;

        [HttpPost]
        public async Task<IActionResult> Post(
            [FromServices] IUseCase useCase,
            [FromBody] TodoVo todo)
        {
            await useCase.Execute(new Input { Description = todo.Description, Owner = todo.Owner });
            return addTodoPresenter.ViewModel;
        }
    }
}