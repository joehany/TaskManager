using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using TaskManager.Infrastructure.Interfaces.Dal;

namespace TaskManager.DAL.Base
{
    public abstract class BaseDataContext : DbContext, IBaseDataContext
    {
        protected BaseDataContext(string connection) : base(connection) {}

        protected override void Dispose(bool disposing)
        {
            Debug.WriteLine("Data context disposed");
            base.Dispose(disposing);
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public new DbEntityEntry Entry(object newEntity)
        {
            return base.Entry(newEntity);
        }
    }
}
