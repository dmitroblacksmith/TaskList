using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskList.WEB.Models
{
    public class TaskViewModel
    {
        public int Id { get; set; }

        public string Ticket { get; set; }

        public string Description { get; set; }

        public int ProjectId { get; set; }

        public ProjectViewModel Project { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime StartDate { get; set; }
        
        public DateTime FinishDate { get; set; }

        public IEnumerable<TaskListViewModel> TaskList { get; set; }

        public TimeSpan Longtitude { get; set; }

        public bool IsStarted { get; set; }
    }
}
