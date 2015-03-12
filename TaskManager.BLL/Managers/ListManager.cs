using System;
using System.Collections.Generic;
using System.Linq;
using TaskManager.Infrastructure.Data.Entities;
using TaskManager.Infrastructure.Interfaces;
using TaskManager.Infrastructure.Interfaces.Managers;

namespace TaskManager.BLL.Managers
{
    public class ListManager : IListManager
    {
        private readonly ITaskManagerDataUnit _dataUnit;

        public ListManager(ITaskManagerDataUnit dataUnit)
        {
            _dataUnit = dataUnit;
        }
        public IEnumerable<TaskList> GetAlLists()
        {
            return _dataUnit.ListRepository.GetIncluding("Items");
        }
        public TaskList GetListById(Guid id)
        {
            var list = _dataUnit.ListRepository.GetIncluding("Items").SingleOrDefault(l => l.Id == id);
            if (list != null)
            {
                list.Items = list.Items.OrderBy(k => k.DateCreated).ToList();
            } 
            return list;
        }

        public TaskList CreateList()
        {
            var list = new TaskList
                {
                    Id = GenerateNewGuid(),
                    DateCreated = DateTime.Now.ToUniversalTime(),
                    DateModified = DateTime.Now.ToUniversalTime()
                };
            _dataUnit.ListRepository.Insert(list);
            _dataUnit.SaveChanges();
            return list;
        }

        public List<TaskItem> GetListTasks(Guid listId)
        {
            return _dataUnit.TaskRepository.All.Where(item => item.TaskListId == listId).ToList();
        }
        public TaskItem CreateTask(TaskItem newtTask)
        {
            newtTask.Id = GenerateNewGuid();
            newtTask.DateCreated = DateTime.Now.ToUniversalTime();
            newtTask.DateModified = DateTime.Now.ToUniversalTime();
            _dataUnit.TaskRepository.Insert(newtTask);
            TaskList foundList = _dataUnit.ListRepository.Find(newtTask.TaskListId);
            foundList.DateModified = DateTime.Now.ToUniversalTime();
            _dataUnit.SaveChanges();
            return newtTask;
        }
        public void UpdateTask(Guid id, TaskItem updatedTask)
        {
            TaskItem foundTask = _dataUnit.TaskRepository.Find(id);
            foundTask.IsDone = updatedTask.IsDone;
            foundTask.Name = updatedTask.Name;
            foundTask.DateModified = DateTime.Now.ToUniversalTime();
            TaskList foundList = _dataUnit.ListRepository.Find(foundTask.TaskListId);
            foundList.DateModified = DateTime.Now.ToUniversalTime();
            _dataUnit.SaveChanges();
        }

        public void DeleteTask(Guid id)
        {
            TaskItem foundTask = _dataUnit.TaskRepository.Find(id);
            _dataUnit.TaskRepository.Remove(foundTask);
            _dataUnit.SaveChanges();
        }

        private Guid GenerateNewGuid()
        {
            return Guid.NewGuid();
        }
    }
}
