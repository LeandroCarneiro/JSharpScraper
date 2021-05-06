using JSharpScraper.AppService.Interfaces;
using System;

namespace JSharpScraper.AppService
{
    public class ScraperAppService
    {
        private IScraper _scraper;

        public ScraperAppService(IScraper scraper)
        {
            _scraper = scraper;
        }

        public string Go(string url, string jobKey)
        {
            _scraper.Setup(url, jobKey);
            return _scraper.Navagate();
        }
    }
}
