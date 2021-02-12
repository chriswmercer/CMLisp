using System;
using System.Collections.Generic;
using CMLisp.Language;

namespace CMLisp.Types
{
    public class DecimalType : DynamicType<Decimal>
    {
        public DecimalType(decimal val) : base(val)
        {
            Type = LanguageTypes.Decimal;
        }

        public static implicit operator BooleanType(DecimalType x)
        {
            return new BooleanType((decimal)x.Value > 0 ? true : false);
        }

        public static implicit operator IntegerType(DecimalType x)
        {
            return new IntegerType((int)x.Value);
        }

        public static implicit operator ListContainer(DecimalType x)
        {
            return new ListContainer(new List<BaseType>
            {
                x
            });
        }

        public static implicit operator StringType(DecimalType x)
        {
            return new StringType(((decimal)x.Value).ToString());
        }

        public static implicit operator VectorType(DecimalType x)
        {
            return new VectorType(new List<BaseType>
            {
                x
            });
        }
    }
}
