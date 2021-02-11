using System;
using System.Collections.Generic;
using CMLisp.Types;

namespace CMLisp.Language
{
    internal static class Keywords
    {
        private static Dictionary<string, Func<BaseType, BaseType, BaseType>> FunctionLookup = new Dictionary<string, Func<BaseType, BaseType, BaseType>>()
        {
            {"+", (x, y) => x.Value + y.Value }
        };

        public static Func<BaseType, BaseType, BaseType> FunctionFor(string functionName)
        {
            if (!FunctionLookup.ContainsKey(functionName)) throw new ArgumentException($"Function { functionName } was not recognised.");
            return FunctionLookup[functionName];
        }

        public static bool IsKnown(string potentialKeyword)
        {
            return FunctionLookup.ContainsKey(potentialKeyword.ToLower());
        }
    }
}
