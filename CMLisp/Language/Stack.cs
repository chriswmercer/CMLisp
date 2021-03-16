using System;
using System.Collections.Generic;

namespace CMLisp.Language
{
    public class Stack
    {
        private Stack<string> stack;

        public Stack(string initial)
        {
            stack = new Stack<string>();
            Push(initial);
        }

        public void Push(string value)
        {
            var rand = Guid.NewGuid().ToString();
            stack.Push($"{value}-{rand}");
        }

        public string Pop()
        {
            var value = stack.Pop();
            var values = value.Split("-");
            return values[0];
        }

        public string Current()
        {
            return stack.Peek();
        }
    }
}
