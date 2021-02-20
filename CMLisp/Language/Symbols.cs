using System;
using System.Collections.Generic;
using CMLisp.Language;
using CMLisp.Operations;
using CMLisp.Types;

namespace CMLisp.Language
{
    public static class Symbols
    {
        private static Dictionary<string, Func<BaseType[], BaseType>> FunctionLookup = new Dictionary<string, Func<BaseType[], BaseType>>()
        {
            {"+", (x) => Addition.SumOf(x)},
            {"-", (x) => Subtraction.MinusSumOf(x)},
            {"*", (x) => Multiplication.ProductOf(x)},
            {"/", (x) => Division.DivisorOf(x)},
            {"=", (x) => Equality.EqualityOf(x) },
            {"==", (x) => Equality.EqualityOf(x)},
            {"!=", (x) => new BooleanType(!(Equality.EqualityOf(x).Value))}
        };

        public static Func<BaseType[], BaseType> FunctionFor(string functionName)
        {
            if (!FunctionLookup.ContainsKey(functionName)) throw new ArgumentException($"Function { functionName} was not recognised.");
            return FunctionLookup[functionName];
        }

        public static bool IsKnown(string potentialSymbol)
        {
            return FunctionLookup.ContainsKey(potentialSymbol);
        }
    }
}
