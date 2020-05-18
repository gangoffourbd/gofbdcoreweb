namespace Gofbd.DataAccess
{
    using Gof.Api.Domain;
    using Gofbd.Core;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Threading.Tasks;

    public class DataContext : DbContext, IDataContext
    {
        //private readonly ILogger logger;

        public DataContext(DbContextOptionsBuilder builder) : base(builder.Options)
        {
        }

        //public DataContext(DbContextOptionsBuilder builder, ILogger<DataContext> logger) : this(builder)
        //{
        //    this.logger = logger;
        //}
        public DbSet<Product> QueueCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public DbSet<TDomainObject> Get<TDomainObject>() where TDomainObject : class
        {
            return this.Set<TDomainObject>();
        }

        public void MarkAsModified<TDomainObject>(TDomainObject instance) where TDomainObject : class
        {
            this.Entry(instance).State = EntityState.Modified;
        }

        public void Reload<TDomainObject>(TDomainObject instance) where TDomainObject : class
        {
            this.Entry(instance).Reload();
        }

        public async Task<IEnumerable<DynamicRecord>> ExecuteSProc(string procedureName, IDictionary<string, object> parameters = null)
        {
            var records = new List<DynamicRecord>();
            try
            {
                using (var reader = await this.GetReader(CommandType.StoredProcedure, procedureName, parameters))
                {
                    while (await reader.ReadAsync())
                    {
                        records.Add(new DynamicRecord(reader));
                    }
                }
            }
            catch (Exception exception)
            {
                //TODO: Make logger async
                //await logger.Error($"Got the following exception while executing this procedure: {procedureName}\n Exception Details: {exception}");
                throw;
            }

            return records;
        }

        public async Task<IEnumerable<DynamicRecord>> ExecuteQuery(string query, IDictionary<string, object> parameters = null)
        {
            var records = new List<DynamicRecord>();
            try
            {
                using (var reader = await this.GetReader(CommandType.Text, query, parameters))
                {
                    while (await reader.ReadAsync())
                    {
                        records.Add(new DynamicRecord(reader));
                    }
                }
            }
            catch (Exception exception)
            {
                //TODO: Make logger async
                //await logger.Error($"Got the following exception while executing this query: {query}\n Exception Details: {exception}");
                throw;
            }

            return records;
        }

        public async Task ExecuteNoQueryResult(string query, IDictionary<string, object> parameters = null)
        {
            try
            {
                using (var command = this.CreateCommand(CommandType.Text, query, parameters))
                {
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception exception)
            {
                //TODO: Make logger async
                //await logger.Error($"Got the following exception while executing this query: {query}\n Exception Details: {exception}");
                throw;
            }
        }


        private async Task<DbDataReader> GetReader(CommandType commandType, string commandText, IDictionary<string, object> parameters)
        {
            var command = this.CreateCommand(commandType, commandText, parameters);
            var reader = await command.ExecuteReaderAsync();
            return reader;
        }

        private DbCommand CreateCommand(CommandType commandType, string commandText, IDictionary<string, object> parameters)
        {
            var connection = Database.GetDbConnection();
            EnsureOpenConnection(connection);
            var command = Database.GetDbConnection().CreateCommand();
            command.CommandTimeout = 20 * 60; // Increase timeout for sql command
            command.CommandType = commandType;
            command.CommandText = commandText;
            AssignCommandParameters(command, parameters);
            return command;
        }

        private void EnsureOpenConnection(DbConnection connection)
        {
            if (connection.State != ConnectionState.Closed && connection.State != ConnectionState.Broken)
                return;

            if (connection.State == ConnectionState.Broken)
            {
                connection.Close();
            }
            connection.Open();
        }

        private static void AssignCommandParameters(DbCommand command, IDictionary<string, object> parameters)
        {
            if (parameters == null || parameters.Count == 0)
            {
                return;
            }

            foreach (var pair in parameters)
            {
                var param = command.CreateParameter();
                param.ParameterName = $"@{pair.Key}";
                var value = pair.Value;
                param.Value = value;
                param.DbType = GetDbType(value);
                command.Parameters.Add(param);
            }
        }

        private static DbType GetDbType(object value)
        {
            if (value is DateTime)
            {
                return DbType.DateTime;
            }
            if (value is bool)
            {
                return DbType.Boolean;
            }
            if (value is int)
            {
                return DbType.Int32;
            }
            if (value is Guid)
            {
                return DbType.Guid;
            }
            return DbType.String;
        }
    }

}