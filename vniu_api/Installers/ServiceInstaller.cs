using vniu_api.Repositories.Auths;
using vniu_api.Repositories.Carts;
using vniu_api.Repositories.Chats;
using vniu_api.Repositories.Orders;
using vniu_api.Repositories.Payments;
using vniu_api.Repositories.Products;
using vniu_api.Repositories.Profiles;
using vniu_api.Repositories.Promotions;
using vniu_api.Repositories.Reviews;
using vniu_api.Repositories.Shippings;
using vniu_api.Repositories.Utils;
using vniu_api.Services.Auths;
using vniu_api.Services.Carts;
using vniu_api.Services.Chats;
using vniu_api.Services.Orders;
using vniu_api.Services.Payments;
using vniu_api.Services.Products;
using vniu_api.Services.Profiles;
using vniu_api.Services.Promotions;
using vniu_api.Services.Reviews;
using vniu_api.Services.Shippings;
using vniu_api.Services.Utils;

namespace vniu_api.Installers
{
    public class ServiceInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            // ADD SCOPED REPOSITORIES

            // repo-auths
            services.AddScoped<IAuthRepo, AuthRepo>();

            // repo-carts
            services.AddScoped<ICartRepo, CartRepo>();
            services.AddScoped<ICartItemRepo, CartItemRepo>();


            // repo-chats
            services.AddScoped<IChatRoomRepo, ChatRoomRepo>();
            services.AddScoped<IMessageRepo, MessageRepo>();


            // repo-orders
            services.AddScoped<IOrderRepo, OrderRepo>();
            services.AddScoped<IOrderLineRepo, OrderLineRepo>();
            services.AddScoped<IOrderStatusRepo, OrderStatusRepo>();

            // repo-payments
            services.AddScoped<IPaymentMethodRepo, PaymentMethodRepo>();
            services.AddScoped<IPaymentTypeRepo, PaymentTypeRepo>();

            // repo-products
            services.AddScoped<ISizeOptionRepo, SizeOptionRepo>();
            services.AddScoped<IColourRepo, ColourRepo>();
            services.AddScoped<IVariationRepo, VariationRepo>();
            services.AddScoped<IProductCategoryRepo, ProductCategoryRepo>();
            services.AddScoped<IProductImageRepo, ProductImageRepo>();
            services.AddScoped<IProductItemRepo, ProductItemRepo>();
            services.AddScoped<IProductRepo, ProductRepo>();

            // repo-profiles
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IAddressRepo, AddressRepo>();

            // repo-Variations
            services.AddScoped<IPromotionRepo, PromotionRepo>();

            // repo-reviews
            services.AddScoped<IReviewRepo, ReviewRepo>();
            services.AddScoped<IReviewImageRepo, ReviewImageRepo>();

            // repo-shippings
            services.AddScoped<IShippingMethodRepo, ShippingMethodRepo>();

            // repo-utils
            services.AddScoped<IPhotoService, PhotoService>();
        }
    }
}
