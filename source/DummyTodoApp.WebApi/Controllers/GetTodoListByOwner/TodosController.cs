using DummyTodoApp.Core.Boundaries.GetTodosByOwner;
using DummyTodoApp.WebApi.Settings;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DummyTodoApp.WebApi.Controllers.GetTodoListByOwner
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        readonly GetTodoListByOwnerPresenter getTodoPresenter;
        readonly DummySettings dummySettings;

        public TodosController(
            GetTodoListByOwnerPresenter presenter,
            DummySettings settings) 
        {
            getTodoPresenter = presenter;
            dummySettings = settings;
        }

        [HttpGet]
        public async Task<IActionResult> Get(
            [FromServices] IUseCase useCase,
            [FromQuery] string owner)
        {
            if (dummySettings.IsOpenToGetTodos) 
            {
                await useCase.Execute(owner);
                return getTodoPresenter.ViewModel;
            }
            else
            {
                return Ok(new { message = "Today is not opened to get todos."});
            }            
        }
    }
}