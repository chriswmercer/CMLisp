using System;
using CMLisp.Types;

namespace CMLisp.Keywords
{
    public class MessageKeyword
    {
        public BaseType Evaluate(BaseType[] input)
        {
            var label = input[0]?.Value.ToString() ?? "nil";
            return new StringType(label);
        }
    }
}
