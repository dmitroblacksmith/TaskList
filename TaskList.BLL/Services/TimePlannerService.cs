using System;
using System.Collections.Generic;
using System.Text;
using TaskList.BLL.Interfaces;
using TaskList.BLL.DTO;
using TaskList.DAL.Entities;
using TaskList.DAL.Interfaces;
using System.Linq;
using AutoMapper;

namespace TaskList.BLL.Services
{
    public class TimePlannerService : ITimePlannerService
    {
        IUnitOfWork DataBase { get; set; }

        public TimePlannerService(IUnitOfWork unitOfWork)
        {
            DataBase = unitOfWork;
        }

        public TimeSpan GetTaskTime(int taskId)
        {
            TimeSpan taskTime = TimeSpan.Zero;
            IEnumerable<TaskList.DAL.Entities.TaskList> taskLists = DataBase.TaskLists.GetAll().Where(tl => tl.TaskId == taskId);
            IEnumerable<TimeSpan> times = taskLists.Select(t => GetTaskListTime(t));
            foreach (var time in times)
            {
                taskTime += time;
            }
            return taskTime;
        }

        public TimeSpan GetProjectTime(int projectId)
        {
            TimeSpan projectTime = TimeSpan.Zero;
            IEnumerable<Task> tasks = DataBase.Tasks.GetAll().Where(t => t.ProjectId == projectId);
            IEnumerable<TimeSpan> times = tasks.Select(t => GetTaskTime(t.Id));
            foreach (var time in times)
            {
                projectTime += time;
            }
            return projectTime;
        }

        private TimeSpan GetTaskListTime(TaskList.DAL.Entities.TaskList taskList)
        {
            if (taskList.CancelDate == DateTime.MinValue)
            {
                return DateTime.Now.Subtract(taskList.StartDate);
            }
            else
            {
                return taskList.CancelDate.Subtract(taskList.StartDate);
            }
        }
        
        public DateTime GetStartTime(int taskId)
        {
            DateTime startDate = new DateTime();
            if (DataBase.TaskLists.GetAll().Where(tl => tl.TaskId == taskId).Any() == false)
            {
                return startDate;
            }
            else
            {
                return DataBase.TaskLists.GetAll().Where(tl => tl.TaskId == taskId).FirstOrDefault().StartDate;
            }
        }

        public DateTime GetFinishDate(int taskId)
        {
            DateTime cancelDate = new DateTime();
            if (DataBase.TaskLists.GetAll().Where(tl => tl.TaskId == taskId).Any() == false)
            {
                return new DateTime();
            }
            else if((cancelDate = DataBase.TaskLists.GetAll().Where(tl => tl.TaskId == taskId).LastOrDefault().CancelDate) != DateTime.MinValue)
            {
                return cancelDate;
            }
            else
            {
                return new DateTime();
            }
        }

        public bool GetState(int taskId)
        {
            if(DataBase.TaskLists.GetAll().Where(tl => tl.TaskId == taskId).Any() == false)
            {
                return false;
            }
            else if((DataBase.TaskLists.GetAll().Where(tl => tl.TaskId == taskId).LastOrDefault().CancelDate != null) &&
                    (DataBase.TaskLists.GetAll().Where(tl => tl.TaskId == taskId).LastOrDefault().CancelDate != DateTime.MinValue))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
