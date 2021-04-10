using TransactionStore.Core.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;

namespace EducationSystem.Data.Tests
{
    [ExcludeFromCodeCoverage]
    public static class TestHelper
    {
        public static IConfigurationRoot GetIConfigurationRoot(string outputPath)
        {
            return new ConfigurationBuilder()
                .SetBasePath(outputPath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Testing.json")
                .Build();
        }

        public static AppSettings GetApplicationConfiguration(string outputPath)
        {
            var configuration = new AppSettings();
            var iConfig = GetIConfigurationRoot(outputPath);

            iConfig
                .Bind(configuration);

            return configuration;
        }

        public static IOptions<AppSettings> GetIOptionsFromAppSettings(string outputPath)
        {
            var settings = GetApplicationConfiguration(outputPath);
            IOptions<AppSettings> appSettingsOptions = Options.Create(settings);

            return appSettingsOptions;
        }
    }
}
