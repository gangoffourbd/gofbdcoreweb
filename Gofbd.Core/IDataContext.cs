namespace Gofbd.Core
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    public interface IDataContext : IDisposable
    {
        DbSet<TDomainObject> Get<TDomainObject>() where TDomainObject : class;

        void MarkAsModified<TDomainObject>(TDomainObject instance) where TDomainObject : class;

        void Reload<TDomainObject>(TDomainObject instance) where TDomainObject : class;

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken));


        EntityEntry Add(object entity);

        EntityEntry<TDomainObject> Add<TDomainObject>(TDomainObject entity) where TDomainObject : class;

        ValueTask<EntityEntry> AddAsync(object entity, CancellationToken cancellationToken = default(CancellationToken));

        ValueTask<EntityEntry<TDomainObject>> AddAsync<TDomainObject>(TDomainObject entity, CancellationToken cancellationToken = default(CancellationToken)) where TDomainObject : class;

        void AddRange(IEnumerable<object> entities);

        void AddRange(params object[] entities);

        Task AddRangeAsync(IEnumerable<object> entities, CancellationToken cancellationToken = default(CancellationToken));

        Task AddRangeAsync(params object[] entities);

        EntityEntry<TDomainObject> Attach<TDomainObject>(TDomainObject entity) where TDomainObject : class;

        EntityEntry Attach(object entity);

        void AttachRange(params object[] entities);

        void AttachRange(IEnumerable<object> entities);

        EntityEntry<TDomainObject> Entry<TDomainObject>(TDomainObject entity) where TDomainObject : class;

        EntityEntry Entry(object entity);

        object Find(Type entityType, params object[] keyValues);

        TDomainObject Find<TDomainObject>(params object[] keyValues) where TDomainObject : class;

        ValueTask<TDomainObject> FindAsync<TDomainObject>(params object[] keyValues) where TDomainObject : class;

        ValueTask<object> FindAsync(Type entityType, object[] keyValues, CancellationToken cancellationToken);

        ValueTask<TDomainObject> FindAsync<TDomainObject>(object[] keyValues, CancellationToken cancellationToken) where TDomainObject : class;

        ValueTask<object> FindAsync(Type entityType, params object[] keyValues);

        EntityEntry Remove(object entity);

        EntityEntry<TDomainObject> Remove<TDomainObject>(TDomainObject entity) where TDomainObject : class;

        void RemoveRange(IEnumerable<object> entities);

        void RemoveRange(params object[] entities);

        EntityEntry Update(object entity);

        EntityEntry<TDomainObject> Update<TDomainObject>(TDomainObject entity) where TDomainObject : class;

        void UpdateRange(params object[] entities);

        void UpdateRange(IEnumerable<object> entities);

        DatabaseFacade Database { get; }

    }
}
