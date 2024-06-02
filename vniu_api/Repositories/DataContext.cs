using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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

namespace vniu_api.Repositories
{
    public class DataContext : IdentityDbContext<IdentityUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        #region Init DbSet

        // carts
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        // chats
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<Message> Messages { get; set; }

        // orders
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }

        // payments
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }

        // products
        public DbSet<SizeOption> SizeOptions { get; set; }
        public DbSet<Colour> Colours { get; set; }
        public DbSet<ProductCategory> Categories { get; set; }
        public DbSet<ProductItem> ProductItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Variation> Variations { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

        // profiles
        public DbSet<User> Users {  get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<UserAddress> UserAddresses {  get; set; }

        // promotions
        public DbSet<Promotion> Promotions {  get; set; }

        public DbSet<PromotionCategory> PromotionCategories { get; set; }

        // reviews
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ReviewImage> ReviewImages { get; set; }

        // shippings
        public DbSet<ShippingMethod> ShippingMethods { get; set; }

        // utils
        public DbSet<Photo> Photos { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User_Address
            modelBuilder.Entity<UserAddress>()
                .HasKey(ua => new { ua.UserId, ua.AddressId });

            modelBuilder.Entity<UserAddress>()
                .HasOne(u => u.User)
                .WithMany(ua => ua.UserAddresses)
                .HasForeignKey(ua => ua.UserId);

            modelBuilder.Entity<UserAddress>()
                .HasOne(a => a.Address)
                .WithMany(ua => ua.UserAddresses)
                .HasForeignKey(ua => ua.AddressId);

            // Review_OrderLine
            modelBuilder.Entity<OrderLine>()
                .HasOne(ol => ol.Review)
                .WithOne(r => r.OrderLine)
                .HasForeignKey<Review>(r => r.OrderLineId);

            // Decimal Precision
            modelBuilder.Entity<Order>()
                .Property(o => o.OrderTotal)
                .HasPrecision(18, 2);

            modelBuilder.Entity<OrderLine>()
                .Property(ol => ol.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<ProductItem>()
                .Property(pi => pi.OriginalPrice);
                //.HasPrecision(18, 2);

            modelBuilder.Entity<ProductItem>()
                .Property(pi => pi.SalePrice);
            //.HasPrecision(18, 2);

            modelBuilder.Entity<ProductItem>()
                .Property(pi => pi.ProductItemRating);
                //.HasPrecision(18, 2);

            modelBuilder.Entity<ShippingMethod>()
                .Property(sm => sm.ShippingMethodPrice)
                .HasPrecision(18, 2);

            // User_Cart
            modelBuilder.Entity<User>()
                .HasOne(u => u.Cart)
                .WithOne(c => c.User)
                .HasForeignKey<Cart>(c => c.UserId);

            // CartItem
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Variation)
                .WithMany(v => v.CartItems)
                .HasForeignKey(ci => ci.VariationId)
                .OnDelete(DeleteBehavior.NoAction);

            // OrderLine
            modelBuilder.Entity<OrderLine>()
                .HasOne(ol => ol.Variation)
                .WithMany(v => v.OrderLines)
                .HasForeignKey(ol => ol.VariationId)
                .OnDelete(DeleteBehavior.NoAction);

            // Review
            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            // Order
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            // ProductCategory
            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.ParentCategory)
                .WithMany(pc => pc.ChildProductCategories)
                .HasForeignKey(pc => pc.ParentCategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            // Promotion_Category
            modelBuilder.Entity<PromotionCategory>()
                .HasKey(pc => new { pc.PromotionId, pc.ProductCategoryId });

            modelBuilder.Entity<PromotionCategory>()
                .HasOne(pc => pc.Promotion)
                .WithMany(p => p.PromotionCategories)
                .HasForeignKey(pc => pc.PromotionId);

            modelBuilder.Entity<PromotionCategory>()
                .HasOne(pc => pc.ProductCategory)
                .WithMany(c => c.PromotionCategories)
                .HasForeignKey(pc => pc.ProductCategoryId);

            // ChatRoom
            modelBuilder.Entity<ChatRoom>()
                .HasOne(r => r.User)
                .WithMany(u => u.ChatRooms)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            // Message
            modelBuilder.Entity<Message>()
                .HasOne(r => r.ChatRoom)
                .WithMany(u => u.Messages)
                .HasForeignKey(r => r.ChatRoomId)
                .OnDelete(DeleteBehavior.NoAction);
        }

    }
}
