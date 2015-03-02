using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace TaskManager.Infrastructure.Interfaces.Dal
{
    public interface IBaseDataContext
    {
        int SaveChanges();
        IDbSet<T> Set<T>() where T : class;

        DbEntityEntry Entry(object newEntity);
    }
}
