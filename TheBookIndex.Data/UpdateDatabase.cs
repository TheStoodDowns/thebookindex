using System;
using System.Reflection;
using DbUp;
using TheBookIndex.Data.BaseTypes;

namespace TheBookIndex.Data
{
    public static class UpdateDatabase
    {
        public static void UpdateDatabaseIfRequired(ConnectionString connectionString)
        {
            //var cs =
            //    "Server=127.0.0.1; Port=3306; Database=thebookindex; Uid=tbi; Pwd=password;";


            EnsureDatabase.For.MySqlDatabase(connectionString.Db);

            var dbUpgradeEngineBuilder = DeployChanges.To.MySqlDatabase(connectionString.Db)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .WithTransaction();

            var dbUpgradeEngine = dbUpgradeEngineBuilder.Build();
            if (dbUpgradeEngine.IsUpgradeRequired())
            {
                var operation = dbUpgradeEngine.PerformUpgrade();
                if (!operation.Successful)
                {
                    throw new Exception($"DbUp tried to update the database and failed: {operation.Error}.");
                }
            }
        }
    }
}
