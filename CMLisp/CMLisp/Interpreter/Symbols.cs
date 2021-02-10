using System;
using System.Collections.Generic;
using CMLisp.Types;

namespace CMLisp
{
    public static class Symbols
    {
        private static Dictionary<string, Func<BaseType, BaseType, BaseType>> FunctionLookup = new Dictionary<string, Func<BaseType, BaseType, BaseType>>()
        {
            {"+", (x, y) => Addition(x, y)},
            {"-", (x, y) => Addition(x, y)},
            {"*", (x, y) => Addition(x, y)},
            {"/", (x, y) => Addition(x, y)},
        };

        static Symbols()
        {
        }

        public static Func<BaseType, BaseType, BaseType> FunctionFor(string functionName)
        {
            if (!FunctionLookup.ContainsKey(functionName)) throw new ArgumentException($"Function { functionName} was not recognised.");
            return FunctionLookup[functionName];
        }

        public static bool IsKnown(string potentialSymbol)
        {
            return FunctionLookup.ContainsKey(potentialSymbol.ToLower());
        }

        internal static BaseType Addition(BaseType x, BaseType y)
        {
            if (x.Type == Types.LanguageTypes.Decimal || y.Type == Types.LanguageTypes.Decimal)
            {
                CheckNumbers(x, y);

                decimal result = (decimal)x.Value + (decimal)y.Value;
                return new DecimalType(result);
            }

            if (x.Type == Types.LanguageTypes.Integer || y.Type == Types.LanguageTypes.Integer)
            {
                CheckNumbers(x, y);

                int result = (int)x.Value + (int)y.Value;
                return new IntegerType(result);
            }

            if (x.Type == Types.LanguageTypes.List || y.Type == Types.LanguageTypes.List)
            {
                if(x.Type == Types.LanguageTypes.List && y.Type == Types.LanguageTypes.List)
                {
                    List<BaseType> bx = (List<BaseType>)x.Value;
                    List<BaseType> by = (List<BaseType>)y.Value;
                    bx.AddRange(by);
                    return new ListType(bx);
                }

                if(x.Type != Types.LanguageTypes.List)
                {
                    List<BaseType> bx = new List<BaseType>();
                    bx.Add(x);
                    List<BaseType> by = (List<BaseType>)y.Value;
                    bx.AddRange(by);
                    return new ListType(bx);
                }

                if(y.Type != Types.LanguageTypes.List)
                {
                    List<BaseType> bx = (List<BaseType>)x.Value;
                    List<BaseType> by = new List<BaseType>();
                    by.Add(y);
                    bx.AddRange(by);
                    return new ListType(bx);
                }
            }

            if(x.Type == Types.LanguageTypes.Boolean && y.Type == Types.LanguageTypes.Boolean)
            {
                return new BooleanType((bool)x.Value && (bool)y.Value);
            }

            return new StringType((string)x.Value + (string)y.Value);
        }

        private static void CheckNumbers(BaseType x, BaseType y)
        {
            if (x.Type == Types.LanguageTypes.String ||
                x.Type == Types.LanguageTypes.Boolean ||
                x.Type == Types.LanguageTypes.List ||
                y.Type == Types.LanguageTypes.String ||
                y.Type == Types.LanguageTypes.Boolean ||
                y.Type == Types.LanguageTypes.List)
            {
                throw new ArgumentException("Cannot add decimal to non-numeric types");
            }
        }
    }
}
