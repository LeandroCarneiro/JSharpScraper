using System;
using System.Collections.Generic;
using System.Text;
using VetScraper.Domain;

namespace VetScraper.DataCollector
{
    public interface IClinicScraper : IDisposable
    {
        IList<VetClinic> ProcessNextPage(int currentPage);
    }
}
