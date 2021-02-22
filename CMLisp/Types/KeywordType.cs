namespace CMLisp.Types
{
    public class KeywordType : StringType
    {
        public KeywordType(string val) : base(val)
        {
            Type = LanguageTypes.Keyword;
        }

        public static implicit operator string(KeywordType x)
        {
            return (string)x.Value;
        }
    }
}
