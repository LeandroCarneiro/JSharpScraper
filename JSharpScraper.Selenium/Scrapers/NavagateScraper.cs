using JSharpScraper.Common.Exceptions;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace JSharpScraper.Selenium.Scrapers
{
    public partial class NavagateScraper : BaseScraper
    {
        public override IWebElement FindJobPage(IWebElement element = null)
        {
            _driver.Navigate().GoToUrl(_baseUrl);
            var lst = new List<string>()
            {
                "Career",
                "Careers",
                "Jobs",
                "Vacancies"
            };
            foreach (var item in lst)
            {
                var btns = FindByXPath(item);
                if (btns != null && btns.Any())
                {
                    foreach (var btn in btns)
                    {
                        if (btn.TagName == "a" || (btn.TagName == "input" || btn.GetAttribute("type") == "button") && element != btn)
                            return btn;
                    }
                }
            }
            throw new JobNotFoundException();
        }
    }   

    public partial class NavagateScraper : BaseScraper
    {
        public override void FindJob(string jobKey)
        {
            var job = FindByXPath(jobKey);
            if (job != null && job.Any())
            {
                foreach (var item in job)
                {
                    if (item.TagName == "a" || (item.TagName == "input" || item.GetAttribute("type") == "button"))
                        return;
                }
            }
            throw new JobNotFoundException();
        }
    }
}
