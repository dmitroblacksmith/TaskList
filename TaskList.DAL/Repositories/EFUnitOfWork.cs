using System;
using TaskList.DAL.EF;
using TaskList.DAL.Interfaces;
using TaskList.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace TaskList.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private TaskListContext db;
        private IRepository<Project> projectRepository;
        private IRepository<Task> taskRepository;
        private IRepository<TaskList.DAL.Entities.TaskList> tasklistRepository;

        public EFUnitOfWork(DbContextOptions options)
        {
            db = new TaskListContext(options);
        }
        public IRepository<Project> Projects
        {
            get
            {
                if (projectRepository == null)
                    projectRepository = new ProjectRepository(db);
                return projectRepository;
            }
        }

        public IRepository<Task> Tasks
        {
            get
            {
                if (taskRepository == null)
                    taskRepository = new TaskRepository(db);
                return taskRepository;
            }
        }

        public IRepository<TaskList.DAL.Entities.TaskList> TaskLists
        {
            get
            {
                if (tasklistRepository == null)
                    tasklistRepository = new TaskListRepository(db);
                return tasklistRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}