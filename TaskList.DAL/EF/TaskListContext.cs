using System;
using TaskList.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace TaskList.DAL.EF
{
    public class TaskListContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskList.DAL.Entities.TaskList> TaskLists { get; set; }
        
        public TaskListContext(DbContextOptions options)
            : base(options)
        {
            if (this.Database.EnsureCreated())
            {
                this.Projects.Add(new Project { Name = "KiteLTD", CreateDate = new DateTime(2019, 2, 1, 8, 0, 0)});
                this.Projects.Add(new Project { Name = "Afina", CreateDate = new DateTime(2019, 8, 1, 8, 0, 0) });
                this.Projects.Add(new Project { Name = "CCBS", CreateDate = new DateTime(2019, 1, 15, 8, 0, 0) });
                this.SaveChanges();

                foreach (var p in this.Projects)
                {
                    this.Tasks.Add(new Task
                    {
                        Ticket = "Create DAL layer for " + p.Name,
                        Description = "DAL via Repository",
                        CreateDate = new DateTime(2019, 2, 1, 8, 0, 0),
                        ProjectId = p.Id
                    });
                    this.Tasks.Add(new Task
                    {
                        Ticket = "Create BLL layer" + p.Name,
                        Description = "Use DTO",
                        CreateDate = new DateTime(2019, 2, 1, 8, 0, 0),
                        ProjectId = p.Id
                    });
                    this.Tasks.Add(new Task
                    {
                        Ticket = "Create UI layer" + p.Name,
                        Description = "MVC pattern",
                        CreateDate = new DateTime(2019, 2, 1, 8, 0, 0),
                        ProjectId = p.Id
                    });
                }

                this.SaveChanges();
                this.TaskLists.Add(new Entities.TaskList
            {
                CreateTime = new DateTime(2019, 10, 1, 1, 9, 0, 5),
                StartDate = new DateTime(2019, 10, 1, 2, 10, 1, 0),
                CancelDate = new DateTime(2019, 10, 1, 12, 30, 0),
                TaskId = 1
            });
            this.TaskLists.Add(new Entities.TaskList
            {
                CreateTime = new DateTime(2019, 10, 1, 3, 9, 0, 5),
                StartDate = new DateTime(2019, 10, 1, 8, 10, 1, 0),
                CancelDate = new DateTime(2019, 10, 1, 12, 30, 0),
                TaskId = 1
            });
            this.TaskLists.Add(new Entities.TaskList
            {
                CreateTime = new DateTime(2019, 10, 5, 1, 9, 0, 5),
                StartDate = new DateTime(2019, 10, 6, 9, 10, 1, 0),
                CancelDate = new DateTime(2019, 10, 6, 12, 30, 0),
                TaskId = 1
            });
            this.TaskLists.Add(new Entities.TaskList
            {
                CreateTime = new DateTime(2019, 10, 1, 1, 9, 0, 5),
                StartDate = new DateTime(2019, 10, 2, 11, 10, 1, 0),
                CancelDate = new DateTime(2019, 10, 2, 12, 30, 0),
                TaskId = 2
            });
            this.TaskLists.Add(new Entities.TaskList
            {
                CreateTime = new DateTime(2019, 10, 1, 3, 9, 0, 5),
                StartDate = new DateTime(2019, 10, 7, 7, 10, 1, 0),
                CancelDate = new DateTime(2019, 10, 7, 12, 30, 0),
                TaskId = 2
            });
                this.SaveChanges();
            }

        }
    }
}
