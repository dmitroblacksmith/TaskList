using System;
using System.Collections.Generic;

namespace TaskList.BLL.DTO
{
    public class TaskDTO
    {
        public int Id { get; set; }
        
        public string Ticket { get; set; }

        public string Description { get; set; }

        public int ProjectId { get; set; }
        
        public ProjectDTO Project { get; set; }

        public DateTime CreateDate { get; set; }

        public IEnumerable<TaskListDTO> TaskList { get; set; }
    }
}
