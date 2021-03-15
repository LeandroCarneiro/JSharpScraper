using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetScraper.Domain;

namespace VetScraper.Repository.MongoRepositories
{
    public class MongoVetClinicRepository : IVetClinicRepository
    {
        private readonly IMongoCollection<VetClinic> collection;

        public MongoVetClinicRepository(DatabaseConfig config)
        {
            var client = new MongoClient(config.ConnectionString);
            var database = client.GetDatabase("vetScraper");
            collection = database.GetCollection<VetClinic>("vetClinic");
        }

        public async Task<IList<VetClinic>> ListClinics(int skip, int limit)
        {
            return await collection.Find(o => o.Name != null).Skip(skip).Limit(limit).ToListAsync();
        }

        public async Task SaveClinics(IList<VetClinic> clinics)
        {
            if (clinics == null || !clinics.Any())
                return;

            await collection.InsertManyAsync(clinics, new InsertManyOptions()
            {
                IsOrdered = false,
            });
        }
    }
}
