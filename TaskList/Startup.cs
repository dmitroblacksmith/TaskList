using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Web.Mvc;
using Microsoft.Extensions.Configuration;
using TaskList.DAL.Interfaces;
using TaskList.BLL.Interfaces;
using TaskList.DAL.EF;
using Microsoft.EntityFrameworkCore;
using TaskList.BLL.Services;
using TaskList.DAL.Repositories;

namespace TaskList.WEB
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("SQLServerDB");
            services.AddDbContext<TaskListContext>(options => options.UseSqlServer(connectionString));
            services.AddTransient<IUnitOfWork, EFUnitOfWork>();
            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<ITaskService, TaskService>();
            services.AddTransient<ITaskListService, TaskListService>();
            services.AddTransient<ITimePlannerService, TimePlannerService>();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
