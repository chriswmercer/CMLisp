using System;
using System.Collections.Generic;
using CMLisp.Types;

namespace CMLisp.Language
{
    public static class Symbols
    {
        private static Dictionary<string, Func<BaseType, BaseType, BaseType>> FunctionLookup = new Dictionary<string, Func<BaseType, BaseType, BaseType>>()
        {
            {"+", (x, y) => x + y},
            {"-", (x, y) => x - y},
            {"*", (x, y) => x * y},
            {"/", (x, y) => x / y},
            {"=", (x, y) => x.Value = y.Value },
            {"==", (x, y) => x == y },
            {"!=", (x, y) => x != y }
        };

        public static Func<BaseType, BaseType, BaseType> FunctionFor(string functionName)
        {
            if (!FunctionLookup.ContainsKey(functionName)) throw new ArgumentException($"Function { functionName} was not recognised.");
            return FunctionLookup[functionName];
        }

        public static bool IsKnown(string potentialSymbol)
        {
            return FunctionLookup.ContainsKey(potentialSymbol.ToLower());
        }
    }
}
