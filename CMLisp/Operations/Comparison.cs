using System;
using CMLisp.Exceptions;
using CMLisp.Types;

namespace CMLisp.Operations
{
    public static class Comparison
    {
        public static BooleanType GreaterThan(BaseType[] items)
        {
            if(CheckItems(items, out BaseType x, out BaseType y))
            {
                return new BooleanType(x.Value > y.Value);
            }
            else
            {
                throw new LanguageException("It is only possible to compare decimal and integer types");
            }
        }

        public static BooleanType LessThan(BaseType[] items)
        {
            if (CheckItems(items, out BaseType x, out BaseType y))
            {
                return new BooleanType(x.Value < y.Value);
            }
            else
            {
                throw new LanguageException("It is only possible to compare decimal and integer types");
            }
        }

        public static BooleanType GreaterThanOrEqualTo(BaseType[] items)
        {
            if (CheckItems(items, out BaseType x, out BaseType y))
            {
                return new BooleanType(x.Value >= y.Value);
            }
            else
            {
                throw new LanguageException("It is only possible to compare decimal and integer types");
            }
        }

        public static BooleanType LessThanOrEqualTo(BaseType[] items)
        {
            if (CheckItems(items, out BaseType x, out BaseType y))
            {
                return new BooleanType(x.Value <= y.Value);
            }
            else
            {
                throw new LanguageException("It is only possible to compare decimal and integer types");
            }
        }

        private static bool CheckItems(BaseType[] items, out BaseType x, out BaseType y)
        {
            if (items.Length != 2) throw new LanguageException("The greater than symbol can only be used with 2 operands");

            x = items[0];
            y = items[1];


            if (x.Type == LanguageTypes.DateTime || y.Type == LanguageTypes.DateTime)
            {

                return (x.Type == LanguageTypes.DateTime && y.Type == LanguageTypes.DateTime);
            }
            else if ((x.Type != LanguageTypes.Decimal && x.Type != LanguageTypes.Integer) || (y.Type != LanguageTypes.Decimal && y.Type != LanguageTypes.Integer))
            {
                return false;
            }

            return true;
        }
    }
}
