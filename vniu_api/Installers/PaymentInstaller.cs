using vniu_api.Configuration;
using vniu_api.Repositories.Utils;
using vniu_api.Services.Utils;

namespace vniu_api.Installers
{
    public class PaymentInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<VnPayConfiguration>(configuration.GetSection("VnPayConfiguration"));

            services.AddTransient<IVnPayService, VnPayService>();

            services.Configure<PayPalConfiguration>(configuration.GetSection("PayPalConfiguration"));

            services.AddTransient<IPayPalService, PayPalService>();
        }
    }
}
