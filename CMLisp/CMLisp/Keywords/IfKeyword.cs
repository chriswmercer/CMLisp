﻿using System;
using CMLisp.Core;
using CMLisp.Exceptions;
using CMLisp.Language;
using CMLisp.Types;

namespace CMLisp.Keywords
{
    public class IfKeyword
    {
        private ListContainer Condition;
        private ListContainer CaseIfTrue;
        private ListContainer CaseIfFalse;
        private bool hasFalseCase = false;

        public BaseType Evaluate(BaseType[] input)
        {
            try
            {
                Condition = input[0] as ListContainer;
                CaseIfTrue = input[1] as ListContainer;

                if(input.Length > 2)
                {
                    CaseIfFalse = input[2] as ListContainer;
                    hasFalseCase = true;
                }
                else
                {
                    hasFalseCase = false;
                }

                BooleanType result = Evaluator.Evaluate(input[0]) as BooleanType;

                if((bool)result.Value == true)
                {
                    return Evaluator.Evaluate(CaseIfTrue);
                }
                else if(hasFalseCase)
                {
                    return Evaluator.Evaluate(CaseIfFalse);
                }

                return new NilType();
            }
            catch
            {
                throw new SyntaxException("The 'if' keyword requires a list to evalue a condition, a list to evaluate if true and an optional list to evaluate if false");
            }
        }
    }
}
