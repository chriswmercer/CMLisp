namespace CMLisp.Exceptions
{
    public class GuardException : LanguageException
    {
        public GuardException(string variables) : base($"Guard statement exception: one or more variables where not in scope at the time the guard was executed - { variables }")
        {
        }
    }
}
