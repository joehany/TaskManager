using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Infrastructure.Data.Entities
{
    public class TaskList
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public ICollection<TaskItem> TaskItems { get; set; } 
    }
}
