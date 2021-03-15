using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;

namespace VetScraper.Domain
{
    public class VetClinic
    {
        [BsonId]
        public string ExternalId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
