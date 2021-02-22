using System;
using System.Collections.Generic;
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
            {"!=", (x) => new BooleanType(!(Equality.EqualityOf(x).Value))},
            {"and", (x) => BooleanLogic.And(x) },
            {"or", (x) => BooleanLogic.Or(x) },
            {"xor", (x) => BooleanLogic.Xor(x) },
            {"not", (x) => BooleanLogic.Not(x) },
            {">", (x) => Comparison.GreaterThan(x) },
            {"<", (x) => Comparison.LessThan(x) },
            {">=", (x) => Comparison.GreaterThanOrEqualTo(x) },
            {"<=", (x) => Comparison.LessThanOrEqualTo(x) }
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
