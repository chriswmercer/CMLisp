namespace CMLisp.Types
{
    public class SymbolType : StringType
    {
        public SymbolType(string val) : base(val)
        {
            Type = LanguageTypes.Symbol;
        }
    }
}
