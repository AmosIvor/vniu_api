using Microsoft.Extensions.DependencyInjection;

namespace vniu_api.Installers
{
    public class SystemInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            //services.AddSignalR().AddAzureSignalR(configuration.GetConnectionString("AzureSignalR:ConnectionString"));
            services.AddSignalR();
            services.AddControllers();
        }
    }
}
