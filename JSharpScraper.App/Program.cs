using JSharpScraper.AppService;
using JSharpScraper.Common.Exceptions;
using JSharpScraper.Selenium.Scrapers;
using System;
using System.Collections.Generic;

namespace JSharpScraper.App
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Startup.CreateHostBuilder(args);
                var appService = new ScraperAppService(new NavagateScraper());

                var list = new List<string>()
                {
                    "https://www.epam.com/"
                };

                foreach (var item in list)
                {
                    try
                    {
                        Console.WriteLine(appService.Go(item, ".net"));
                    }
                    catch (AppBaseException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }                
            }            
            catch (Exception ex)
            {
                Console.WriteLine("System could not process it. Error: " + ex.Message);
            }            
        }
    }
}
