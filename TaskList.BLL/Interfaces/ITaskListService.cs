using System;
using System.Collections.Generic;
using TaskList.BLL.DTO;

namespace TaskList.BLL.Interfaces
{
    public interface ITaskListService
    {
        void CreateTaskList(TaskListDTO taskListDto);
        TimeSpan CountDuration(int tasklistId);
        IEnumerable<TaskListDTO> GetAllTaskLists();
        void SetStartDate(TaskListDTO taskListDto);
        void SetCancelDate(TaskListDTO taskListDto);
    }
}
