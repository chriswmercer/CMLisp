namespace CMLisp.Types
{
    public class FragmentType : StringType
    {
        public FragmentType(string val) : base(val)
        {
            Type = LanguageTypes.Fragment;
        }
    }
}
