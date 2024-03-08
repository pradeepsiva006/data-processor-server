using DataProcessor.Common.Helper;
using DataProcessor.Core.DataStore;
using DataProcessor.Core.Implementations;
using DataProcessor.Core.Interfaces;

namespace DataProcessor.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<IDataService, DataService>();
            services.AddTransient<IDataUtility, DataUtility>();
            services.AddSingleton<IDataStore, DataStore>();
        }
    }
}
