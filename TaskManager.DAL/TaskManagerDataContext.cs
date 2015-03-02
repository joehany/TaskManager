using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using TaskManager.DAL.Base;
using TaskManager.Infrastructure.Data.Entities;
using TaskManager.Infrastructure.Interfaces;

namespace TaskManager.DAL
{
    public class TaskManagerDataContext : BaseDataContext, ITaskManagerDataContext
    {
        static TaskManagerDataContext()
        {
            Database.SetInitializer<TaskManagerDataContext>(null);
        }

        public TaskManagerDataContext(string connection)
            : base(connection)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskList>().ToTable("TaskList");
            modelBuilder.Entity<TaskItem>().ToTable("TaskItem");

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<TaskList> List { get; set; }
        public DbSet<TaskItem> Task { get; set; }
    }
}
