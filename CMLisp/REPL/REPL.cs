using System;
using System.IO;
using CMLisp.Core;

namespace CMLisp.REPL
{
    public static class REPL
    {
        public static void Run()
        {
            var input = "";

            while(input.ToLower() != "exit")
            {
                Console.Write("\ncmlisp> ");
                input = Console.ReadLine();

                if(input.ToLower().StartsWith("run"))
                {
                    var parsedCommand = input.Split("\"");

                    if (parsedCommand.Length < 2)
                    {
                        Console.WriteLine("Usage: run \"c:\\full\\path\\to\\filename.txt\"");
                    }
                    else
                    {
                        var filename = parsedCommand[1];

                        if (!File.Exists(filename))
                        {
                            Console.Write($"Filename \"{ filename }\" was not found, or you do not have permissions to read it.");
                        }
                        else
                        {
                            string file = "";
                            try
                            {
                                file = File.ReadAllText(filename);
                            }
                            catch (Exception exc)
                            {
                                throw new FileNotFoundException($"Could not load the file { filename }. See inner exception for more details.", exc);
                            }

                            var output = Interpreter.Interpret(file);
                            Console.WriteLine(output);
                        }
                    }
                }
                else if(input.ToLower() == "exit")
                {
                    Console.WriteLine("Goodbye!");
                }
                else
                {
                    var output = Interpreter.Interpret(input);
                    Console.WriteLine(output);
                }
            }
        }
    }
}
