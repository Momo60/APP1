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


namespace APP1
{
    public class Program
    {
        public static void Main(string[] args)
        {
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
