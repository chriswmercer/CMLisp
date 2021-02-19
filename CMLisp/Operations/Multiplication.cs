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
                    value = item;
                }
                else
                {
                    value = Times(value, item);
                }
            }

            return BaseType.GeneratorFor(value.Type, value.Value);
        }

        private static StringType Times(StringType x, IntegerType y)
        {
            string returnValue = x.Value;

            for(int i = 1; i <= (int)y.Value; i++)
            {
                returnValue = string.Concat(returnValue, returnValue);
            }
            return BaseType.GeneratorFor(x.Type, returnValue) as StringType;
        }

        private static BaseType Times(BaseType x, BaseType y)
        {
            return BaseType.GeneratorFor(x.Type, x.Value * y.Value);
        }
    }
}
