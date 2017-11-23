using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace JadeLikeFairies
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
                .ConfigureAppConfiguration(BuildConfiguration)
                .Build();

        public static void BuildConfiguration(WebHostBuilderContext context, IConfigurationBuilder configurationBuilder) 
            => configurationBuilder
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"config/appsettings.{context.HostingEnvironment.EnvironmentName}.json", true, true)
            .AddEnvironmentVariables()
            .Build();
    }
}
