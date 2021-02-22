using System;
namespace CMLisp.Exceptions
{
    public class LanguageException : Exception
    {
        public LanguageException(string error) : base(error)
        {
        }

        public LanguageException(string error, Exception innerException) : base (error, innerException)
        {

        }
    }
}
