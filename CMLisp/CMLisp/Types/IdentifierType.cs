using System;
using CMLisp.Language;

namespace CMLisp.Types
{
    public class IdentifierType : StringType
    {
        public IdentifierType(string val) : base(val)
        {
            Type = LanguageTypes.Identifier;
        }
    }
}
