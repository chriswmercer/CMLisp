using System;

namespace CMLisp.Types
{
    public class KeywordType : StringType
    {
        public KeywordType(string val) : base(val)
        {
            Type = LanguageTypes.Keyword;
        }
    }
}
