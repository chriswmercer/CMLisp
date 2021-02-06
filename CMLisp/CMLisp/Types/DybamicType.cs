using System;
using System.Collections.Generic;

namespace CMLisp.Types
{
    public class DynamicType<T> : BaseType where T: new()
    {
        private T internalValue;

        public DynamicType(T val)
        {
            internalValue = val;
            Type = Types.Dynamic;
        }

        public override object Value
        {
            get => internalValue;
            set
            {
                internalValue = (T)value;
            }
        }
    }

    public class StringType : BaseType
    {
        private string internalValue;

        public StringType(string val)
        {
            internalValue = val;
            Type = Types.String;
        }

        public override dynamic Value
        {
            get => internalValue;
            set
            {
                internalValue = (string)value;
            }
        }
    }

    public class IntegerType : DynamicType<int>
    {
        public IntegerType(int val) : base(val)
        {
            Type = Types.Integer;
        }
    }

    public class BooleanType : DynamicType<bool>
    {
        public BooleanType(bool val) : base(val)
        {
            Type = Types.Boolean;
        }
    }

    public class DecimalType : DynamicType<Decimal>
    {
        public DecimalType(Decimal val) : base(val)
        {
            Type = Types.Decimal;
        }
    }

    public class SymbolType : StringType
    {
        public SymbolType(string val) : base(val)
        {
            Type = Types.Symbol;
        }
    }

    public class ListType : DynamicType<List<BaseType>>
    {
        public ListType(List<BaseType> val) : base(val)
        {
            Type = Types.List;
        }
    }

    public enum Types
    {
        String,
        Integer,
        Decimal,
        Boolean,
        Dynamic,
        Symbol,
        List
    }
}
