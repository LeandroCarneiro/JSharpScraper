using JSharpScraper.AppService;
using JSharpScraper.Common.Exceptions;
using System;

namespace JSharpScraper.App
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Startup.CreateHostBuilder(args);
                var appService = Startup.Resolve<ScraperAppService>();

                Console.WriteLine(appService.Go("https://www.epam.com/", ".net"));
            }
            catch (AppBaseException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("System could not process it. Error: " + ex.Message);
            }            
        }
    }
}
