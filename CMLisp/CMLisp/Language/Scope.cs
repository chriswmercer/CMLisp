using System;
using System.Collections.Generic;
using System.Linq;
using CMLisp.Exceptions;
using CMLisp.Types;

namespace CMLisp.Language
{
    public class Scope
    {
        private List<ScopeElement> Items;

        public Scope()
        {
            Items = new List<ScopeElement>();
        }

        public void Add(ScopeElement element)
        {
            if(Items.Any(item => item.Label == element.Label))
            {
                throw new ScopeException($"The identifier { element.Label } already exists");
            }

            Items.Add(element);
        }

        public BaseType Get(string identifier)
        {
            if(Items.Any(item => item.Label == identifier))
            {
                return Items.First(item => item.Label == identifier).Value;
            }
            else
            {
                throw new ScopeException($"The identifier { identifier } does not exist in scope");
            }
        }
    }
}
