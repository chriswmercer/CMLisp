using System;

namespace CMLisp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("CMLisp - type 'exit' to exit;");

            REPL.REPL.Run();
        }
    }
}
