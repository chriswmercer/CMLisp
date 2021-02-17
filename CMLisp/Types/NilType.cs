using System;
namespace CMLisp.Types
{
    public class NilType : DynamicType<object>
    {
        public NilType() : base("Nil")
        {
            Type = LanguageTypes.Nil;
        }
    }
}
