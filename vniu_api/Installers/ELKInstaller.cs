using vniu_api.Configuration;
//using Nest;
using vniu_api.ViewModels.ProfilesViewModels;
using vniu_api.Repositories.Utils;
using vniu_api.Services.Utils;
using Elastic.Clients.Elasticsearch;
using Nest;

namespace vniu_api.Installers
{
    public class ELKInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var elkConfiguration = new ELKConfiguration();

            configuration.GetSection("ELKConfiguration").Bind(elkConfiguration);

            var connectionSettings = new ConnectionSettings(new Uri(elkConfiguration.BaseUrl))
                .PrettyJson()
                .DefaultIndex(elkConfiguration.DefaultIndex);

            AddDefaultMappings(connectionSettings);

            var elasticClient = new ElasticClient(connectionSettings);

            services.AddSingleton<IElasticClient>(elasticClient);

            services.AddScoped<IElasticSearchService<AddressVM>, ElasticSearchService<AddressVM>>();

            CreateIndex(elasticClient, elkConfiguration.DefaultIndex);
        }

        private static void AddDefaultMappings(ConnectionSettings connectionSettings)
        {
            connectionSettings.DefaultMappingFor<AddressVM>(a =>
                a.Ignore(x => x.UnitNumber)
                .Ignore(x => x.AddressLine2)
            );
        }

        private static void CreateIndex(IElasticClient elasticClient, string indexName)
        {
            elasticClient.Indices.Create(indexName, i => i.Map<AddressVM>(a => a.AutoMap()));
        }

    }
}
