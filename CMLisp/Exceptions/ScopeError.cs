using System;
namespace CMLisp.Exceptions
{
    public class ScopeException : Exception
    {
        public ScopeException(string message) : base(message)
        {
        }
    }
}
