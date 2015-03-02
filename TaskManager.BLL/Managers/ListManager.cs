using System;
using System.Collections.Generic;
using System.Linq;
using TaskManager.Infrastructure.Data.Entities;
using TaskManager.Infrastructure.Interfaces;
using TaskManager.Infrastructure.Interfaces.Managers;

namespace TaskManager.BLL.Managers
{
    public class ListManager:IListManager
    {
        private readonly ITaskManagerDataUnit _dataUnit;

        public ListManager(ITaskManagerDataUnit dataUnit)
        {
            _dataUnit = dataUnit;
        }

        public TaskList GetListById(Guid id)
        {
            return _dataUnit.ListRepository.Find(id);
        }

        public void CreateList(Guid id)
        {
            _dataUnit.ListRepository.Insert(new TaskList(){Id = id});
            _dataUnit.SaveChanges();
        }

        public List<TaskItem> GetListTasks(Guid listId)
        {
            return _dataUnit.TaskRepository.All.Where(item => item.TaskListId == listId).ToList();
        }

    }
}
