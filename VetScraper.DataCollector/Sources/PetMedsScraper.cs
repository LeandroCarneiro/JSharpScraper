using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using VetScraper.Domain;

namespace VetScraper.DataCollector.Sources
{
    public class PetMedsScraper : IClinicScraper
    {
        private readonly IWebDriver driver;
        private readonly IJavaScriptExecutor js;
        private readonly string baseUrl;
        private readonly int recordsPerPage;
        private readonly int maximumAttempts;

        public PetMedsScraper()
        {
            baseUrl = "https://www.1800petmeds.com/vetdirectory?";
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless", "disable-gpu", "no-sandbox");
            var service = ChromeDriverService.CreateDefaultService(AppDomain.CurrentDomain.BaseDirectory);
            driver = new ChromeDriver(service, chromeOptions);
            js = (IJavaScriptExecutor)driver;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            recordsPerPage = 50;
            maximumAttempts = 3;
        }

        public void Dispose()
        {
            driver.Quit();
        }

        public IList<VetClinic> ProcessNextPage(int currentPage)
        {
            return ProcessPage(currentPage, 1);
        }

        private IList<VetClinic> ProcessPage(int currentPage, int attempt)
        {
            List<VetClinic> response = new List<VetClinic>();

            if (attempt == maximumAttempts)
            {
                Console.WriteLine("Maximum of attempts reached. Finishing page execution.");
                return response;
            }

            driver.Navigate().GoToUrl(GetUrl(currentPage));
            string noResultsDisplay = driver.FindElement(By.ClassName("store-locator-no-results"))?.GetCssValue("display");
            if (noResultsDisplay != "none")
            {
                Console.WriteLine($"Re-trying to process page {currentPage + 1}. Attempt: {attempt}.");
                return ProcessPage(currentPage, attempt + 1);
            }

            var elements = driver.FindElements(By.ClassName("store-details"));
            foreach (var element in elements)
                response.Add(GetClinic(element));

            return response;
        }


        private VetClinic GetClinic(IWebElement element)
        {
            var clinic = new VetClinic();
            clinic.ExternalId = element.GetAttribute("data-store-id");
            clinic.Name = element.FindElement(By.ClassName("store-name"))?.Text;
            var addressElement = element.FindElement(By.CssSelector("address"));
            if (addressElement != null)
            {
                clinic.Phone = addressElement.FindElement(By.ClassName("storelocator-phone"))?.Text;
                var addressLines = addressElement.Text.Split(Environment.NewLine);
                clinic.AddressLine1 = addressLines[0];
                if (addressLines.Length > 1)
                    clinic.AddressLine2 = addressLines[1];
            }

            return clinic;
        }

        private string GetUrl(int currentPage)
        {
            int startingPage = currentPage * recordsPerPage;

            return baseUrl + (
                  currentPage == 0 ? "horizontalView=true" :
                  $"startingPage={startingPage}&horizontalView=true&pageChange=true"
                );
        }
    }
}
