using System;

namespace JSharpScraper.Common.Exceptions
{
    public class JobNotFoundException : AppBaseException
    {
        public JobNotFoundException() : base("Job was not found")
        {
        }
    }
}
