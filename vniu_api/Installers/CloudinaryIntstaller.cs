using vniu_api.Configuration;

namespace vniu_api.Installers
{
    public class CloudinaryIntstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CloudinarySetting>(configuration.GetSection("CloudinarySetting"));
        }
    }
}
