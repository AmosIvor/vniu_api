using AutoMapper;
using vniu_api.Models.EF.Carts;
using vniu_api.Models.EF.Orders;
using vniu_api.Models.EF.Payments;
using vniu_api.Models.EF.Profiles;
using vniu_api.Models.EF.Promotions;
using vniu_api.Models.EF.Reviews;
using vniu_api.Models.EF.Shippings;
using vniu_api.ViewModels.CartsViewModels;
using vniu_api.ViewModels.OrdersViewModels;
using vniu_api.ViewModels.PaymentsViewModels;
using vniu_api.ViewModels.ProfilesViewModels;
using vniu_api.ViewModels.PromotionsViewModels;
using vniu_api.ViewModels.ReviewsViewModels;
using vniu_api.ViewModels.ShippingViewModels;

namespace vniu_api.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // carts
            CreateMap<Cart, CartVM>().ReverseMap();
            CreateMap<CartItem, CartItemVM>().ReverseMap();

            // orders
            CreateMap<Order, OrderVM>().ReverseMap();
            CreateMap<OrderLine, OrderLineVM>().ReverseMap();
            CreateMap<OrderStatus, OrderStatusVM>().ReverseMap();

            // payments
            CreateMap<PaymentMethod, PaymentMethodVM>().ReverseMap();
            CreateMap<PaymentType, PaymentTypeVM>().ReverseMap();

            // products

            // profiles
            CreateMap<User, UserVM>().ReverseMap();
            CreateMap<Address, AddressVM>().ReverseMap();
            CreateMap<UserAddress, UserAddressVM>().ReverseMap();

            // promotions
            CreateMap<Promotion, PromotionVM>().ReverseMap();

            // reviews
            CreateMap<Review, ReviewVM>().ReverseMap();
            CreateMap<ReviewImage, ReviewImageVM>().ReverseMap();

            // shippings
            CreateMap<ShippingMethod, ShippingMethodVM>().ReverseMap();
        }
    }
}
