using System;
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
    }
}
