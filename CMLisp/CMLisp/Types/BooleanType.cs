using System;
using System.Collections.Generic;
using CMLisp.Language;

namespace CMLisp.Types
{
    public class BooleanType : DynamicType<bool>
    {
        public BooleanType(bool val) : base(val)
        {
            Type = LanguageTypes.Boolean;
        }

        public static implicit operator DecimalType(BooleanType x)
        {
            return new DecimalType((bool)x.Value ? 1 : 0);
        }

        public static implicit operator IntegerType(BooleanType x)
        {
            return new IntegerType((bool)x.Value ? 1 : 0);
        }

        public static implicit operator ListContainer(BooleanType x)
        {
            return new ListContainer(new List<BaseType>
            {
                x
            });
        }

        public static implicit operator StringType(BooleanType x)
        {
            return new StringType((bool)x.Value ? "true" : "false");
        }

        public static implicit operator ArrayType(BooleanType x)
        {
            return new ArrayType(new List<BaseType>
            {
                x
            });
        }
    }
}
