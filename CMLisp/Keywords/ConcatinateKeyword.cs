using System;
using CMLisp.Core;
using CMLisp.Types;

namespace CMLisp.Keywords
{
    public class ConcatinateKeyword
    {
        public StringType Evaluate(BaseType[] input)
        {
            string value = string.Empty;

            foreach(var item in input)
            {
                var evaluted = item.Type == LanguageTypes.Identifier ? Evaluator.Evaluate(item, Evaluator.LocalScope) : item;
                value += evaluted.ToString();
            }

            return new StringType(value);
        }
    }
}
