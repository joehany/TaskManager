using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Ninject;
using TaskManager.DAL.Base;
using TaskManager.Infrastructure.Data.Entities;
using TaskManager.Infrastructure.Interfaces;
using TaskManager.Infrastructure.Interfaces.Dal;

namespace TaskManager.DAL
{
    public class TaskManagerDataUnit : BaseDataUnit,ITaskManagerDataUnit
    {
        private string _connection { get; set; }

        public TaskManagerDataUnit()
        {
            _connection = System.Configuration.ConfigurationManager.ConnectionStrings["TaskManagerDataContext"].ConnectionString;
            DataContext = new TaskManagerDataContext(_connection);
        }
        [Inject]
        public IBaseDataRepository<TaskList, ITaskManagerDataUnit> ListRepository { get; set; }

        [Inject]
        public IBaseDataRepository<TaskItem, ITaskManagerDataUnit> TaskRepository { get; set; }

    }
}
