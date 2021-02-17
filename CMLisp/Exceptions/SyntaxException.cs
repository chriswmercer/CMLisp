using System;
namespace CMLisp.Exceptions
{
    public class SyntaxException : Exception
    {
        public SyntaxException(string error) : base(error)
        {
        }

        public SyntaxException(string error, Exception innerException) : base (error, innerException)
        {

        }
    }
}
