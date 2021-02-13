using System;
namespace CMLisp.Exceptions
{
    public class SyntaxException : Exception
    {
        public SyntaxException(string error) : base(error)
        {
        }
    }
}
