using System;
using System.Collections.Generic;
using System.Text;
using TaskList.DAL.Entities;

namespace TaskList.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Project> Projects { get; }
        IRepository<Task> Tasks { get; }
        IRepository<TaskList.DAL.Entities.TaskList> TaskLists { get; }
        void Save();
    }
}
