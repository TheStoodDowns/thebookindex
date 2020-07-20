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
            UpdateDatabase.UpdateDatabaseIfRequired(_connectionString);

            return next;
        }
    }
}
