using System;

namespace CMLisp.Types
{
    public abstract class BaseType
    {
        public abstract dynamic Value { get; set; }
        public LanguageTypes Type { get; protected set; }
    }
}
