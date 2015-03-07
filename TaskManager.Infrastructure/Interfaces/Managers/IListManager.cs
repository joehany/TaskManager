using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Infrastructure.Data.Entities;

namespace TaskManager.Infrastructure.Interfaces.Managers
{
    public interface IListManager
    {
        IEnumerable<TaskList> GetAlLists();
        TaskList GetListById(Guid id);
        TaskList CreateList();
        List<TaskItem> GetListTasks(Guid listId);
        TaskItem CreateTask(TaskItem newTask);
        void UpdateTask(Guid id, TaskItem updatedTask);
        void DeleteTask(Guid id);
    }
    
}
