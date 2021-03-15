using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VetScraper.Domain;

namespace VetScraper.Repository
{
    public interface IVetClinicRepository
    {
        Task SaveClinics(IList<VetClinic> clinics);
        Task<IList<VetClinic>> ListClinics(int skip, int limit);
    }
}
