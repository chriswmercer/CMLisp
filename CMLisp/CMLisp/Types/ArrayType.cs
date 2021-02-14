using System;
using System.Collections.Generic;
using CMLisp.Language;

namespace CMLisp.Types
{
    public class ArrayType : ListContainer
    {
        public ArrayType(List<BaseType> val) : base(val)
        {
            Type = LanguageTypes.Array;
        }
    }
}
