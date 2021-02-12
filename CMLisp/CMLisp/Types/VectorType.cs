using System;
using System.Collections.Generic;
using CMLisp.Language;

namespace CMLisp.Types
{
    public class VectorType : ListContainer
    {
        public VectorType(List<BaseType> val) : base(val)
        {
            Type = LanguageTypes.Vector;
        }
    }
}
