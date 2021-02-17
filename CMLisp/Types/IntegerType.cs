﻿using System;
using System.Collections.Generic;
using CMLisp.Language;

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

        public static implicit operator ListContainer(IntegerType x)
        {
            return new ListContainer(new List<BaseType>
            {
                x
            });
        }

        public static implicit operator StringType(IntegerType x)
        {
            return new StringType(((int)x.Value).ToString());
        }

        public static implicit operator ArrayType(IntegerType x)
        {
            return new ArrayType(new List<BaseType>
            {
                x
            });
        }
    }
}
