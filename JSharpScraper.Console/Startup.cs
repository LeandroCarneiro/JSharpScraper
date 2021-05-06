using JSharpScraper.AppService;
using JSharpScraper.AppService.Interfaces;
using JSharpScraper.Selenium.Scrapers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace JSharpScraper.Console
{
    public class Startup
    {
        public static IHostBuilder CreateHostBuilder(string[] args) =>
             Host.CreateDefaultBuilder(args)
                 //Dependency Injection :)
                 .ConfigureServices((_, services) => 
                 services
                    .AddTransient<IScraper, NavagateScraper>()
                    .AddTransient<ScraperAppService>()
                 );
    }
}
