﻿using System;
using System.Collections.Generic;
using CMLisp.Core;
using CMLisp.Exceptions;
using CMLisp.Language;
using CMLisp.Types;

namespace CMLisp.Keywords
{
    public class PrintKeyword
    {
        public BaseType Evaluate(BaseType[] input, bool newline = false)
        {
            foreach(var item in input)
            {
                string result = string.Empty;
                var evaluated = Evaluator.Evaluate(item);

                if(evaluated.Type == LanguageTypes.Array)
                {
                    result = "[";
                    var values = (List<BaseType>)evaluated.Value;
                    var count = values.Count;

                    for(int i = 1; i <= count; i++)
                    {
                        result += $"{ values[i - 1].Value.ToString()}";

                        if (i != count) result += ", ";
                    }
                    result += "]";
                }
                else
                {
                    result = evaluated.Value.ToString();
                }

                if (newline)
                {
                    Console.WriteLine($"{ result } ");
                }
                else
                {
                    Console.Write($"{ result } ");
                }
            }

            return new NilType();
        }
    }
}
