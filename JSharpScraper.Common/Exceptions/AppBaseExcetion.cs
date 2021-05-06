using System;

namespace JSharpScraper.Common.Exceptions
{
    public class AppBaseException : Exception
    {
        public AppBaseException(string msg) : base(msg) { }
    }
}