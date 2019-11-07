using System;
using System.Collections.Generic;
using System.Linq;
using TaskList.DAL.Entities;
using TaskList.DAL.EF;
using TaskList.DAL.Interfaces;
using System.Data.Entity;

namespace TaskList.DAL.Repositories
{
    public class ProjectRepository : IRepository<Project>
    {
        private TaskListContext db;

        public ProjectRepository(TaskListContext context)
        {
            this.db = context;
        }

        public IEnumerable<Project> GetAll()
        {
            return db.Projects;
        }

        public Project Get(int id)
        {
            return db.Projects.Find(id);
        }

        public void Create(Project project)
        {
            db.Projects.Add(project);
        }

        public void Update(Project project)
        {
            db.Projects.Update(project);
        }

        public IEnumerable<Project> Find(Func<Project, Boolean> predicate)
        {
            return db.Projects.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Project project = db.Projects.Find(id);
            if (project != null)
                db.Projects.Remove(project);
        }
    }
}
