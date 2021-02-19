using System;
using System.Collections.Generic;
using CMLisp.Types;

namespace CMLisp.Operations
{
    public static class Addition
    {
        internal static BaseType SumOf(BaseType[] items)
        {
            dynamic value = null;

            foreach (dynamic item in items)
            {
                if (value == null)
                {
                    value = item;
                }
                else
                {
                    value = Add(value, item);
                }
            }

            return BaseType.GeneratorFor(value.Type, value.Value);
        }

        private static IntegerType Add(IntegerType x, IntegerType y)
        {
            return BaseType.GeneratorFor(x.Type, (int)x.Value + (int)y.Value) as IntegerType;
        }

        private static StringType Add(StringType x, StringType y)
        {
            return BaseType.GeneratorFor(LanguageTypes.String, string.Concat(x.Value, y.Value));
        }

        private static DecimalType Add(DecimalType x, DecimalType y)
        {
            return BaseType.GeneratorFor(LanguageTypes.Decimal, (Decimal)x.Value + (Decimal)y.Value) as DecimalType;
        }

        private static ArrayType Add(ArrayType x, ArrayType y)
        {
            List<BaseType> newList = (List<BaseType>)x.Value;
            newList.AddRange((List<BaseType>)y.Value);
            return BaseType.GeneratorFor(LanguageTypes.Array, newList) as ArrayType;
        }

        private static ArrayType Add(ArrayType x, BaseType y)
        {
            List<BaseType> newList = (List<BaseType>)x.Value;
            newList.Add(y);
            return BaseType.GeneratorFor(LanguageTypes.Array, newList) as ArrayType;
        }

        private static FragmentType Add(FragmentType x, FragmentType y)
        {
            return BaseType.GeneratorFor(LanguageTypes.Fragment, string.Concat(x.Value, y.Value)) as FragmentType;
        }

        private static ObjectType Add(ObjectType x, KeyValuePairType y)
        {
            List<BaseType> newList = (List<BaseType>)x.Value;
            newList.Add(y);
            return BaseType.GeneratorFor(LanguageTypes.Object, newList) as ObjectType;
        }
    }
}
