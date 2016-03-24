using System;

namespace Store
{
    /// <summary>
    /// Required product does not exist and can't be found
    /// </summary>
    public class InvalidInputValueForSearchException : Exception
    {
        /// <summary>
        /// Initializes a new instance of InvalidInputValueForSearchException w/o parameters
        /// </summary>
        public InvalidInputValueForSearchException() {}

        /// <summary>
        /// Initializes a new instance of InvalidInputValueForSearchException constructor with parameters
        /// </summary>
        public InvalidInputValueForSearchException(string exceptionMessage, string actualSearchValue)
            : base(exceptionMessage){}
    }
}
