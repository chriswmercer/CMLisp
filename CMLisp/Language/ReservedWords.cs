using System.Collections.Generic;

namespace CMLisp.Language
{
    public class ReservedWords
    {
        private static List<string> Words = new List<string>
        {
            "is",
            "with",
            "expect"
        };

        public static bool IsKnown(string potentialWord)
        {
            return Words.Contains(potentialWord);
        }
    }
}
