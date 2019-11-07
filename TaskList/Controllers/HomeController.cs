using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ninject;
using Ninject.Modules;
using Microsoft.AspNetCore.Mvc;
using TaskList.BLL.Interfaces;
using TaskList.BLL.DTO;
using TaskList.BLL.Infrastructure;
using TaskList.WEB.Util;
using Microsoft.Extensions.Configuration;
using TaskList.WEB.Models;
using AutoMapper;

namespace TaskList.WEB.Controllers
{
    public class HomeController : Controller
    {
        IProjectService projectService;
        ITaskService taskService;
        ITaskListService taskListService;
        ITimePlannerService timePlannerService;

        IConfiguration Configuration { get; set; }

        public HomeController(IProjectService projectService, ITaskService taskService, ITaskListService taskListService, ITimePlannerService timePlannerService)
        {
            this.projectService = projectService;
            this.taskService = taskService;
            this.taskListService = taskListService;
            this.timePlannerService = timePlannerService;
        }

        public IActionResult Index()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ProjectDTO, ProjectViewModel>());
            IMapper mapper = config.CreateMapper();
            IEnumerable<ProjectViewModel> projectVMs = projectService.GetAllProjects().Select(p => mapper.Map<ProjectDTO, ProjectViewModel>(p));
            return View(projectVMs);
        }

        [HttpGet]
        public IActionResult ShowTasks(int projectId)
        {
            IEnumerable<TaskDTO> tasks = taskService.GetTasksByProjectId(projectId);
            IEnumerable<TaskViewModel> taskVMs = tasks.Select(t => this.MapTaskToViewModel(t));
            return PartialView(taskVMs);
        }

        [HttpGet]
        public IActionResult GetTotalTime(int projectId)
        {
            ViewData["ProjectTime"] = timePlannerService.GetProjectTime(projectId).ToString(@"hh\:mm");
            return PartialView();
        }

        [HttpGet]
        public IActionResult EditTask(int taskId)
        {
            return PartialView(this.MapTaskToViewModel(taskService.GetAllTasks().Where(t => t.Id == taskId).First()));
        }

        private TaskViewModel MapTaskToViewModel(TaskDTO taskDto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TaskDTO, TaskViewModel>()
                .ForMember(dest => dest.Longtitude, opt => opt.MapFrom(dto => timePlannerService.GetTaskTime(dto.Id)))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(dto => timePlannerService.GetStartTime(dto.Id)))
                .ForMember(dest => dest.FinishDate, opt => opt.MapFrom(dto => timePlannerService.GetFinishDate(dto.Id)))
                .ForMember(dest => dest.IsStarted, opt => opt.MapFrom(dto => timePlannerService.GetState(dto.Id))));
            IMapper mapper = config.CreateMapper();
            return mapper.Map<TaskDTO, TaskViewModel>(taskDto);
        }

        [HttpPost]
        public void SaveTask(TaskViewModel taskVm)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TaskViewModel, TaskDTO>());
            IMapper mapper = config.CreateMapper();
            taskService.UpdateTask(mapper.Map<TaskViewModel, TaskDTO>(taskVm));
        }

        [HttpGet]
        public IActionResult SetStartTime(int taskId)
        {
            TaskListViewModel taskList = new TaskListViewModel() { TaskId = taskId };
            ViewData["Ticket"] = taskService.GetAllTasks().Where(t => t.Id == taskId).FirstOrDefault().Ticket;
            return PartialView(taskList);
        }

        [HttpGet]
        public IActionResult SetFinishTime(int taskId)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TaskListDTO, TaskListViewModel>());
            IMapper mapper = config.CreateMapper();
            TaskListDTO taskList = taskListService.GetAllTaskLists().Where(tl => tl.TaskId == taskId).LastOrDefault();
            ViewData["Ticket"] = taskService.GetAllTasks().Where(t => t.Id == taskId).FirstOrDefault().Ticket;
            return PartialView(mapper.Map<TaskListDTO, TaskListViewModel>(taskList));
        }

        [HttpPost]
        public void SaveStartTime(TaskListViewModel taskListVm)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TaskListViewModel, TaskListDTO>());
            IMapper mapper = config.CreateMapper();
            taskListService.SetStartDate(mapper.Map<TaskListViewModel, TaskListDTO>(taskListVm));
        }

        [HttpPost]
        public void SaveFinishTime(TaskListViewModel taskListVm)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TaskListViewModel, TaskListDTO>());
            IMapper mapper = config.CreateMapper();
            taskListService.SetCancelDate(mapper.Map<TaskListViewModel, TaskListDTO>(taskListVm));
        }

        [HttpPost]
        public void DeleteTask(int taskId)
        {
            taskService.DeleteTask(taskId);
        }

        [HttpGet]
        public IActionResult AddTask(int projectId)
        {
            return PartialView(new TaskViewModel() { ProjectId = projectId});
        }

        [HttpPost]
        public void SaveNewTask(TaskViewModel taskVm)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TaskViewModel, TaskDTO>());
            IMapper mapper = config.CreateMapper();
            taskService.CreateTask(mapper.Map<TaskViewModel, TaskDTO>(taskVm));
        }
    }
}