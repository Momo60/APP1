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


namespace APP1
{
    public class Startup
    {
       
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<SondageContext>(opt => opt.UseInMemoryDatabase("Sondage"));
			services.AddMvc();
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

            app.UseMvc();
        }
    }
}
