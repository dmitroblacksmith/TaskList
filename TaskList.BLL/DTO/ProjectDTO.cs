using System;
using System.Collections.Generic;

namespace TaskList.BLL.DTO
{
    public class ProjectDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreateDate { get; set; }

        public IEnumerable<TaskDTO> Tasks { get; set; }
    }
}
