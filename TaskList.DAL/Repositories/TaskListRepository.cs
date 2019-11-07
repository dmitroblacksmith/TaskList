using System;
using System.Collections.Generic;
using System.Linq;
using TaskList.DAL.Entities;
using TaskList.DAL.EF;
using TaskList.DAL.Interfaces;
using System.Data.Entity;

namespace TaskList.DAL.Repositories
{
    public class TaskListRepository : IRepository<TaskList.DAL.Entities.TaskList>
    {
        private TaskListContext db;

        public TaskListRepository(TaskListContext context)
        {
            this.db = context;
        }

        public IEnumerable<TaskList.DAL.Entities.TaskList> GetAll()
        {
            return db.TaskLists;
        }

        public TaskList.DAL.Entities.TaskList Get(int id)
        {
            return db.TaskLists.Find(id);
        }

        public void Create(TaskList.DAL.Entities.TaskList taskList)
        {
            db.TaskLists.Add(taskList);
        }

        public void Update(TaskList.DAL.Entities.TaskList taskList)
        {
            db.TaskLists.Update(taskList);
        }

        public IEnumerable<TaskList.DAL.Entities.TaskList> Find(Func<TaskList.DAL.Entities.TaskList, Boolean> predicate)
        {
            return db.TaskLists.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            TaskList.DAL.Entities.TaskList taskList = db.TaskLists.Find(id);
            if (taskList != null)
                db.TaskLists.Remove(taskList);
        }
    }
}
