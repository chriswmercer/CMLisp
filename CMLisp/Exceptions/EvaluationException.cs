using System;
namespace CMLisp.Exceptions
{
    public class EvaluationException : Exception
    {
        private Exception innerException;

        public EvaluationException(string message, Exception innerException) : base(message)
        {
            this.innerException = innerException;
        }
    }
}
