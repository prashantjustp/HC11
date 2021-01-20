using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HC11.GraphQL
{
    public class Mutation
    {
        public IConfiguration Configuration { get; }

        public Mutation(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public string Login(Login loginModel)
        {
            //Password validation logic
            if (true)
            {
                var claims = new List<Claim>
            {
                 new Claim(ClaimTypes.Name, "UserName"),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                 new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(), ClaimValueTypes.Integer64)

            };

                claims.Add(new Claim("UserId", "123"));
                claims.Add(new Claim("CompanyId", "45"));

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));

                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(Configuration["Jwt:Issuer"], Configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddSeconds(Convert.ToInt32(Configuration["Jwt:ExpiryInSec"])), signingCredentials: signIn);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            throw new UnauthorizedAccessException();
        }
    }
    public class Login
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public string Password { get; set; }
    }
}
