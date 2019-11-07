using System;
using System.Collections.Generic;
using System.Text;

namespace TaskList.BLL.Interfaces
{
    public interface ITimePlannerService
    {
        TimeSpan GetTaskTime(int taskId);
        TimeSpan GetProjectTime(int projectId);
        DateTime GetStartTime(int projectId);
        DateTime GetFinishDate(int taskId);
        bool GetState(int taskId);
    }
}
