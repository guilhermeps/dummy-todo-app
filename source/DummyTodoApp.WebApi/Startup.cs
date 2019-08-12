using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DummyTodoApp.Core.Domain;
using DummyTodoApp.Core.UseCases;
using DummyTodoApp.Core.Repositories;
using DummyTodoApp.Infrastructure.Data.TodoRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using DummyTodoApp.Infrastructure.BackgroundServices;

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
            services.AddTransient<ITodoRepository, TodoRepository>();
            services.AddScoped<IUseCase<Todo>, AddTodoItem>();
            services.AddScoped<IUseCase<string, IList<Todo>>, GetAllTodos>();
            services.AddDbContext<TodoContext>(options =>
            {
                options.UseInMemoryDatabase("Database_Production");
            });
            services.AddHostedService<ReadTodoService>();
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
    }
}
