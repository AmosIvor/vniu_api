using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using vniu_api.Models.EF.Carts;
using vniu_api.Models.EF.Orders;
using vniu_api.Models.EF.Payments;
using vniu_api.Models.EF.Products;
using vniu_api.Models.EF.Profiles;
using vniu_api.Models.EF.Promotions;
using vniu_api.Models.EF.Reviews;
using vniu_api.Models.EF.Shippings;

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

        // orders
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }

        // payments
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }

        // products

        // profiles
        public DbSet<User> Users {  get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<UserAddress> UserAddresses {  get; set; }

        // Promotions
        public DbSet<Promotion> Promotions {  get; set; }

        // reviews
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ReviewImage> ReviewImages { get; set; }

        // shippings
        public DbSet<ShippingMethod> ShippingMethods { get; set; }

        //
        public DbSet<SizeOption> Sizes { get; set; }
        public DbSet<Colour> Colours { get; set; }
        public DbSet<ProductCategory> Categories { get; set; }
        public DbSet<ProductItem> ProductItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Variation> Variations { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }


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


        }

    }
}
