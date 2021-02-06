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

            if (firstCharacter == "(")
            {
                returnValue = ReadList(reader);
            }
            else
            {
                returnValue = ReadAtom(firstToken);
            }

            return returnValue;
        }

        public static ListType ReadList(Reader reader)
        {
            var tokens = new List<BaseType>();

            while (!reader.EndOfFile)
            {
                var form = ReadForm(reader);

                if(form.Type == Types.Types.String && form.Value == ")")
                {
                    return new ListType(tokens);
                }

                tokens.Add(form);
            }

            var lastToken = tokens[tokens.Count - 1];

            if (lastToken.Type != Types.Types.String) throw new ArgumentException("List was not closed");

            string value = lastToken.Value.ToString();
            if (value.Substring(value.Length - 1, 1) != ")") throw new ArgumentException("List was not closed");

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
    }
}
