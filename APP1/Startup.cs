using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore; /*Ajout pour le Dossier Model*/
using APP1.Models; /*Ajout*/
using Microsoft.AspNetCore.Rewrite;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;


namespace APP1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
			// secretKey contains a secret passphrase only your server knows
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("a secret that needs to be at least 16 characters long"));

            var claims = new Claim[] {
            new Claim(ClaimTypes.Name, "John"),
            new Claim(JwtRegisteredClaimNames.Email, "john.doe@blinkingcaret.com"),
            new Claim(JwtRegisteredClaimNames.Exp, $"{new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds()}"),
            new Claim(JwtRegisteredClaimNames.Nbf, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}")
            };

            var token = new JwtSecurityToken(new JwtHeader(new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)), new JwtPayload(claims));

            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);


			Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

			services.AddAuthentication(options => {
				options.DefaultAuthenticateScheme = "JwtBearer";
				options.DefaultChallengeScheme = "JwtBearer";
			})
	        .AddJwtBearer("JwtBearer", jwtBearerOptions =>
	        {
		        jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
		        {
			        ValidateIssuerSigningKey = true,
			        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your secret goes here")),

			        ValidateIssuer = true,
			        ValidIssuer = "The name of the issuer",

			        ValidateAudience = true,
			        ValidAudience = "The name of the audience",

			        ValidateLifetime = true, //validate the expiration and not before values in the token

			        ClockSkew = TimeSpan.FromMinutes(5) //5 minute tolerance for the expiration date
		        };
	        });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

			var options = new RewriteOptions()
			   .AddRedirectToHttps();

            app.UseRewriter(options);

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
