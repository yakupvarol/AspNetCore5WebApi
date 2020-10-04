using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework.Repository
{
    public interface IEntityRepository<T> where T : class
    {
        IQueryable<T> GetAllBy();
        IEnumerable<T> GetAllIEnumerable();
        IList<T> GetAllIList();
        ICollection<T> GetAllICollection();

        Task<IEnumerable<T>> GetAllIEnumerableAsync();
        Task<IList<T>> GetAllIListAsync();
        Task<ICollection<T>> GetAllICollectionAsync();

        T GetById(params object[] id);
        T GetById(object id);
        T GetById(Expression<Func<T, bool>> predicate);
        T FirstBy(Expression<Func<T, bool>> predicate);
        T SingleBy(Expression<Func<T, bool>> predicate);
        T LastBy(Expression<Func<T, bool>> predicate);

        Task<T> GetByIdAsync(params object[] id);
        Task<T> GetByIdAsync(object id);
        Task<T> GetByIdAsync(Expression<Func<T, bool>> predicate);
        Task<T> FirstByAsync(Expression<Func<T, bool>> predicate);
        Task<T> SingleByAsync(Expression<Func<T, bool>> predicate);
        Task<T> LastByAsync(Expression<Func<T, bool>> predicate);

        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        IEnumerable<T> FindByIEnumerable(Expression<Func<T, bool>> predicate);
        IList<T> FindByIList(Expression<Func<T, bool>> predicate);
        ICollection<T> FindByICollection(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> FindByIEnumerableAsync(Expression<Func<T, bool>> predicate);
        Task<IList<T>> FindByIListAsync(Expression<Func<T, bool>> predicate);
        Task<ICollection<T>> FindByICollectionAsync(Expression<Func<T, bool>> predicate);

        object CountBy();
        object CountByFind(Expression<Func<T, bool>> predicate);

        Task<object> CountByAsync();
        Task<object> CountByFindAsync(Expression<Func<T, bool>> predicate);

        object LongCountBy();
        object LongCountByFind(Expression<Func<T, bool>> predicate);
        Task<object> LongCountByAsync();
        Task<object> LongCountByFindAsync(Expression<Func<T, bool>> predicate);

        bool AnyBy(Expression<Func<T, bool>> predicate);
        Task<bool> AnyByAsync(Expression<Func<T, bool>> predicate);

        bool AllBy(Expression<Func<T, bool>> predicate);
        Task<bool> AllByAsync(Expression<Func<T, bool>> predicate);

        void AddVoid(T t);
        void InsertVoid(T t);
        void AddVoidRange(IEnumerable<T> t);

        T Add(T t);
        T Insert(T t);
        IEnumerable<T> AddRange(IEnumerable<T> t);

        Task AddVoidAsync(T t);
        Task AddVoidRangeAsync(IEnumerable<T> t);

        Task<T> AddAsync(T t);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> t);

        void UpdateVoid(T t);
        void UpdateVoid(object model, object id);
        void UpdateVoid(object model, Expression<Func<T, bool>> predicate);
        void UpdateVoidRange(IEnumerable<T> t);

        T Update(T t);
        T Update(object model, object id);
        T Update(object model, Expression<Func<T, bool>> predicate);
        IEnumerable<T> UpdateRange(IEnumerable<T> t);

        Task UpdateVoidAsync(object model, object id);
        Task UpdateVoidAsync(object model, Expression<Func<T, bool>> predicate);

        Task<T> UpdateAsync(object model, object id);
        Task<T> UpdateAsync(object model, Expression<Func<T, bool>> predicate);

        void Remove(T t);
        void Delete(T t);
        void RemoveRange(IEnumerable<T> entity);
        void FindAndRemoveVoid(object id);
        void FindAndRemoveVoid(Expression<Func<T, bool>> predicate);

        bool FindAndRemove(object id);
        bool FindAndRemove(Expression<Func<T, bool>> predicate);

        Task FindAndRemoveVoidAsync(object id);
        Task FindAndRemoveVoidAsync(Expression<Func<T, bool>> predicate);
        Task<bool> FindAndRemoveAsync(object id);
        Task<bool> FindAndRemoveAsync(Expression<Func<T, bool>> predicate);

        T IncludeBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> IncludeBy(params Expression<Func<T, object>>[] includeProperties);

        T IncludeFirstBy(Expression<Func<T, object>> includeProperties, Expression<Func<T, bool>> predicate);
        Task<T> IncludeFirstByAsync(Expression<Func<T, object>> includeProperties, Expression<Func<T, bool>> predicate);

        IQueryable<T> IncludeBy(Expression<Func<T, object>> includeProperties);
        IEnumerable<T> IncludeIEnumerable(Expression<Func<T, object>> includeProperties);
        IList<T> IncludeIList(Expression<Func<T, object>> includeProperties);
        ICollection<T> IncludeICollection(Expression<Func<T, object>> includeProperties);

        Task<IEnumerable<T>> IncludeIEnumerableAsync(Expression<Func<T, object>> includeProperties);
        Task<IList<T>> IncludeIListAsync(Expression<Func<T, object>> includeProperties);
        Task<ICollection<T>> IncludeICollectionAsync(Expression<Func<T, object>> includeProperties);

        IQueryable<T> RawSql(string query);
        IQueryable<T> RawSql(string query, params object[] parameters);
    }
}
