using System;
namespace CMLisp.Types
{
    public abstract class BaseType
    {
        public abstract dynamic Value { get; set; }
        public Types Type { get; protected set; }
    }
}
