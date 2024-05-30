using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using vniu_api.Repositories;

namespace vniu_api.Helpers
{
    public static class AppUtils
    {
        public static JwtSecurityToken GetToken(List<Claim> authClaims, IConfiguration configuration)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                    issuer: configuration["JWT:ValidIssuer"],
                    audience: configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddYears(2),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha512Signature)
                );

            return token;
        }

        public static async Task<string> GenerateUserID(DataContext context)
        {
            var ID = "CS0001";

            var maxID = await context.Users
                .OrderByDescending(v => v.Id)
                .Select(v => v.Id)
                .FirstOrDefaultAsync();

            if (string.IsNullOrEmpty(maxID))
            {
                return ID;
            }

            var numeric = Regex.Match(maxID, @"\d+").Value;

            if (string.IsNullOrEmpty(numeric))
            {
                return ID;
            }

            ID = "CS";

            numeric = (int.Parse(numeric) + 1).ToString();

            while (ID.Length + numeric.Length < 6)
            {
                ID += '0';
            }

            return ID + numeric;
        }
    }
}
