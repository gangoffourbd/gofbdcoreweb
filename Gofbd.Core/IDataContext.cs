namespace Gofbd.Core
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    public interface IDataContext
    {
        DbSet<TDomainObject> Get<TDomainObject>() where TDomainObject : class;

        EntityEntry<TDomainObject> Add<TDomainObject>(TDomainObject tDomainObject) where TDomainObject : class;

        EntityEntry<TDomainObject> Update<TDomainObject>(TDomainObject tDomainObject) where TDomainObject : class;

        EntityEntry<TDomainObject> Remove<TDomainObject>(TDomainObject tDomainObject) where TDomainObject : class;

        TDomainObject Find<TDomainObject>(params object[] keyValues) where TDomainObject : class;
    }
}
