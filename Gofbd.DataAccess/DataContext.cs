namespace Gofbd.DataAccess
{
    using Gofbd.Core;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    public class DataContext : DbContext, IDataContext
    {
        public DataContext(DbContextOptions options): base(options)
        {
            
        }

        public DataContext(DbContextOptionsBuilder optionsBuilder) : this(optionsBuilder.Options)
        {

        }

        // public DbSet<User> Users { get; set; }
        // public DbSet<Attendance> Attendances { get; set; }
        // public DbSet<Product> Products { get; set; }
        // public DbSet<ProductRequest> ProductRequests { get; set; }
        // public DbSet<ProductRequestDetail> ProductRequestDetails { get; set; }
        // public DbSet<Catchment> Catchments { get; set; }
        // public DbSet<SubCatchment> SubCatchments { get; set; }
        // public DbSet<Camp> Camps { get; set; }
        // public DbSet<Partner> Partners { get; set; }

        
        //public DbSet<Order> Orders { get; set; }
        //public DbSet<OrderDetail> OrderDetails { get; set; }
        //public DbSet<ProductCategory> ProductCategories { get; set; }
        //public DbSet<UserProfile> UserProfiles { get; set; }

        public DbSet<TDomainObject> Get<TDomainObject>() where TDomainObject : class
        {
            return this.Set<TDomainObject>();
        }

        public new EntityEntry<TDomainObject> Add<TDomainObject>(TDomainObject tDomainObject) where TDomainObject : class
        {
            return base.Add(tDomainObject);
        }

        public new EntityEntry<TDomainObject> Update<TDomainObject>(TDomainObject tDomainObject) where TDomainObject : class
        {
            return base.Update(tDomainObject);
        }

        public new EntityEntry<TDomainObject> Remove<TDomainObject>(TDomainObject tDomainObject) where TDomainObject : class
        {
            return base.Remove(tDomainObject);
        }

        public override TDomainObject Find<TDomainObject>(params object[] keyValues)
        {
            return base.Find<TDomainObject>(typeof(TDomainObject), keyValues);
        }
    }
}