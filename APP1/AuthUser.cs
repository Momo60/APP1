/**************************************
* Méthode pour la génération du token *
*   Mis en pace mais non utilisé ici  *
***************************************/

using System;
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
			/* le secretKet contient le mot de passe pour le token*/
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
