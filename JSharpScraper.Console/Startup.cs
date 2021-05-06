using JSharpScraper.AppService;
using JSharpScraper.AppService.Interfaces;
using JSharpScraper.Selenium.Scrapers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace JSharpScraper.App
{
    public static class Startup
    {
        private static IServiceCollection _container;

        public static IHostBuilder CreateHostBuilder(string[] args) =>
             Host.CreateDefaultBuilder(args)
                 //Dependency Injection :)
                 .ConfigureServices((_, services) => 
                 _container =  services
                    .AddTransient<IScraper, NavagateScraper>()
                    .AddTransient<ScraperAppService>()
                 );

        public static T Resolve<T>()
        {
            var serviceProvider = _container.BuildServiceProvider();
            var service = serviceProvider.GetService<T>();

            return service;
        }
    }
}
