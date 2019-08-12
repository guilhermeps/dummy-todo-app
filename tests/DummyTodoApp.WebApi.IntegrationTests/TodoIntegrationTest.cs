using DummyTodoApp.WebApi.ValueObjects;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace DummyTodoApp.IntegrationTests
{
    public class TodoIntegrationTest : IntegrationTest
    {
        [Fact(DisplayName = "DEVE retornar uma lista contendo um TodoItem")]
        public async Task ShouldReturnAListOfTodoItemsWithOneTodoItem()
        {
            var todoItem = new TodoVo { Description = "test todo", Owner = "Guilherme" };
            await CreateTodoItem(todoItem);
            var response = await TestClient.GetAsync($"/api/todos?owner={todoItem.Owner}");

            IList<TodoVo> todoList = JsonConvert.DeserializeObject<IList<TodoVo>>(await response.Content.ReadAsStringAsync());

            Assert.Equal(200, (int)response.StatusCode);
            Assert.Equal(1, todoList.Count);
        }

        [Fact(DisplayName = "DEVE adicionar um item de Todo")]
        public async Task ShouldAddATodoIntoTodoList()
        {
            var todoItem = new TodoVo { Description = "test todo", Owner = "Guilherme" };
            var content = CreateContent(todoItem);
            var response = await TestClient.PostAsync("/api/todos", content);

            Assert.Equal(201, (int)response.StatusCode);
        }
    }
}
