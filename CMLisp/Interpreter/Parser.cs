using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CMLisp.Language;
using CMLisp.Types;

namespace CMLisp.Core
{
    internal static class Parser
    {
        private const string tokenPattern = "[\\s,]*(~@|[\\[\\]{}()'`~^@]|\"(?:\\.|[^\\\"])*\" ?|;.*|[^\\s\\[\\]{}'\"`,;)]*)";

        public static BaseType ReadString(string input)
        {
            var tokens = Tokenize(input);
            var reader = new Reader(tokens);
            var parsedList = ReadForm(reader);

            return parsedList;
        }

        public static BaseType ReadForm(Reader reader)
        {
            BaseType returnValue;

            var firstToken = reader.Next();
            var firstCharacter = firstToken.Value.Substring(0, 1);

            if (firstCharacter == OpeningCharacterFor(LanguageTypes.List))
            {
                returnValue = ReadList(reader, LanguageTypes.List);
            }
            else if (firstCharacter == OpeningCharacterFor(LanguageTypes.Array))
            {
                returnValue = ReadList(reader, LanguageTypes.Array);
            }
            else if (firstCharacter == OpeningCharacterFor(LanguageTypes.Object))
            {
                returnValue = ReadList(reader, LanguageTypes.Object);
            }
            else
            {
                returnValue = ReadAtom(firstToken);
            }

            return returnValue;
        }

        public static ListContainer ReadList(Reader reader, LanguageTypes type)
        {
            var tokens = new List<BaseType>();

            while (!reader.EndOfFile)
            {
                var form = ReadForm(reader);

                //check for fragments
                string tokenString = form.Value.ToString();

                if(tokenString.StartsWith("<") && tokenString.EndsWith(">"))
                {
                    var newForm = BuildFragment(reader, form, tokenString);

                    if(newForm != null)
                    {
                        tokens.Add(newForm);
                        continue;
                    }
                }

                if (form.Type == LanguageTypes.String && form.Value == ClosingCharacterFor(type))
                {
                    return GenerateFor(type, tokens);
                }

                tokens.Add(form);
            }

            var lastToken = tokens[tokens.Count - 1];

            if (lastToken.Type != LanguageTypes.String) throw new ArgumentException("List was not closed");

            string value = lastToken.Value.ToString();
            if (value.Substring(value.Length - 1, 1) != ClosingCharacterFor(type)) throw new ArgumentException("List was not closed");

            tokens.RemoveAt(tokens.Count - 1);
            return new ListContainer(tokens);
        }

        public static BaseType ReadAtom(Token token)
        {
            if (Int32.TryParse(token.Value, out int result))
            {
                return new IntegerType(result);
            }
            if (Decimal.TryParse(token.Value, out decimal descresult))
            {
                return new DecimalType(descresult);
            }

            var stringType = new StringType(token.Value);

            if (stringType.Value.ToLower() == "true" || stringType.Value.ToLower() == "false")
            {
                return new BooleanType(stringType.Value.ToLower() == "true");
            }

            if(ReservedWords.IsKnown(stringType.Value))
            {
                return new ReservedWordType(stringType.Value);
            }

            if (Symbols.IsKnown(stringType.Value))
            {
                return new SymbolType(stringType.Value);
            }

            if (Language.Keywords.IsKnown(stringType.Value))
            {
                return new KeywordType(stringType.Value);
            }

            if(stringType.Value.ToLower() == "nil")
            {
                return new NilType();
            }

            var value = stringType.Value as string;

            if (value == null) throw new ArgumentException($"Couldn't parse value { stringType.Value }");
            value = value.Trim();

            if (value.Length == 1)
            {
                if (GetKnownDelimiters().Contains(value))
                {
                    return stringType;
                }
            }

            //is it a fragment?
            if (value.Length > 2)
            {
                var startFrag = (value.Substring(0, 2));
                var endFrag = (value.Substring(value.Length - 2, 2));
                if (startFrag == "\"<" && endFrag == ">\"")
                {
                    value = value.Replace("\"", "");
                    return new FragmentType(value);
                }
            }

            //Is it a string?
            var start = (value.Substring(0, 1));
            var end = (value.Substring(value.Length - 1, 1));
            if (start == "\"")
            {
                //is it correct delimited?
                if(end != "\"") throw new ArgumentException($"String {value} is not correctly delimited");

                value = value.Replace("\"", "");
                stringType.Value = value;
                return stringType;
            }

            //if not it must be an identifier
            return new IdentifierType(stringType.Value);
        }

