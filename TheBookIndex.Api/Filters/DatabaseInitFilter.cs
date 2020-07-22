using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using TheBookIndex.Data;
using TheBookIndex.Data.BaseTypes;

namespace TheBookIndex.Api.Filters
{
    public class DatabaseInitFilter : IStartupFilter
    {
        private readonly ConnectionString _connectionString;

        public DatabaseInitFilter(ConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            Console.WriteLine("Starting DB Upodate");
            try
            {
                UpdateDatabase.UpdateDatabaseIfRequired(_connectionString);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error updating db: {e.Message}");
                Console.WriteLine($"  Db Error inner exception: {e.InnerException?.Message}");
                throw;
            }

            Console.WriteLine("DB Upodate complete");

            return next;
        }
    }
}
