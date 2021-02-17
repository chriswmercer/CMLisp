using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CMLisp.Language;

namespace CMLisp.Core
{
    internal class Reader
    {
        internal List<Token> Tokens;
        private int position;
        public bool EndOfFile;

        public Reader(List<Token> input)
        {
            Tokens = input;
            position = 0;
            EndOfFile = false;
        }

        public Token Next()
        {
            if (EndOfFile) throw new InvalidOperationException("Reader is at end of file");

            Token value = Tokens[position];
            position++;

            if(position >= Tokens.Count)
            {
                EndOfFile = true;
            }

            return value;
        }

        public Token Peak()
        {
            if (EndOfFile) throw new InvalidOperationException("Reader is at end of file");
            Token value = Tokens[position];
            return value;
        }
    }
}