        private static List<Token> Tokenize(string input)
        {
            string[] raw = Regex.Split(input, tokenPattern);
            List<Token> builtTokens = new List<Token>();

            foreach (string token in raw)
            {
                if(token == "" || token.StartsWith(";")) continue;
                builtTokens.Add(new Token(token));
            }

            return builtTokens;
        }

        private static FragmentType BuildFragment(Reader reader, BaseType form, string tokenString)
        {
            string tag = tokenString.Replace("<", "").Replace(">", "");
            int nestCount = 0;

            while (!reader.EndOfFile)
            {
                var htmlForm = ReadForm(reader);
                string htmlString = htmlForm.Value.ToString();

                if (htmlString == $"<{tag}>") nestCount++;
                if (htmlString == $"</{tag}>") nestCount--;

                tokenString += $"{htmlString} ";

                if (nestCount == -1)
                {
                    tokenString = tokenString.Replace("> <", "><").Replace("> ", ">");
                    form = new FragmentType(tokenString);
                    break;
                }
            }

            return form as FragmentType;
        }

        private static string OpeningCharacterFor(LanguageTypes type)
        {
            switch(type)
            {
                case LanguageTypes.List: return "(";
                case LanguageTypes.Array: return "[";
                case LanguageTypes.Object: return "{";
                default: throw new ArgumentException($"{type} is not a valid type with a required opening character");
            }
        }

        private static string ClosingCharacterFor(LanguageTypes type)
        {
            switch (type)
            {
                case LanguageTypes.List: return ")";
                case LanguageTypes.Array: return "]";
                case LanguageTypes.Object: return "}";
                default: throw new ArgumentException($"{type} is not a valid type with a required closing character");
            }
        }

        private static ListContainer GenerateFor(LanguageTypes type, List<BaseType> tokens)
        {
            switch (type)
            {
                case LanguageTypes.List: return new ListContainer(tokens);
                case LanguageTypes.Array: return new ArrayType(tokens);
                case LanguageTypes.Object: return new ObjectType(ArrayGenerator(tokens));
                default: throw new ArgumentException($"{type} is not a valid list/array/object type");
            }
        }

        private static BaseType GenerateFor(LanguageTypes type, string tokenString)
        {
            switch(type)
            {
                case LanguageTypes.Fragment: return new FragmentType(tokenString);
                default: throw new ArgumentException($"{type} is not a valid type");
            }
        }

        private static List<BaseType> ArrayGenerator(List<BaseType> tokens)
        {
            List<BaseType> returnList = new List<BaseType>();

            if (tokens.Count % 3 != 0) throw new ArgumentException("The hashmap was invalid. Format example: { identifier : <basetype> , identifier2 : <basetype> }");

            tokens.RemoveAll(x => x.Value == ":");

            using (var iterator = tokens.GetEnumerator())
            {
                while (iterator.MoveNext())
                {
                    var first = iterator.Current;
                    var second = iterator.MoveNext() ? iterator.Current : throw new Exception("Internal error: Hashmap invalid after check");

                    returnList.Add(new KeyValuePairType(new KeyValuePair<IdentifierType, BaseType>(first as IdentifierType, second)));
                }
            }

            return returnList;
        }

        private static List<string> GetKnownDelimiters()
        {
            return new List<string>
            {
                "(",
                ")",
                "[",
                "]",
                "{",
                "}",
                ":"
            };
        }
    }
}
