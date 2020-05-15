namespace Gofbd.DataAccess
{
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    public class DataContextFactory : IDataContextFactory
    {
        private readonly IConfiguration _configuration;
        public DataContextFactory(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public async Task<DataContext> Create()
        {
            var connectionString = this._configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return await Task.FromResult(new DataContext(optionsBuilder));
        }
    }
}