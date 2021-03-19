using System;
using System.Collections.Generic;
using System.Linq;
using CMLisp.Core;
using CMLisp.Exceptions;
using CMLisp.Language;
using CMLisp.Types;

namespace CMLisp.Keywords
{
    public class LambdaKeyword
    {
        private const string Error = "The Lambda needs 3 paramters: Array to operate over, Identifer to represent each element, List to apply to each element.";

        public BaseType Evaluate(BaseType[] input)
        {
            if (input.Length != 3 &&
                (input[0].Type != LanguageTypes.String || input[0].Type != LanguageTypes.Identifier) &&
                (input[1].Type != LanguageTypes.Identifier) &&
                (input[2].Type != LanguageTypes.List))
            {
                throw new LanguageException(Error);
            }

            try
            {
                var arr = Explode(input[0], LanguageTypes.Array) as ArrayType;
                var id = input[1] as IdentifierType;
                var operation = input[2] as ListContainer;

                var returnArr = new ArrayType(new List<BaseType>());

                foreach(var item in arr)
                {
                    var btItem = item as BaseType;
                    var tempLocalScope = new Scope();
                    var element = new ScopeElement(id, btItem);
                    var thisOperation = new ListContainer(operation.Value);

                    tempLocalScope.Add(element);
                    var result = Evaluator.Evaluate(thisOperation, tempLocalScope);
                    returnArr.Value.Add(result);
                }

                return returnArr;

            }
            catch (Exception exc)
            {
                throw new LanguageException(Error, exc);
            }

        }

        private BaseType Explode(BaseType input, LanguageTypes desiredOutput)
        {
            var value = input;

            int tries = 0;
            int maxTries = 10;

            while (value.Type == LanguageTypes.Identifier && value.Type != desiredOutput)
            {
                value = Evaluator.Evaluate(value, Evaluator.LocalScope);

                if (++tries > maxTries) break;
            }

            return BaseType.GeneratorFor(desiredOutput, value.Value);
        }
    }
}
