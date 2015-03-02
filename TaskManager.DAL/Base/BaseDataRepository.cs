using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using TaskManager.Infrastructure.Interfaces.Dal;

namespace TaskManager.DAL.Base
{
    public class BaseDataRepository<TDataObject, TU> : IBaseDataRepository<TDataObject, TU>
        where TDataObject : class
        where TU : IBaseDataUnit
    {
        protected readonly IBaseDataContext Context;

        public BaseDataRepository(TU unit)
        {
            Context = unit.DataContext;
        }

        public TDataObject Find(Guid id)
        {
            return Context.Set<TDataObject>().Find(id);
        }

        public virtual IQueryable<TDataObject> All
        {
            get { return Context.Set<TDataObject>(); }
        }

        public TDataObject Insert(TDataObject newEntity)
        {
            return Context.Set<TDataObject>().Add(newEntity);
        }

        public void Update(TDataObject entityToEdit)
        {
            Context.Entry(entityToEdit).State = EntityState.Modified;
        }

        public void Remove(TDataObject entityToDelete)
        {
            Context.Set<TDataObject>().Remove(entityToDelete);
        }

        public void Attach(TDataObject entityToAttach)
        {
            Context.Set<TDataObject>().Attach(entityToAttach);
        }

        public TDataObject FindSingle(Expression<Func<TDataObject, bool>> searchCriteria)
        {
            return Context.Set<TDataObject>().Where(searchCriteria).SingleOrDefault();
        }

        public IQueryable<TDataObject> GetIncluding(params string[] includeProperties)
        {
            IQueryable<TDataObject> query = Context.Set<TDataObject>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public IQueryable<TDataObject> AddIncluding(IQueryable<TDataObject> query, params Expression<Func<TDataObject, string>>[] includeProperties)
        {
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

    }
}
