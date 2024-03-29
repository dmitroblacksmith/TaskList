﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskList.WEB.Models
{
    public class ProjectViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreateDate { get; set; }

        public IEnumerable<TaskViewModel> Tasks { get; set; }
    }
}
