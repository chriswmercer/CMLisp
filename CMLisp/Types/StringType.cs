using CMLisp.Language;

namespace CMLisp.Types
{
    public class StringType : BaseType
    {
        private string internalValue;

        public StringType(string val)
        {
            internalValue = Placeholders.ReplacePlaceholders(val);
            Type = LanguageTypes.String;
        }

        public override dynamic Value
        {
            get => internalValue;
            set
            {
                internalValue = Placeholders.ReplacePlaceholders((string)value);
            }
        }
    }
}
