using System;
using System.Collections.Generic;

namespace CMLisp.Types
{
    public class IntegerType : DynamicType<int>
    {
        public IntegerType(int val) : base(val)
        {
            Type = LanguageTypes.Integer;
        }

        public static implicit operator BooleanType(IntegerType x)
        {
            return new BooleanType((decimal)x.Value > 0 ? true : false);
        }

        public static implicit operator DecimalType(IntegerType x)
        {
            return new DecimalType((bool)x.Value ? 1 : 0);
        }

        public static implicit operator ListType(IntegerType x)
        {
            return new ListType(new List<BaseType>
            {
                x
            });
        }

        public static implicit operator StringType(IntegerType x)
        {
            return new StringType(((int)x.Value).ToString());
        }

        public static implicit operator VectorType(IntegerType x)
        {
            return new VectorType(new List<BaseType>
            {
                x
            });
        }
    }
}
