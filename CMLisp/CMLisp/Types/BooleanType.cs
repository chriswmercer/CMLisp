using System;
using System.Collections.Generic;

namespace CMLisp.Types
{
    public class BooleanType : DynamicType<bool>
    {
        public BooleanType(bool val) : base(val)
        {
            Type = LanguageTypes.Boolean;
        }

        //public static implicit operator BooleanType(DecimalType x)
        //{
        //    return new BooleanType((decimal)x.Value > 0 ? true : false);
        //}

        public static implicit operator DecimalType(BooleanType x)
        {
            return new DecimalType((bool)x.Value ? 1 : 0);
        }

        public static implicit operator IntegerType(BooleanType x)
        {
            return new IntegerType((bool)x.Value ? 1 : 0);
        }

        public static implicit operator ListType(BooleanType x)
        {
            return new ListType(new List<BaseType>
            {
                x
            });
        }

        public static implicit operator StringType(BooleanType x)
        {
            return new StringType((bool)x.Value ? "true" : "false");
        }

        public static implicit operator VectorType(BooleanType x)
        {
            return new VectorType(new List<BaseType>
            {
                x
            });
        }
    }
}
