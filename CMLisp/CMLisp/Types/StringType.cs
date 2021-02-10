using System;

namespace CMLisp.Types
{
    public class StringType : BaseType
    {
        private string internalValue;

        public StringType(string val)
        {
            internalValue = val;
            Type = LanguageTypes.String;
        }

        public override dynamic Value
        {
            get => internalValue;
            set
            {
                internalValue = (string)value;
            }
        }
    }
}
