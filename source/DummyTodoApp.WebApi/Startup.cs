using DummyTodoApp.Core.Repositories;
using DummyTodoApp.Core.UseCases;
using DummyTodoApp.Infrastructure.Data.TodoRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DummyTodoApp.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AddDummyTodoAppDatabase(services);
            AddDummyTodoAppCore(services);

            // services.AddScoped<IUseCase<string, IList<Todo>>, GetAllTodos>();
            
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                    });
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseMvc();
        }

        private void AddDummyTodoAppCore(IServiceCollection services)
        {
            services.AddScoped<DummyTodoApp.Core.UseCases.AddTodo.Boundaries.IUseCase, AddTodoItem>();
            services.AddScoped<DummyTodoApp.WebApi.Presenter.AddAccountPresenter, DummyTodoApp.WebApi.Presenter.AddAccountPresenter>();
            services.AddScoped<DummyTodoApp.Core.UseCases.AddTodo.Boundaries.IOutputHandler>(s => 
                s.GetRequiredService<DummyTodoApp.WebApi.Presenter.AddAccountPresenter>());
        }

        private void AddDummyTodoAppDatabase(IServiceCollection services)
        {
            services.AddTransient<ITodoRepository, TodoRepository>();
            services.AddDbContext<TodoContext>(options =>
            {
                options.UseInMemoryDatabase("Database_Production");
            });
        }
    }
}
