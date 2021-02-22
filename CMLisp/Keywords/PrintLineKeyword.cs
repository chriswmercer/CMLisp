using CMLisp.Types;

namespace CMLisp.Keywords
{
    public class PrintLineKeyword
    {
        public BaseType Evaluate(BaseType[] input)
        {
            return new PrintKeyword().Evaluate(input, true);
        }
    }
}
