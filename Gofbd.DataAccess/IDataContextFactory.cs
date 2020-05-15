namespace Gofbd.DataAccess
{
    using System.Threading.Tasks;
    
    public interface IDataContextFactory
    {
        Task<DataContext> Create();
    }
}