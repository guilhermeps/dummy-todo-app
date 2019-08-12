using DummyTodoApp.Infrastructure.Data.TodoRepository;
using DummyTodoApp.WebApi;
using DummyTodoApp.WebApi.ValueObjects;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DummyTodoApp.IntegrationTests
{
    public class IntegrationTest
    {
        protected readonly HttpClient TestClient;

        protected IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder => {
                    builder.ConfigureServices(services => {
                        services.RemoveAll(typeof(TodoContext));
                        services.AddDbContext<TodoContext>(options => {
                            options.UseInMemoryDatabase("dataBaseTest");
                        });
                    });
                });
            TestClient = appFactory.CreateClient();
        }

        protected async Task CreateTodoItem(TodoVo todo)
        {
            var content = CreateContent(todo);
            var response = await TestClient.PostAsync("/api/todos", content);
            // return response.Content.ReadAsStreamAsync();
        }

        protected ByteArrayContent CreateContent(TodoVo todo)
        {
            var todoSerialized = JsonConvert.SerializeObject(todo);
            var buffer = Encoding.UTF8.GetBytes(todoSerialized);
            var content = new ByteArrayContent(buffer);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return content;
        }
    }
}
