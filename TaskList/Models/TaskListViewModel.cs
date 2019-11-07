using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskList.WEB.Models
{
    public class TaskListViewModel
    {
        public int Id { get; set; }

        public int TaskId { get; set; }

        public TaskViewModel Task { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime CancelDate { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
