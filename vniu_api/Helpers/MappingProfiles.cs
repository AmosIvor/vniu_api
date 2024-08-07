﻿using AutoMapper;
using Microsoft.VisualBasic;
using vniu_api.Models.EF.Carts;
using vniu_api.Models.EF.Chats;
using vniu_api.Models.EF.Orders;
using vniu_api.Models.EF.Payments;
using vniu_api.Models.EF.Products;
using vniu_api.Models.EF.Profiles;
using vniu_api.Models.EF.Promotions;
using vniu_api.Models.EF.Reviews;
using vniu_api.Models.EF.Shippings;
using vniu_api.Models.EF.Utils;
using vniu_api.ViewModels.CartsViewModels;
using vniu_api.ViewModels.ChatsViewModels;
using vniu_api.ViewModels.OrdersViewModels;
using vniu_api.ViewModels.PaymentsViewModels;
using vniu_api.ViewModels.ProductsViewModels;
using vniu_api.ViewModels.ProfilesViewModels;
using vniu_api.ViewModels.PromotionsViewModels;
using vniu_api.ViewModels.ResponsesViewModels;
using vniu_api.ViewModels.ReviewsViewModels;
using vniu_api.ViewModels.ShippingViewModels;
using vniu_api.ViewModels.UtilsViewModels;

namespace vniu_api.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // carts
            CreateMap<Cart, CartVM>().ReverseMap();
            CreateMap<CartItem, CartItemVM>().ReverseMap();

            // chats
            CreateMap<ChatRoom, ChatRoomVM>().ReverseMap();
            CreateMap<Message, MessageVM>().ReverseMap();

            // orders
            CreateMap<Order, OrderVM>().ReverseMap().ForMember(dest => dest.OrderLines, opt => opt.MapFrom(src => src.OrderLines));
            CreateMap<OrderLine, OrderLineVM>().ReverseMap();
            CreateMap<OrderStatus, OrderStatusVM>().ReverseMap();

            // payments
            CreateMap<PaymentMethod, PaymentMethodVM>().ReverseMap();
            CreateMap<PaymentType, PaymentTypeVM>().ReverseMap();

            // products
            CreateMap<SizeOption, SizeOptionVM>().ReverseMap();
            CreateMap<Colour, ColourVM>().ReverseMap();
            CreateMap<ProductCategory, ProductCategoryVM>().ReverseMap();
            CreateMap<ProductItem, ProductItemVM>().ReverseMap();
            CreateMap<Product, ProductVM>().ReverseMap();
            CreateMap<Variation, VariationVM>().ReverseMap();
            CreateMap<ProductImage, ProductImageVM>().ReverseMap();

            // profiles
            CreateMap<User, UserVM>().ReverseMap();
            CreateMap<Address, AddressVM>().ReverseMap();
            CreateMap<Address, AddressResponseVM>().ReverseMap();

            CreateMap<UserAddress, UserAddressVM>().ReverseMap();

            // promotions
            CreateMap<Promotion, PromotionVM>().ReverseMap();
            CreateMap<PromotionCategory, PromotionCategoryVM>().ReverseMap();

            // reviews
            CreateMap<Review, ReviewVM>().ReverseMap();
            CreateMap<ReviewImage, ReviewImageVM>().ReverseMap();

            // shippings
            CreateMap<ShippingMethod, ShippingMethodVM>().ReverseMap();

            // utils
            CreateMap<Photo, PhotoVM>().ReverseMap();
        }
    }
}
