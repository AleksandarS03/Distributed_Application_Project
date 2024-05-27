using Microsoft.IdentityModel.Tokens;
using MotorcycleCatalog.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MotorcycleCatalog.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly Dictionary<string, string> _admin = new()
        {
            { "user", "1234" }
        };

        private readonly string _tokenKey;
        /// <summary>
        /// Constructor for the authentication
        /// </summary>
        /// <param name="tokenKey"></param>
        public AuthRepository(string tokenKey)
        {
            _tokenKey = tokenKey;
        }


        /// <summary>
        /// Authenticates an admin user based on the provided admin ID and secret.
        /// </summary>
        /// <param name="adminId">The admin ID of the user attempting to authenticate.</param>
        /// <param name="secret">The secret key corresponding to the admin ID.</param>
        /// <returns>
        /// A JWT token as a string if authentication is successful; otherwise, null.
        /// </returns>
        public string? Authenticate(string adminId, string secret)
        {
            if (!_admin.Any(x => x.Key == adminId && x.Value == secret))
            {
                return null;
            }

            JwtSecurityTokenHandler handler = new();
            var key = Encoding.ASCII.GetBytes(_tokenKey);
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new(new Claim[]
                {
                    new(ClaimTypes.Name, adminId),
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }
    }
}
