using JSharpScraper.AppService.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;

namespace JSharpScraper.Selenium
{
    public abstract class BaseScraper : IScraper
    {
        protected readonly IWebDriver _driver;
        protected readonly IJavaScriptExecutor _js;
        protected readonly string _baseUrl;
        protected readonly int _recordsPerPage;
        protected readonly int _maximumAttempts;
        protected const string _funcGetByXPath = @"function getElementByXpath(path) {
                                                      return document.evaluate(path, document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;
                                                    }
                                                   getElementByXpath(#path#);";

        public BaseScraper(string baseUrl)
        {
            var chromeOptions = new ChromeOptions();
            
            var service = ChromeDriverService.CreateDefaultService(AppDomain.CurrentDomain.BaseDirectory);
            _driver = new ChromeDriver(service, chromeOptions);
            
            _js = (IJavaScriptExecutor)_driver;
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _recordsPerPage = 50;
            _maximumAttempts = 10;
        }

        public void Dispose()
        {
            _driver.Quit();
        }

        public IReadOnlyCollection<IWebElement> FindByClass(string key, IWebElement element = null)
        {
            if(element != null)
                element.FindElements(By.ClassName(key));

            return _driver.FindElements(By.ClassName(key));
        }

        public IWebElement FindById(string key, IWebElement element = null)
        {
            if (element != null)
                element.FindElement(By.Id(key));

            return _driver.FindElement(By.Id(key));
        }

        public IReadOnlyCollection<IWebElement> FindByTag(string key, IWebElement element = null)
        {
            if (element != null)
                element.FindElements(By.CssSelector(key));

            return _driver.FindElements(By.CssSelector(key));
        }


        public IReadOnlyCollection<IWebElement> FindByXPath(string key, IWebElement element = null)
        {
            var result = (IReadOnlyCollection<IWebElement>)_js.ExecuteScript(_funcGetByXPath.Replace("#path#",key));
            return result;
        }

        public string FindJob(string url, string jobKey)
        {
            throw new NotImplementedException();
        }
    }
}
