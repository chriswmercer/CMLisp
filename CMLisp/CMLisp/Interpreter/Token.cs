using System;
namespace CMLisp.Interpreter
{
    public class Token
    {
        public string Value { get; set; }

        public Token(string token)
        {
            Value = token.ToLower();
        }
    }
}
