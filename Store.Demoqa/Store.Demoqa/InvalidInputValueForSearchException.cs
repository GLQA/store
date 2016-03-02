using System;

namespace Store.Demoqa
{
    public class InvalidInputValueForSearchException : Exception
    {
        public InvalidInputValueForSearchException()
        {

        }

        public InvalidInputValueForSearchException(string exceptionMessage, string actualSearchValue)
            : base(exceptionMessage)
        {

        }
    }
}
