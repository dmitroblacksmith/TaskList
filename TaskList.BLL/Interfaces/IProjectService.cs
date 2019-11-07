using System;
using System.Collections.Generic;
using TaskList.BLL.DTO;

namespace TaskList.BLL.Interfaces
{
    public interface IProjectService
    {
        void CreateProject(ProjectDTO projectDto);
        IEnumerable<ProjectDTO> GetAllProjects();
        void Dispose();
    }
}
