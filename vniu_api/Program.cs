using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using vniu_api.Models.EF.Profiles;
using vniu_api.Repositories;
using vniu_api.Repositories.Auths;
using vniu_api.Repositories.Carts;
using vniu_api.Repositories.Orders;
using vniu_api.Repositories.Payments;
using vniu_api.Repositories.Profiles;
using vniu_api.Repositories.Promotions;
using vniu_api.Repositories.Reviews;
using vniu_api.Repositories.Shippings;
using vniu_api.Services.Auths;
using vniu_api.Services.Carts;
using vniu_api.Services.Orders;
using vniu_api.Services.Payments;
using vniu_api.Services.Profiles;
using vniu_api.Services.Promotions;
using vniu_api.Services.Reviews;
using vniu_api.Services.Shippings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "VNIU API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

// Cors
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

// AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Identity
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();

// DataContext
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("VNIU_STORE"));
});

// Authentication
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"])),
        };
    });

// ADD SCOPED REPOSITORIES

// repo-auths
builder.Services.AddScoped<IAuthRepo, AuthRepo>();

// repo-carts
builder.Services.AddScoped<ICartRepo, CartRepo>();
builder.Services.AddScoped<ICartItemRepo,  CartItemRepo>();

// repo-orders
builder.Services.AddScoped<IOrderRepo, OrderRepo>();
builder.Services.AddScoped<IOrderLineRepo, OrderLineRepo>();
builder.Services.AddScoped<IOrderStatusRepo, OrderStatusRepo>();

// repo-payments
builder.Services.AddScoped<IPaymentMethodRepo, PaymentMethodRepo>();
builder.Services.AddScoped<IPaymentTypeRepo, PaymentTypeRepo>();

// repo-products

// repo-profiles
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IAddressRepo, AddressRepo>();

// repo-promotions
builder.Services.AddScoped<IPromotionRepo, PromotionRepo>();

// repo-reviews
builder.Services.AddScoped<IReviewRepo, ReviewRepo>();
builder.Services.AddScoped<IReviewImageRepo, ReviewImageRepo>();

// repo-shippings
builder.Services.AddScoped<IShippingMethodRepo, ShippingMethodRepo>();


// Build app
var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
