using System;
using TaskList.BLL.Interfaces;
using TaskList.BLL.DTO;
using TaskList.DAL.Interfaces;
using TaskList.DAL.Entities;
using TaskList.BLL.Infrastructure;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace TaskList.BLL.Services
{
    public class TaskService : ITaskService
    {
        IUnitOfWork DataBase { get; set; }

        public TaskService(IUnitOfWork unitOfWork)
        {
            DataBase = unitOfWork;
        }

        public void CreateTask(TaskDTO taskDto)
        {
            if (taskDto.Ticket != String.Empty)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<TaskDTO, Task>());
                IMapper mapper = config.CreateMapper();
                Task task = mapper.Map<TaskDTO, Task>(taskDto);
                DataBase.Tasks.Create(task);
                DataBase.Save();
            }
            else
            {
                throw new ValidationException("The 'Ticket' fields musn't be empty!", "EmptyTicketField");
            }
        }
        
        public IEnumerable<TaskDTO> GetTasksByProjectId(int projectId)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Task, TaskDTO>());
            IMapper mapper = config.CreateMapper();
            IEnumerable<TaskDTO> TaskDTOs = DataBase.Tasks.GetAll().Where(t => t.ProjectId == projectId).Select(t => mapper.Map<Task, TaskDTO>(t));
            return TaskDTOs;
        }

        public IEnumerable<TaskDTO> GetAllTasks()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Task, TaskDTO>());
            IMapper mapper = config.CreateMapper();
            IEnumerable<TaskDTO> TaskDTOs = DataBase.Tasks.GetAll().Select(t => mapper.Map<Task, TaskDTO>(t));
            return TaskDTOs;
        }

        public void UpdateTask(TaskDTO taskDto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TaskDTO, Task>());
            IMapper mapper = config.CreateMapper();
            Task task = mapper.Map<TaskDTO, Task>(taskDto);
            DataBase.Tasks.Update(task);
            DataBase.Save();
        }

        public void DeleteTask(int taskId)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Task, TaskDTO>());
            IMapper mapper = config.CreateMapper();
            TaskDTO taskDto = mapper.Map<Task, TaskDTO>(DataBase.Tasks.Get(taskId));
            config = new MapperConfiguration(cfg => cfg.CreateMap<TaskList.DAL.Entities.TaskList, TaskListDTO>());
            mapper = config.CreateMapper();
            taskDto.TaskList = DataBase.TaskLists.GetAll().Where(tl => tl.TaskId == taskDto.Id).Select(tl => mapper.Map<TaskList.DAL.Entities.TaskList, TaskListDTO>(tl));
            DataBase.Tasks.Delete(taskDto.Id);
            DataBase.Save();
        }

        public void Dispose()
        {
            DataBase.Dispose();
        }
    }
}
