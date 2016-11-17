using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Api
{
    public class Program
    {
        #region Public Methods

        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>();

            if (args.Length == 1)
            {
                host.UseUrls(args[0]);
            }

            host.Build().Run();
        }

        #endregion Public Methods
    }
}
