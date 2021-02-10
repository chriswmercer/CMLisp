using System;
using System.Collections.Generic;

namespace CMLisp.Types
{
    public class VectorType : ListType
    {
        public VectorType(List<BaseType> val) : base(val)
        {
            Type = LanguageTypes.Vector;
        }
    }
}
