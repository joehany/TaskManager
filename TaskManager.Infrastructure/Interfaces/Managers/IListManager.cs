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
        TaskList GetListById(Guid id);
        void CreateList(Guid id);
        List<TaskItem> GetListTasks(Guid listId);
    }
    
}
