using vniu_api.Configuration;
using vniu_api.Repositories.Utils;
using vniu_api.ViewModels.UtilsViewModels;

namespace vniu_api.Installers
{
    public class MailInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var mailConfiguration = new MailConfiguration();

            configuration.GetSection("MailConfiguration").Bind(mailConfiguration);

            services.Configure<MailConfiguration>(configuration.GetSection("MailConfiguration"));

            services.AddTransient<ISendMailService, SendMailService>();
        }
    }
}
