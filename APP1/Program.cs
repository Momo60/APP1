using System.Net; /*Ajout pour le HTTPS*/
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace APP1
{
    public class Program
    {
        public static void Main(string[] args)
        {

            BuildWebHost(args).Run();
        }

        /******************************/
        /****Mise en place du HTTPS****/
        /******************************/

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
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
}
