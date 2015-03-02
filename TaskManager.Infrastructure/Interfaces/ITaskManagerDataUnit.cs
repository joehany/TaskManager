using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Infrastructure.Data.Entities;
using TaskManager.Infrastructure.Interfaces.Dal;

namespace TaskManager.Infrastructure.Interfaces
{
    public interface ITaskManagerDataUnit : IBaseDataUnit
    {
        IBaseDataRepository<TaskList, ITaskManagerDataUnit> ListRepository { get; set; }
        IBaseDataRepository<TaskItem, ITaskManagerDataUnit> TaskRepository { get; set; }
 
    }
}
