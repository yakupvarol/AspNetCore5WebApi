using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework.UnitOfWork
{
    public interface IUnitOfWork<TContext> : IDisposable where TContext : DbContext
    {
        TContext Context { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
        bool Commit();
        Task<bool> CommitAsync();
    }
}
