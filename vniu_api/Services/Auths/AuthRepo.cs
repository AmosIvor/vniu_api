using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using vniu_api.Constants;
using vniu_api.Helpers;
using vniu_api.Models.EF.Auths;
using vniu_api.Models.EF.Carts;
using vniu_api.Models.EF.Profiles;
using vniu_api.Models.Responses;
using vniu_api.Repositories;
using vniu_api.Repositories.Auths;
using vniu_api.ViewModels.ProfilesViewModels;

namespace vniu_api.Services.Auths
{
    public class AuthRepo : IAuthRepo
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AuthRepo(DataContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager,
            IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<AuthResponse> SignInAsync(UserLogin userLogin)
        {
            // check username
            var user = await _userManager.FindByEmailAsync(userLogin.Email);

            if (user == null)
            {
                throw new ArgumentNullException("User not found");
            }

            //check password

            var passwordValid = await _userManager.CheckPasswordAsync(user, userLogin.Password);

            if (!passwordValid)
            {
                throw new KeyNotFoundException("Wrong password");
            }

            // role in token
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }

            // get token
            var jwtToken = Utils.GetToken(authClaims, _configuration);

            // auth response
            var authResponse = new AuthResponse
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                User = new UserVM()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email
                }
            };

            return authResponse;
        }

        public async Task<UserVM> SignUpAsync(UserRegister userRegister)
        {
            // Check User Exist
            var checkMail = await _userManager.FindByEmailAsync(userRegister.Email);

            if (checkMail != null)
            {
                throw new Exception("Email existed!");
            }

            var checkUserName = await _userManager.FindByNameAsync(userRegister.UserName);

            if (checkUserName != null)
            {
                throw new Exception("UserName existed");
            }


            var user = new User()
            {
                // default field
                Id = await Utils.GenerateUserID(_context),
                UserName = userRegister.UserName,
                Email = userRegister.Email,
                PhoneNumber = userRegister.Phone,
                SecurityStamp = Guid.NewGuid().ToString(),

                // custom field
                Male = true,
                DateBirth = DateTime.MinValue,
            };

            var result = await _userManager.CreateAsync(user, userRegister.Password);

            if (!result.Succeeded)
            {
                throw new Exception("User creation failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            // Check role exist
            var roleExist = await _roleManager.RoleExistsAsync(AppRoles.CUSTOMER);

            if (!roleExist)
            {
                // it doesn't exist customer role => create
                var roleCustomer = new IdentityRole()
                {
                    Name = AppRoles.CUSTOMER,
                    ConcurrencyStamp = "1",
                    NormalizedName = AppRoles.CUSTOMER.ToUpper(),
                };

                await _roleManager.CreateAsync(roleCustomer);
            }

            await _userManager.AddToRoleAsync(user, AppRoles.CUSTOMER);

            // Create cart each user register succeeded
            var cart = new Cart()
            {
                UserId = user.Id,
            };

            _context.Carts.Add(cart);


            // Save changes
            await _context.SaveChangesAsync();

            // Map user to userVM
            var userVM = _mapper.Map<UserVM>(user);

            return userVM;
        }
    }
}
