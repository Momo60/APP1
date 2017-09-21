using System;
using System.Net; /*Ajout pour le HTTPS*/
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace APP1
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var secretKey = new SymmetricSecurityKey(Endoding.UTF8.GetBytes("a secret that needs to be at least 16 characters long"));

            var claims = new Claim[] {
                new Claim(ClaimTypes.Name, "John"),
                new Claim(JwtRegisteredClaimNames.Email, "john.doe@blinkingcaret.com"),
                new Claim(JwtRegisteredClaimNames.Exp, $"{new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Nbf, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}")

            };

            var token = new JwtSecurityToken(new JwtHeader(new SigningCredentials(key, SecurityAlgorithms.HmacSha256)), new JwtPayload(claims));
            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            BuildWebHost(args).Run();
        }



        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()

				/*Config du WebServer avec le HTTPS*/ 
				.UseKestrel(options => 
				{
                    /*Pour changer le port modifier le chiffre*/
					options.Listen(IPAddress.Loopback, 5001, listenOptions =>
					{
                        /*Utilisation des certificats*/
						listenOptions.UseHttps("certificat.pfx", "P@ssw0rd");
					});
				})
                   
                .Build();
                       
    }
}
