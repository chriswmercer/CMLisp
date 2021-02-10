using System;
using System.Collections.Generic;

namespace CMLisp.Types
{
    public class ListType : DynamicType<List<BaseType>>
    {
        public ListType(List<BaseType> val) : base(val)
        {
            Type = LanguageTypes.List;
        }
    }
}
