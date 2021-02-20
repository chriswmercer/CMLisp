using System;
using CMLisp.Types;

namespace CMLisp.Operations
{
    public static class Subtraction
    {
        internal static BaseType MinusSumOf(BaseType[] items)
        {
            dynamic value = null;

            foreach (var item in items)
            {
                if (value == null)
                {
                    value = item.Value;
                }
                else
                {
                    value -= item.Value;
                }
            }

            return BaseType.GeneratorFor(items[0].Type, value);
        }
    }
}
