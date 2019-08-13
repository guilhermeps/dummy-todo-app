using DummyTodoApp.Core.Boundaries.GetTodosByOwner;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DummyTodoApp.WebApi.Controllers.GetTodoListByOwner
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        readonly GetTodoListByOwnerPresenter getTodoPresenter;

        public TodosController(GetTodoListByOwnerPresenter presenter) => getTodoPresenter = presenter;

        [HttpGet]
        public async Task<IActionResult> Post(
            [FromServices] IUseCase useCase,
            [FromQuery] string owner)
        {
            await useCase.Execute(owner);
            return getTodoPresenter.ViewModel;
        }
    }
}