using System;
using System.Collections.Generic;
using VetScraper.Domain;
using VetScraper.DataCollector;
using VetScraper.DataCollector.Sources;
using System.Linq;
using VetScraper.Repository;
using VetScraper.Repository.MongoRepositories;

namespace VetScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            IList<VetClinic> processedEntries = new List<VetClinic>();
            IVetClinicRepository repo = new MongoVetClinicRepository("mongodb://localhost:27017/");
            int totalEntries = 0, currentPage = 0;

            using (var scraper = GetPetMedsScraper())
            {
                do
                {
                    try
                    {
                        Console.WriteLine($"Processing page number {currentPage + 1}");
                        processedEntries = scraper.ProcessNextPage(currentPage);
                        repo.SaveClinics(processedEntries).GetAwaiter().GetResult();
                        totalEntries += processedEntries.Count;
                        Console.WriteLine($"{processedEntries.Count} entries processed on page {currentPage + 1}. {totalEntries} entries processed so far.");
                        currentPage++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing page number {currentPage + 1}. Exception: {ex.Message}");
                    }

                } while (processedEntries.Any());
            }

            Console.WriteLine($"A total of {totalEntries} entries have been processed.");
            Console.Read();
        }


        private static IClinicScraper GetPetMedsScraper()
        {
            // TODO: this would be coming from a factory or DI
            return new PetMedsScraper();
        }
    }
}
