using System;
using System.Collections.Generic;
using CMLisp.Types;

namespace CMLisp.Language
{
    public class ListContainer : DynamicType<List<BaseType>>
    {
        public new List<BaseType> Value
        {
            get => internalValue;
            set
            {
                internalValue = value;
            }
        }

        public ListContainer(List<BaseType> val) : base(val)
        {
            Type = LanguageTypes.List;
        }
    }
}
