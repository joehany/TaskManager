using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace TaskManager.Infrastructure.Interfaces.Dal
{
    public interface IBaseDataRepository<TDataObject, TU>
        where TDataObject : class
        where TU : IBaseDataUnit
    {
        TDataObject Find(Guid id);
        IQueryable<TDataObject> All { get; }
        TDataObject Insert(TDataObject newEntity);
        void Update(TDataObject entityToEdit);
        void Remove(TDataObject entityToDelete);
        void Attach(TDataObject entityToAttach);

        TDataObject FindSingle(Expression<Func<TDataObject, bool>> searchCriteria);
        IQueryable<TDataObject> GetIncluding(params string[] includeProperties);
        IQueryable<TDataObject> AddIncluding(IQueryable<TDataObject> query, params Expression<Func<TDataObject, string>>[] includeProperties);
    }
}
