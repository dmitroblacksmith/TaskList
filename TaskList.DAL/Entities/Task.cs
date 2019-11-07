using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskList.DAL.Entities
{
    public class Task
    {
        public int Id { get; set; }

        public string Ticket { get; set; }

        public string Description { get; set; }

        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public Project Project { get; set; }

        public DateTime CreateDate { get; set; }

        public IEnumerable<TaskList> TaskList { get; set;}
    }
}
