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
            return _dataUnit.ListRepository.GetIncluding("Items").SingleOrDefault(list => list.Id == id);
        }

        public TaskList CreateList()
        {
            var list = new TaskList
                {
                    Id = GenerateNewGuid(),
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now
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
            newtTask.DateCreated = DateTime.Now;
            newtTask.DateModified = DateTime.Now;
            _dataUnit.TaskRepository.Insert(newtTask);
            TaskList foundList = _dataUnit.ListRepository.Find(newtTask.TaskListId);
            foundList.DateModified = DateTime.Now;
            _dataUnit.SaveChanges();
            return newtTask;
        }
        public void UpdateTask(TaskItem updatedTask)
        {
            TaskItem foundTask = _dataUnit.TaskRepository.Find(updatedTask.Id);
            foundTask.IsDone = updatedTask.IsDone;
            foundTask.Name = updatedTask.Name;
            foundTask.DateModified = DateTime.Now;
            TaskList foundList = _dataUnit.ListRepository.Find(updatedTask.TaskListId);
            foundList.DateModified = DateTime.Now;
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
