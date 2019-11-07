using System;

namespace TaskList.BLL.DTO
{
    public class TaskListDTO
    {
        public int Id { get; set; }

        public int TaskId { get; set; }
        
        public TaskDTO Task { get; set; }

        public DateTime StartDate { get; set; }
        
        public DateTime CancelDate { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
