using System;
using CMLisp.Types;

namespace CMLisp.Syntax
{
    public class Parser
    {
        public Parser()
        {
        }

        private static Expression From(SExpAtom atom)
        {
            double x;
            if (double.TryParse(atom.Value, out x))
                return new Number(x);
            if (atom.Value.ToLower() == "true")
                return new Bool(true);
            if (atom.Value.ToLower() == "false")
                return new Bool(false);
            return new Variable(atom.Value);
        }

        private static Expression From(SExpList root)
        {
            // "Require" throws an exception if the test condition is not satisfied

            var head = root.Contents.Pop();
            if (head is SExpList)
                return new Call(From(head), root.Contents.Select(x => From(x)).ToList());

            var cast = head as SExpAtom;

            switch (cast.Value)
            {
                case "+":
                    // ... similar for -, *, /, =, etc
                    return new ListFunction(cast.Value, root.Contents.Select(x => From(x)));

                case "lambda":
                    Require(root.Contents.First() is SExpList,
                        "\"lambda\" statements should be followed by list of parameters");

                    var parameters = (SExpList)root.Contents.Pop();
                    Require(parameters.Contents.All(x => x is SExpAtom),
                        "\"lambda\" statements should be followed by list of string parameters");

                    var body = From(root.Contents.Pop());
                    return new Lambda(body, parameters.Contents.Select(x => (x as SExpAtom).Value));

                case "if":
                    Require(root.Contents.Count == 3,
                        "\"if\" statements should be followed by three expressions");
                    return new If(From(root.Contents.Pop()),
                        From(root.Contents.Pop()), From(root.Contents.Pop()));

                case "define":
                    Require(root.Contents.First() is SExpAtom,
                        "\"define\" statements should be followed by string identifier");
                    var atom = (SExpAtom)root.Contents.Pop();
                    return new Define(atom.Value, From(root.Contents.Pop()));

                default:
                    return new Call(From(cast), root.Contents.Select(x => From(x)).ToList());
            }
        }
    }
}
