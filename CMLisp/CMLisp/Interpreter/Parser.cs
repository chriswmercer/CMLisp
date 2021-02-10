using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CMLisp.Types;

namespace CMLisp.Interpreter
{
    public static class Parser
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
            else if (firstCharacter == OpeningCharacterFor(LanguageTypes.Vector))
            {
                returnValue = ReadList(reader, LanguageTypes.Vector);
            }
            else
            {
                returnValue = ReadAtom(firstToken);
            }

            return returnValue;
        }

        public static ListType ReadList(Reader reader, LanguageTypes type)
        {
            var tokens = new List<BaseType>();

            while (!reader.EndOfFile)
            {
                var form = ReadForm(reader);

                if(form.Type == Types.LanguageTypes.String && form.Value == ClosingCharacterFor(type))
                {
                    return GenerateFor(type, tokens);
                }

                tokens.Add(form);
            }

            var lastToken = tokens[tokens.Count - 1];

            if (lastToken.Type != Types.LanguageTypes.String) throw new ArgumentException("List was not closed");

            string value = lastToken.Value.ToString();
            if (value.Substring(value.Length - 1, 1) != ClosingCharacterFor(type)) throw new ArgumentException("List was not closed");

            tokens.RemoveAt(tokens.Count - 1);
            return new ListType(tokens);
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

            if (Symbols.IsKnown(stringType.Value))
            {
                return new SymbolType(stringType.Value);
            }

            if (Keywords.IsKnown(stringType.Value))
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

            //This atom isn't special, therefore check its a valid string
            var start = (value.Substring(0, 1));
            var end = (value.Substring(value.Length - 1, 1));
            if (start != "\"" || end != "\"") throw new ArgumentException($"String {value} is not correctly delimited");
            value = value.Replace("\"", "");
            stringType.Value = value;

            return stringType;
        }

        private static List<Token> Tokenize(string input)
        {
            string[] raw = Regex.Split(input, tokenPattern);
            List<Token> builtTokens = new List<Token>();

            foreach (string token in raw)
            {
                if(token == "") continue;
                builtTokens.Add(new Token(token));
            }

            return builtTokens;
        }

        private static string OpeningCharacterFor(LanguageTypes type)
        {
            switch(type)
            {
                case LanguageTypes.List: return "(";
                case LanguageTypes.Vector: return "[";
                default: throw new ArgumentException($"{type} is not a valid type with a required opening character");
            }
        }

        private static string ClosingCharacterFor(LanguageTypes type)
        {
            switch (type)
            {
                case LanguageTypes.List: return ")";
                case LanguageTypes.Vector: return "]";
                default: throw new ArgumentException($"{type} is not a valid type with a required closing character");
            }
        }

        private static ListType GenerateFor(LanguageTypes type, List<BaseType> tokens)
        {
            switch (type)
            {
                case LanguageTypes.List: return new ListType(tokens);
                case LanguageTypes.Vector: return new VectorType(tokens);
                default: throw new ArgumentException($"{type} is not a valid list/vector type");
            }
        }

        private static List<string> GetKnownDelimiters()
        {
            return new List<string>
            {
                "(",
                ")",
                "[",
                "]"
            };
        }
    }
}
