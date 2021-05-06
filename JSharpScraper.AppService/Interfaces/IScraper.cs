namespace JSharpScraper.AppService.Interfaces
{
    public interface IScraper
    {
        void Setup(string baseUrl, string jobKey);
        string Navagate();
    }
}
