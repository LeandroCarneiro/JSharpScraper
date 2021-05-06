using JSharpScraper.Common.Exceptions;
using OpenQA.Selenium;
using System.Linq;

namespace JSharpScraper.Selenium.Scrapers
{
    public partial class NavagateScraper : BaseScraper
    {
        public override IWebElement FindJobPage(IWebElement element = null)
        {
            var btns = FindByXPath("career");
            if(btns != null && btns.Any())
            {
                foreach (var item in btns)
                {
                    if (item.TagName == "a" || (item.TagName == "input" || item.GetAttribute("type") == "button") && element != item)
                        return item;
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
