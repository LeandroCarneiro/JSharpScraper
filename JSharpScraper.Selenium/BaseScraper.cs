using JSharpScraper.AppService.Interfaces;
using JSharpScraper.Common.Exceptions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Threading;

namespace JSharpScraper.Selenium
{
    public abstract class BaseScraper : IScraper
    {
        protected IWebDriver _driver;
        protected IJavaScriptExecutor _js;
        protected string _baseUrl;
        protected string _jobKey;
        protected int _recordsPerPage;
        protected int _maximumAttempts;
        protected const string _funcGetByXPath = @"function getElementByXpath(path) {
                                                      return document.evaluate(path, document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;
                                                    }
                                                   getElementByXpath(#path#);";

        public void Setup(string baseUrl, string jobKey)
        {
            var chromeOptions = new ChromeOptions();
            
            var service = ChromeDriverService.CreateDefaultService(AppDomain.CurrentDomain.BaseDirectory);
            _driver = new ChromeDriver(service, chromeOptions);
            
            _js = (IJavaScriptExecutor)_driver;
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _recordsPerPage = 50;
            _maximumAttempts = 10;
            _baseUrl = baseUrl;
            _jobKey = jobKey;
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

        public abstract IWebElement FindJobPage(IWebElement element = null);
        public abstract void FindJob(string jobKey);

        public string Navagate()
        {
            var btn = FindJobPage();
            btn.Click();
            
            Thread.Sleep(5000);

            FindJob(_jobKey);
            return _driver.Url;
        }
    }
}
