using System;
using System.Collections.Generic;
using System.Linq;
using CMLisp.Types;

namespace CMLisp.Language
{
    public class Placeholders
    {
        private static Dictionary<string, string> InternedResults = new Dictionary<string, string>();

        private static Dictionary<string, Func<string>> PlaceholderLookup = new Dictionary<string, Func<string>>()
        {
            {"@now", new Placeholders().Now }
        };

        public static string PlaceholderFor(string placeholderName)
        {
            if (!PlaceholderLookup.ContainsKey(placeholderName)) return null;
            return PlaceholderLookup[placeholderName]();
        }

        public static bool IsKnown(string placeholderName)
        {
            return PlaceholderLookup.ContainsKey(placeholderName);
        }

        public static string ReplacePlaceholders(string dataIn)
        {
            if (InternedResults.ContainsKey(dataIn))
            {
                return InternedResults[dataIn];
            }

            var words = dataIn.Split(' ');

            for(int i = 0; i < words.Count(); i++)
            {
                var word = words[i];

                if(IsKnown(word.ToLower()))
                {
                    words[i] = PlaceholderFor(word);
                }
            }

            var result = string.Join(' ', words);

            if (!InternedResults.ContainsKey(dataIn))
            {
                InternedResults.Add(dataIn, result);
            }

            return result;
        }

        public string Now()
        {
            //we'll use the inbuilt tostring so it gives sensible results
            return new DateTimeType(DateTime.Now).ToString();
        }
    }
}
