using System;
using System.Collections.Generic;
using TaskList.BLL.Interfaces;
using TaskList.BLL.DTO;
using TaskList.DAL.Interfaces;
using TaskList.DAL.Entities;
using System.Linq;
using AutoMapper;

namespace TaskList.BLL.Services
{
    public class ProjectService : IProjectService
    { 
        IUnitOfWork DataBase { get; set; }

        public ProjectService(IUnitOfWork unitOfWork)
        {
            DataBase = unitOfWork;
        }

        public void CreateProject(ProjectDTO projectDto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ProjectDTO, Project>());
            IMapper mapper = config.CreateMapper();
            Project project = mapper.Map<ProjectDTO, Project>(projectDto);
            DataBase.Projects.Create(project);
            DataBase.Save();
        }

        public IEnumerable<ProjectDTO> GetAllProjects()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Project, ProjectDTO>());
            IMapper mapper = config.CreateMapper();
            IEnumerable<ProjectDTO> ProjectDTOs = DataBase.Projects.GetAll().Select(t => mapper.Map<Project, ProjectDTO>(t));
            return ProjectDTOs;
        }
        
        public void Dispose()
        {
            DataBase.Dispose();
        }
    }

}
