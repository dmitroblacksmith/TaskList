using System;
using System.Collections.Generic;
using System.Linq;
using TaskList.DAL.Entities;
using TaskList.DAL.EF;
using TaskList.DAL.Interfaces;
using System.Data.Entity;

namespace TaskList.DAL.Repositories
{
    public class TaskRepository : IRepository<Task>
    {
        private TaskListContext db;

        public TaskRepository(TaskListContext context)
        {
            this.db = context;
        }

        public IEnumerable<Task> GetAll()
        {
            return db.Tasks;
        }

        public Task Get(int id)
        {
            return db.Tasks.Find(id);
        }

        public void Create(Task task)
        {
            db.Tasks.Add(task);
        }

        public void Update(Task task)
        {
            db.Tasks.Update(task);
        }

        public IEnumerable<Task> Find(Func<Task, Boolean> predicate)
        {
            return db.Tasks.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Task task = db.Tasks.Find(id);
            if (task != null)
                db.Tasks.Remove(task);
        }
    }
}
