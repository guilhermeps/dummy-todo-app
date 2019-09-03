using DummyTodoApp.Core.Repositories;
using DummyTodoApp.Core.UseCases;
using DummyTodoApp.Infrastructure.BackgroundServices;
using DummyTodoApp.Infrastructure.Data.TodoRepository;
using DummyTodoApp.WebApi.ActionFilters;
using DummyTodoApp.WebApi.Settings;
using DummyTodoApp.WebApi.StartupFilters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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
            AddBackgroundServices(services);
            services.AddMvc(options =>
            {
                options.Filters.Add(new CustomExceptionFilterAttribute());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            AddSettings(services);
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
            services.AddScoped<DummyTodoApp.Core.Boundaries.AddTodo.IUseCase, AddTodoItem>();
            services.AddScoped<DummyTodoApp.WebApi.Controllers.AddTodo.AddTodoPresenter, DummyTodoApp.WebApi.Controllers.AddTodo.AddTodoPresenter>();
            services.AddScoped<DummyTodoApp.Core.Boundaries.AddTodo.IOutputHandler>(s => 
                s.GetRequiredService<DummyTodoApp.WebApi.Controllers.AddTodo.AddTodoPresenter>());
            
            services.AddScoped<DummyTodoApp.Core.Boundaries.GetTodosByOwner.IUseCase, GetAllTodos>();
            services.AddScoped<DummyTodoApp.WebApi.Controllers.GetTodoListByOwner.GetTodoListByOwnerPresenter, DummyTodoApp.WebApi.Controllers.GetTodoListByOwner.GetTodoListByOwnerPresenter>();
            services.AddScoped<DummyTodoApp.Core.Boundaries.GetTodosByOwner.IOutputHandler>(s => 
                s.GetRequiredService<DummyTodoApp.WebApi.Controllers.GetTodoListByOwner.GetTodoListByOwnerPresenter>());

            services.AddScoped<DummyTodoApp.Core.Boundaries.ReadAllUnreadTodos.IUseCase, ReadAllUnreadTodos>();
        }

        private void AddDummyTodoAppDatabase(IServiceCollection services)
        {
            services.AddTransient<ITodoRepository, TodoRepository>();
            services.AddDbContext<TodoContext>(options =>
            {
                options.UseInMemoryDatabase("Database_Production");
            });
        }

        private void AddSettings(IServiceCollection services)
        {
            services.AddTransient<IStartupFilter, SettingsValidationStartupFilter>();
            services.Configure<DummySettings>(Configuration.GetSection("DummyConfiguration"));
            services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<DummySettings>>().Value);
            services.AddSingleton<IValidatable>(resolver => 
                resolver.GetRequiredService<IOptions<DummySettings>>().Value);
        }

        private void AddBackgroundServices(IServiceCollection services)
        {
            services.AddHostedService<ReadTodoHostedService>();
        }
    }
}
