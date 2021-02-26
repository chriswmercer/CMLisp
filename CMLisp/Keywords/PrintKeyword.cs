using System;
using System.Collections.Generic;
using CMLisp.Core;
using CMLisp.Types;

namespace CMLisp.Keywords
{
    public class PrintKeyword
    {
        public BaseType Evaluate(BaseType[] input, bool newline = false)
        {
            foreach(var item in input)
            {
                var evaluated = Evaluator.Evaluate(item, Evaluator.LocalScope);

                string result = evaluated.Value.ToString().Replace("\\n", "\n");

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
