using System;
using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Microsoft.Extensions.Configuration;
using TheBookIndex.Data.BaseTypes;

namespace TheBookIndex.Api.Configuration
{
    public class ConfigurationManager
    {
        public static ConfigurationManager CreateForIntegrationTest(string rootPath)
        {
            var builder = BuildBaseConfiguration(rootPath, EnvironmentName);

            return new ConfigurationManager(builder.Build());
        }

        public static ConfigurationManager CreateForWebAndService(string rootPath, string environmentName)
        {
            var awsOptions = new AWSOptions
            {
                Region = RegionEndpoint.APSoutheast2
            };

            var builder = BuildBaseConfiguration(rootPath, environmentName)
                .AddSystemsManager($"/tbi/{environmentName.ToLower()}", awsOptions);

            return new ConfigurationManager(builder.Build());
        }


        private ConfigurationManager(IConfigurationRoot configuration)
        {
            Configuration = configuration;
        }

        private static IConfigurationBuilder BuildBaseConfiguration(string rootPath, string environmentName)
        {
            //Log.Information("Building Configuration from Path {rootPath} for environmentName {environmentName}", rootPath, environmentName.ToLower());

            var builder = new ConfigurationBuilder().SetBasePath(rootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{environmentName.ToLower()}.json", true, true)
                .AddJsonFile($"appsettings.{environmentName.ToLower()}.private.json", true, true)
                .AddEnvironmentVariables();

            return builder;
        }

        public IConfigurationRoot Configuration { get; }

        public static string EnvironmentName => GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").ToLower();
        public ConnectionString ConnectionString => new ConnectionString(GetEnvironmentVariable("TBI_CONNECTIONSTRING"));

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
