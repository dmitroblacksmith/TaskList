using System;
using System.Collections.Generic;
using TaskList.BLL.DTO;

namespace TaskList.BLL.Interfaces
{
    public interface ITaskService
    {
        void CreateTask(TaskDTO taskDto);
        IEnumerable<TaskDTO> GetTasksByProjectId(int projectId);
        IEnumerable<TaskDTO> GetAllTasks();
        void UpdateTask(TaskDTO taskDto);
        void DeleteTask(int taskId);
        void Dispose();
    }
}
