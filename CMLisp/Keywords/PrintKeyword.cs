using System;
using System.Collections.Generic;
using CMLisp.Core;
using CMLisp.Types;
using CMLisp.Language;

namespace CMLisp.Keywords
{
    public class PrintKeyword
    {
        public BaseType Evaluate(BaseType[] input, bool newline = false)
        {
            foreach (var item in input)
            {
                var evaluated = Evaluator.Evaluate(item, Evaluator.LocalScope);
                var value = evaluated.Value;

                string result = string.Empty;

                if (evaluated.Type == LanguageTypes.Array)
                {
                    value = (List<BaseType>)value;
                    result = Extensions.ToString(value);
                }
                else
                {
                    result = value.ToString();
                }

                result = result.Replace("\\n", "\n");

                if (result == "Nil") result = "";

                if (newline)
                {
                    Console.WriteLine($"{ result }");
                }
                else
                {
                    Console.Write($"{ result }");
                }
            }

            return new NilType();
        }
    }
}
