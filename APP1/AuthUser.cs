/**************************************
* Méthode pour la génération du token *
***************************************/

using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace APP1
{
    public class AuthUser
    {
        public static string GenerateToken(string userId)
        {
			// secretKey contains a secret passphrase only your server knows
			var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("stephane_mohammed"));

			var claims = new Claim[] {
                new Claim(ClaimTypes.Name, userId),
                new Claim(JwtRegisteredClaimNames.Email, userId+"@usherbrooke.ca"),
			new Claim(JwtRegisteredClaimNames.Exp, $"{new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds()} "),
			new Claim(JwtRegisteredClaimNames.Nbf, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()} ")
			};

			var token = new JwtSecurityToken(new JwtHeader(new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)), new JwtPayload(claims));

			string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtToken; /*retour du token*/

        }
    }
}
