namespace TaskManager.Infrastructure.Interfaces.Dal
{
    public interface IBaseDataUnit
    {
        IBaseDataContext DataContext { get; }
        void SaveChanges();
    }
}
