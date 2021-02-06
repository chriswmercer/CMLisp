using System;
using System.Collections.Generic;
using CMLisp.Types;

namespace CMLisp
{
    public static class Symbols
    {
        public static Dictionary<string, Func<BaseType, BaseType, BaseType>> Known = new Dictionary<string, Func<BaseType, BaseType, BaseType>>()
        {
            {"+", (x, y) => Addition(x, y)},
            {"-", (x, y) => Addition(x, y)},
            {"*", (x, y) => Addition(x, y)},
            {"/", (x, y) => Addition(x, y)},
        };

        static Symbols()
        {
        }

        public static bool IsKnown(string potentialSymbol)
        {
            return Known.ContainsKey(potentialSymbol.ToLower());
        }

        internal static BaseType Addition(BaseType x, BaseType y)
        {
            if (x.Type == Types.Types.Decimal || y.Type == Types.Types.Decimal)
            {
                CheckNumbers(x, y);

                decimal result = (decimal)x.Value + (decimal)y.Value;
                return new DecimalType(result);
            }

            if (x.Type == Types.Types.Integer || y.Type == Types.Types.Integer)
            {
                CheckNumbers(x, y);

                int result = (int)x.Value + (int)y.Value;
                return new IntegerType(result);
            }

            if (x.Type == Types.Types.List || y.Type == Types.Types.List)
            {
                if(x.Type == Types.Types.List && y.Type == Types.Types.List)
                {
                    List<BaseType> bx = (List<BaseType>)x.Value;
                    List<BaseType> by = (List<BaseType>)y.Value;
                    bx.AddRange(by);
                    return new ListType(bx);
                }

                if(x.Type != Types.Types.List)
                {
                    List<BaseType> bx = new List<BaseType>();
                    bx.Add(x);
                    List<BaseType> by = (List<BaseType>)y.Value;
                    bx.AddRange(by);
                    return new ListType(bx);
                }

                if(y.Type != Types.Types.List)
                {
                    List<BaseType> bx = (List<BaseType>)x.Value;
                    List<BaseType> by = new List<BaseType>();
                    by.Add(y);
                    bx.AddRange(by);
                    return new ListType(bx);
                }
            }

            if(x.Type == Types.Types.Boolean && y.Type == Types.Types.Boolean)
            {
                return new BooleanType((bool)x.Value && (bool)y.Value);
            }

            return new StringType((string)x.Value + (string)y.Value);
        }

        private static void CheckNumbers(BaseType x, BaseType y)
        {
            if (x.Type == Types.Types.String ||
                x.Type == Types.Types.Boolean ||
                x.Type == Types.Types.List ||
                y.Type == Types.Types.String ||
                y.Type == Types.Types.Boolean ||
                y.Type == Types.Types.List)
            {
                throw new ArgumentException("Cannot add decimal to non-numeric types");
            }
        }
    }
}
