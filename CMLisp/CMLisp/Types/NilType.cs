using System;
namespace CMLisp.Types
{
    public class NilType : DynamicType<object>
    {
        public NilType() : base(new object())
        {
            Type = LanguageTypes.Nil;
        }
    }
}
