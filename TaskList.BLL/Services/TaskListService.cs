using System;
using System.Linq;
using System.Collections.Generic;
using TaskList.DAL.Entities;
using TaskList.BLL.Interfaces;
using TaskList.BLL.DTO;
using TaskList.DAL.Interfaces;
using AutoMapper;

namespace TaskList.BLL.Services
{
    public class TaskListService : ITaskListService
    {
        private IUnitOfWork DataBase { get; set; }

        public TaskListService(IUnitOfWork unitOfWork)
        {
            DataBase = unitOfWork;
        }

        public void CreateTaskList(TaskListDTO taskListDto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TaskListDTO, TaskList.DAL.Entities.TaskList>());
            IMapper mapper = config.CreateMapper();
            TaskList.DAL.Entities.TaskList taskList = mapper.Map<TaskListDTO, TaskList.DAL.Entities.TaskList>(taskListDto);
            DataBase.TaskLists.Create(taskList);
            DataBase.Save();
        }

        public TimeSpan CountDuration(int tasklistId)
        {
            TaskList.DAL.Entities.TaskList taskList = DataBase.TaskLists.Get(tasklistId);
            if((taskList.StartDate != null) && (taskList.CancelDate != DateTime.MinValue))
            {
                return taskList.CancelDate - taskList.StartDate;
            }
            else
            {
                return DateTime.Now - taskList.StartDate;
            }
        }

        public void SetStartDate(TaskListDTO taskListDto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TaskListDTO, TaskList.DAL.Entities.TaskList>());
            IMapper mapper = config.CreateMapper();
            taskListDto.CancelDate = DateTime.MinValue;
            TaskList.DAL.Entities.TaskList taskList = mapper.Map<TaskListDTO, TaskList.DAL.Entities.TaskList>(taskListDto);
            DataBase.TaskLists.Create(taskList);
            DataBase.Save();
        }

        public void SetCancelDate(TaskListDTO taskListDto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TaskListDTO, TaskList.DAL.Entities.TaskList>());
            IMapper mapper = config.CreateMapper();
            TaskList.DAL.Entities.TaskList taskList = mapper.Map<TaskListDTO, TaskList.DAL.Entities.TaskList>(taskListDto);
            DataBase.TaskLists.Update(taskList);
            DataBase.Save();
        }

        public IEnumerable<TaskListDTO> GetAllTaskLists()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TaskList.DAL.Entities.TaskList, TaskListDTO>());
            IMapper mapper = config.CreateMapper();
            IEnumerable<TaskListDTO> TaskDTOs = DataBase.TaskLists.GetAll().Select(t => mapper.Map<TaskList.DAL.Entities.TaskList, TaskListDTO>(t));
            return TaskDTOs;
        }
    }
}
