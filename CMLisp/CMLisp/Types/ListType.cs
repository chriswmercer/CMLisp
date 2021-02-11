using System;
using System.Collections.Generic;

namespace CMLisp.Types
{
    public class ListType : DynamicType<List<BaseType>>
    {
        public new List<BaseType> Value
        {
            get => internalValue;
            set
            {
                internalValue = value;
            }
        }

        public ListType(List<BaseType> val) : base(val)
        {
            Type = LanguageTypes.List;
        }
    }
}
