using System; using Microsoft.AspNetCore.Builder; using Microsoft.AspNetCore.Hosting; using Microsoft.Extensions.Configuration; using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Rewrite; using Microsoft.IdentityModel.Tokens; using System.Text;

namespace APP1 {
    public class Startup
	{

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }


		/*****************************************/
		/***Méthode pour la génération du token***/
		/***Mis en place mais non utilisé ici*****/
        /*****************************************/
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
					  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("P@ssw0rd")),

					  ValidateIssuer = true,
					  ValidIssuer = "University",

					  ValidateAudience = true,
					  ValidAudience = "The name of the audience",

					  ValidateLifetime = true, //validate the expiration and not before values in the token                   ClockSkew = TimeSpan.FromMinutes(5) //5 minute tolerance for the expiration date                };
			  });
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
             /*Redirection vers l'HTTPS*/
			var options = new RewriteOptions()
			    .AddRedirectToHttps();

			app.UseRewriter(options);

			app.UseAuthentication(); /*Pour l'authentification*/

			app.UseMvc();
		}
	} }  