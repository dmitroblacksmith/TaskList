using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskList.DAL.Entities
{
    public class TaskList
    {
        [Key]
        public int Id { get; set; }

        public int TaskId { get; set; }

        [ForeignKey("TaskId")]
        public Task Task { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime CancelDate { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
