using System;
using System.Threading;
using System.Threading.Tasks;
using DummyTodoApp.Core.Repositories;
using Microsoft.Extensions.Hosting;

namespace DummyTodoApp.Infrastructure.BackgroundServices
{
    public class ReadTodoService : IHostedService, IDisposable
    {
        readonly ITodoRepository repository;
        private Timer timer;

        public ReadTodoService(ITodoRepository repo) => repository = repo;

        public void Dispose()
        {
            timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(60));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private void DoWork(object state) 
        {
            var todoList = repository.GetAllUnreadTodos();
            
            foreach (var todo in todoList)
            {
                todo.Read = true;
                repository.Update(todo);
            }
        }
    }
}