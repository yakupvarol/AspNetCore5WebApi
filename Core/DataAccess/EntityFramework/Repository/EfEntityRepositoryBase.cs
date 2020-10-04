using Core.DataAccess.EntityFramework.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework.Repository
{
    public class EfEntityRepositoryBase<T, TContext> : IEntityRepository<T>, IDisposable where T : class where TContext : DbContext, new()
    {
        protected DbContext _entities;
        protected readonly DbSet<T> _dbset;
        protected readonly IUnitOfWork<DbContext> _uow;

        public EfEntityRepositoryBase()
        {
            var context = new TContext();
            _entities = context;
            _dbset = context.Set<T>();
            _uow = new UnitOfWork<DbContext>(context);
        }

        //-------------

        public virtual IQueryable<T> GetAllBy() => _dbset.AsNoTracking().AsQueryable();
        public virtual IEnumerable<T> GetAllIEnumerable() => _dbset.AsNoTracking().AsQueryable();
        public virtual IList<T> GetAllIList() => _dbset.AsNoTracking().ToList();
        public virtual ICollection<T> GetAllICollection() => _dbset.AsNoTracking().ToList();

        public virtual async Task<IEnumerable<T>> GetAllIEnumerableAsync() => await _dbset.AsNoTracking().ToListAsync();
        public virtual async Task<IList<T>> GetAllIListAsync() => await _dbset.AsNoTracking().ToListAsync();
        public virtual async Task<ICollection<T>> GetAllICollectionAsync() => await _dbset.AsNoTracking().ToListAsync();

        //-------------



        public virtual T GetById(params object[] id) => _dbset.Find(id);
        public virtual T GetById(object id) => _dbset.Find(id);
        public virtual T GetById(Expression<Func<T, bool>> predicate) => _dbset.Find(predicate);
        public virtual T FirstBy(Expression<Func<T, bool>> predicate) => _dbset.AsNoTracking().FirstOrDefault(predicate);
        public virtual T SingleBy(Expression<Func<T, bool>> predicate) => _dbset.AsNoTracking().SingleOrDefault(predicate);
        public virtual T LastBy(Expression<Func<T, bool>> predicate) => _dbset.AsNoTracking().LastOrDefault(predicate);

        public virtual async Task<T> GetByIdAsync(params object[] id) => await _dbset.FindAsync(id);
        public virtual async Task<T> GetByIdAsync(object id) => await _dbset.FindAsync(id);
        public virtual async Task<T> GetByIdAsync(Expression<Func<T, bool>> predicate) => await _dbset.FindAsync(predicate);
        public virtual async Task<T> FirstByAsync(Expression<Func<T, bool>> predicate) => await _dbset.AsNoTracking().FirstOrDefaultAsync(predicate);
        public virtual async Task<T> SingleByAsync(Expression<Func<T, bool>> predicate) => await _dbset.AsNoTracking().SingleOrDefaultAsync(predicate);
        public virtual async Task<T> LastByAsync(Expression<Func<T, bool>> predicate) => await _dbset.AsNoTracking().LastOrDefaultAsync(predicate);


        //-------------

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate) => _dbset.AsNoTracking().Where(predicate);
        public virtual IEnumerable<T> FindByIEnumerable(Expression<Func<T, bool>> predicate) => _dbset.AsNoTracking().Where(predicate);
        public virtual IList<T> FindByIList(Expression<Func<T, bool>> predicate) => _dbset.AsNoTracking().Where(predicate).ToList();
        public virtual ICollection<T> FindByICollection(Expression<Func<T, bool>> predicate) => _dbset.AsNoTracking().Where(predicate).ToList();

        public virtual async Task<IEnumerable<T>> FindByIEnumerableAsync(Expression<Func<T, bool>> predicate) => await _dbset.AsNoTracking().Where(predicate).ToListAsync();
        public virtual async Task<IList<T>> FindByIListAsync(Expression<Func<T, bool>> predicate) => await _dbset.AsNoTracking().Where(predicate).ToListAsync();
        public virtual async Task<ICollection<T>> FindByICollectionAsync(Expression<Func<T, bool>> predicate) => await _dbset.AsNoTracking().Where(predicate).ToListAsync();

        //-------------

        public virtual object CountBy() => _dbset.AsNoTracking().Count();
        public virtual object CountByFind(Expression<Func<T, bool>> predicate) => _dbset.AsNoTracking().Count(predicate);

        public virtual async Task<object> CountByAsync() => await _dbset.AsNoTracking().CountAsync();
        public virtual async Task<object> CountByFindAsync(Expression<Func<T, bool>> predicate) => await _dbset.AsNoTracking().CountAsync(predicate);

        public virtual object LongCountBy() => _dbset.AsNoTracking().LongCount();
        public virtual object LongCountByFind(Expression<Func<T, bool>> predicate) => _dbset.AsNoTracking().LongCount(predicate);

        public virtual async Task<object> LongCountByAsync() => await _dbset.AsNoTracking().LongCountAsync();
        public virtual async Task<object> LongCountByFindAsync(Expression<Func<T, bool>> predicate) => await _dbset.AsNoTracking().LongCountAsync(predicate);

        //-------------

        public virtual bool AnyBy(Expression<Func<T, bool>> predicate) => _dbset.Any(predicate);
        public virtual async Task<bool> AnyByAsync(Expression<Func<T, bool>> predicate) => await _dbset.AnyAsync(predicate);

        public virtual bool AllBy(Expression<Func<T, bool>> predicate) => _dbset.All(predicate);
        public virtual async Task<bool> AllByAsync(Expression<Func<T, bool>> predicate) => await _dbset.AllAsync(predicate);

        //-------------

        public virtual void AddVoid(T t) => _dbset.Add(t);
        public virtual void InsertVoid(T t) => _entities.Entry(t).State = EntityState.Added;
        public virtual void AddVoidRange(IEnumerable<T> t) => _entities.AddRange(t);

        public virtual T Add(T t) { _dbset.Add(t); return t; }
        public virtual T Insert(T t) { _entities.Entry(t).State = EntityState.Added; return t; }
        public virtual IEnumerable<T> AddRange(IEnumerable<T> t) { _entities.AddRange(t); return t; }

        public virtual async Task AddVoidAsync(T t) => await _dbset.AddAsync(t);
        public virtual async Task AddVoidRangeAsync(IEnumerable<T> t) => await _entities.AddRangeAsync(t);

        public virtual async Task<T> AddAsync(T t) { await _dbset.AddAsync(t); return t; }
        public virtual async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> t) { await _entities.AddRangeAsync(t); return t; }

        //-------------

        public virtual void UpdateVoid(T t) { _entities.Attach(t); _entities.Entry(t).State = EntityState.Modified; }
        public virtual void UpdateVoid(object model, object id)
        {
            var entity = _dbset.Find(id);
            if (entity != null)
            {
                _entities.Attach(entity);
                _entities.Entry(entity).CurrentValues.SetValues(model);
            }
        }
        public virtual void UpdateVoid(object model, Expression<Func<T, bool>> predicate)
        {
            var entity = _dbset.AsNoTracking().FirstOrDefault(predicate);
            if (entity != null)
                _entities.Entry(entity).CurrentValues.SetValues(model);
        }
        public virtual void UpdateVoidRange(IEnumerable<T> t) => _entities.UpdateRange(t);

        public virtual T Update(T t) { _entities.Attach(t); _entities.Entry(t).State = EntityState.Modified; return t; }
        public virtual T Update(object model, object id)
        {
            var entity = _dbset.Find(id);
            if (entity != null)
            {
                _entities.Attach(entity);
                _entities.Entry(entity).CurrentValues.SetValues(model);
            }
            return entity;
        }
        public virtual T Update(object model, Expression<Func<T, bool>> predicate)
        {
            var entity = _dbset.FirstOrDefault(predicate);
            if (entity != null)
            {
                _entities.Attach(entity);
                _entities.Entry(entity).CurrentValues.SetValues(model);
            }
            return entity;
        }
        public virtual IEnumerable<T> UpdateRange(IEnumerable<T> t) { _entities.UpdateRange(t); return t; }

        public virtual async Task UpdateVoidAsync(object model, object id)
        {
            var entity = await _dbset.FindAsync(id);
            if (entity != null)
            {
                _entities.Attach(entity);
                _entities.Entry(entity).CurrentValues.SetValues(model);
            }
        }
        public virtual async Task UpdateVoidAsync(object model, Expression<Func<T, bool>> predicate)
        {
            var entity = await _dbset.AsNoTracking().FirstOrDefaultAsync(predicate);
            if (entity != null)
            {
                _entities.Attach(entity);
                _entities.Entry(entity).CurrentValues.SetValues(model);
            }
        }

        public virtual async Task<T> UpdateAsync(object model, object id)
        {
            var entity = await _dbset.FindAsync(id);
            if (entity != null)
            {
                _entities.Attach(entity);
                _entities.Entry(entity).CurrentValues.SetValues(model);
            }
            return entity;
        }
        public virtual async Task<T> UpdateAsync(object model, Expression<Func<T, bool>> predicate)
        {
            var entity = await _dbset.AsNoTracking().FirstOrDefaultAsync(predicate);
            if (entity != null)
            {
                _entities.Attach(entity);
                _entities.Entry(entity).CurrentValues.SetValues(model);
            }
            return entity;
        }

        //-------------

        public virtual void Remove(T t) { _dbset.Attach(t); _dbset.Remove(t); }
        public virtual void Delete(T t)
        {
            var deleteEntity = _entities.Entry(t);
            deleteEntity.State = EntityState.Deleted;
        }
        public virtual void RemoveRange(IEnumerable<T> entity) => _entities.RemoveRange(entity);

        public virtual void FindAndRemoveVoid(object id)
        {
            var deleteEntity = _dbset.Find(id);
            if (deleteEntity != null)
                _dbset.Remove(deleteEntity);
        }
        public virtual void FindAndRemoveVoid(Expression<Func<T, bool>> predicate)
        {
            var deleteEntity = _dbset.Single(predicate);
            if (deleteEntity != null)
                _dbset.Remove(deleteEntity);
        }

        public virtual bool FindAndRemove(object id)
        {
            var resultValue = false;
            var deleteEntity = _dbset.Find(id);
            if (deleteEntity != null)
            { _dbset.Remove(deleteEntity); resultValue = true; }
            return resultValue;
        }
        public virtual bool FindAndRemove(Expression<Func<T, bool>> predicate)
        {
            var resultValue = false;
            var deleteEntity = _dbset.AsNoTracking().FirstOrDefault(predicate);
            if (deleteEntity != null)
            { _dbset.Remove(deleteEntity); resultValue = true; }
            return resultValue;
        }

        public virtual async Task FindAndRemoveVoidAsync(object id)
        {
            var deleteEntity = await _dbset.FindAsync(id);
            if (deleteEntity != null)
                _dbset.Remove(deleteEntity);
        }
        public virtual async Task FindAndRemoveVoidAsync(Expression<Func<T, bool>> predicate)
        {
            var deleteEntity = await _dbset.SingleAsync(predicate);
            if (deleteEntity != null)
                _dbset.Remove(deleteEntity);
        }

        public virtual async Task<bool> FindAndRemoveAsync(object id)
        {
            var resultValue = false;
            var deleteEntity = await _dbset.FindAsync(id);
            if (deleteEntity != null)
            { _dbset.Remove(deleteEntity); resultValue = true; }
            return resultValue;
        }

        public virtual async Task<bool> FindAndRemoveAsync(Expression<Func<T, bool>> predicate)
        {
            var resultValue = false;
            var deleteEntity = await _dbset.AsNoTracking().FirstOrDefaultAsync(predicate);
            if (deleteEntity != null)
            { _dbset.Remove(deleteEntity); resultValue = true; }
            return resultValue;
        }

        //-------------

        public virtual T IncludeBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbset.AsNoTracking();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.FirstOrDefault(predicate);
        }
        public virtual IQueryable<T> IncludeBy(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbset.AsNoTracking();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public virtual T IncludeFirstBy(Expression<Func<T, object>> includeProperties, Expression<Func<T, bool>> predicate) => _dbset.Include(includeProperties).FirstOrDefault(predicate);
        public virtual async Task<T> IncludeFirstByAsync(Expression<Func<T, object>> includeProperties, Expression<Func<T, bool>> predicate) => await _dbset.Include(includeProperties).FirstOrDefaultAsync(predicate);

        public virtual IQueryable<T> IncludeBy(Expression<Func<T, object>> includeProperties) => _dbset.AsNoTracking().Include(includeProperties);
        public virtual IEnumerable<T> IncludeIEnumerable(Expression<Func<T, object>> includeProperties) => _dbset.AsNoTracking().Include(includeProperties);
        public virtual IList<T> IncludeIList(Expression<Func<T, object>> includeProperties) => _dbset.AsNoTracking().Include(includeProperties).ToList();
        public virtual ICollection<T> IncludeICollection(Expression<Func<T, object>> includeProperties) => _dbset.AsNoTracking().Include(includeProperties).ToList();

        public virtual async Task<IEnumerable<T>> IncludeIEnumerableAsync(Expression<Func<T, object>> includeProperties) => await _dbset.AsNoTracking().Include(includeProperties).ToListAsync();
        public virtual async Task<IList<T>> IncludeIListAsync(Expression<Func<T, object>> includeProperties) => await _dbset.AsNoTracking().Include(includeProperties).ToListAsync();
        public virtual async Task<ICollection<T>> IncludeICollectionAsync(Expression<Func<T, object>> includeProperties) => await _dbset.AsNoTracking().Include(includeProperties).ToListAsync();

        //------------

        public IQueryable<T> RawSql(string query) { return _dbset.FromSqlRaw(query); }

        public IQueryable<T> RawSql(string query, params object[] parameters) { return _dbset.FromSqlRaw(query, parameters); }

        //------------

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _entities.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~EfEntityRepositoryBase()
        {
            Dispose(false);
        }

    }
}
