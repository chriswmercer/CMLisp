using System;
using CMLisp.Types;

namespace CMLisp.Operations
{
    public static class Multiplication
    {
        internal static BaseType ProductOf(BaseType[] items)
        {
            dynamic value = null;

            foreach (var item in items)
            {
                if (value == null)
                {
                    value = BaseType.GeneratorFor(item.Type, item.Value);
                }
                else
                {
                    value = Times(value, BaseType.GeneratorFor(item.Type, item.Value));
                }
            }

            return BaseType.GeneratorFor(value.Type, value.Value);
        }

        private static StringType Times(StringType x, IntegerType y)
        {
            string returnValue = string.Empty;

            for(int i = 1; i <= (int)y.Value; i++)
            {
                returnValue += x.Value;
            }
            return BaseType.GeneratorFor(x.Type, returnValue) as StringType;
        }

        private static DecimalType Times(DecimalType x, DecimalType y)
        {
            return BaseType.GeneratorFor(LanguageTypes.Decimal, (Decimal)x.Value * (Decimal)y.Value) as DecimalType;
        }

        private static DecimalType Times(DecimalType x, IntegerType y)
        {
            return BaseType.GeneratorFor(LanguageTypes.Decimal, (Decimal)x.Value * Convert.ToDecimal(y.Value)) as DecimalType;
        }

        private static BaseType Times(BaseType x, BaseType y)
        {
            return BaseType.GeneratorFor(x.Type, x.Value * y.Value);
        }
    }
}
