using System;
using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Microsoft.Extensions.Configuration;
using TheBookIndex.Data.BaseTypes;

namespace TheBookIndex.Api.Configuration
{
    public class ConfigurationManager
    {
        private ConfigurationManager(IConfigurationRoot configuration)
        {
            Configuration = configuration;
        }

        public static ConfigurationManager BuildConfiguration(string rootPath, string environmentName)
        {
            //Log.Information("Building Configuration from Path {rootPath} for environmentName {environmentName}", rootPath, environmentName.ToLower());

            var builder = new ConfigurationBuilder().SetBasePath(rootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{environmentName.ToLower()}.json", true, true)
                .AddJsonFile($"appsettings.{environmentName.ToLower()}.private.json", true, true)
                .AddEnvironmentVariables();

            if (IsProduction())
            {
                var awsOptions = new AWSOptions
                {
                    Region = RegionEndpoint.APSoutheast2
                };

                builder.AddSystemsManager($"/tbi/{environmentName.ToLower()}", awsOptions);
            }

            return new ConfigurationManager(builder.Build());
        }

        public IConfigurationRoot Configuration { get; }

        public static string EnvironmentName => GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").ToLower();
        public ConnectionString ConnectionString => new ConnectionString(Configuration.GetValue<string>("TBI_CONNECTIONSTRING"));

        public static bool IsBuildServer() => EnvironmentName == "buildserver";
        public static bool IsDevelopment() => EnvironmentName == "development";
        public static bool IsStaging() => EnvironmentName == "staging";
        public static bool IsProduction() => EnvironmentName == "production";


        private static string GetEnvironmentVariable(string variable)
        {
            var value = Environment.GetEnvironmentVariable(variable);
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidOperationException($"Unable to start application. You are missing the {variable} environment variable.");
            }
            return value;
        }

    }
}
