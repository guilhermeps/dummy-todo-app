using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DummyTodoApp.Infrastructure.BackgroundServices
{
    public sealed class ReadTodoHostedService : IHostedService, IDisposable
    {
        readonly IServiceProvider Services;
        private Timer timer;

        public ReadTodoHostedService(IServiceProvider services) => Services = services;

        public void Dispose() => timer?.Dispose();

        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private void DoWork(object state) 
        {
            using(var scope= Services.CreateScope())
            {
                var readAllUnreadTodosUseCase = scope.ServiceProvider.GetRequiredService<Core.Boundaries.ReadAllUnreadTodos.IUseCase>();
                readAllUnreadTodosUseCase.Execute();
            }
        }
    }
}