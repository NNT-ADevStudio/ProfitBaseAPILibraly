using System;

namespace ProfitBaseAPILibraly.Classes
{
    internal class CustomException : Exception
    {
        public CustomException(string message) : base(message) { }
    }
}
