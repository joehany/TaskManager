using TaskManager.Infrastructure.Interfaces.Dal;

namespace TaskManager.DAL.Base
{
    public abstract class BaseDataUnit : IBaseDataUnit
    {
        public IBaseDataContext DataContext { get; set; }

        public void SaveChanges()
        {
            DataContext.SaveChanges();
        }
    }
}
