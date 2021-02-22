A version of LISP which is almost a LISP but not quite a LISP.

Basics
------

The most basic of programs:

(+ 2 3)

This evaluates to and IntegerType with a value of 5. This also works:

(2 + 3)

Which may be easier to read. There are a few basic concepts.


Types
-----

There are 3 categories of types:

- Container types, which the language uses to understand the structure of the progam
- Syntax types, which do special things and are evaluated in a non-standard manner
- Data types
 
All of these categories share the same root type - Base Type. 


Container Type
-------------

The only container type is the ListType, delimited with ( ). They contain other types of any category to be evaulated. So, the above example would read:

List type, containing 3 other types.

---

Data Types
----------

Date types understood are:

+ Array (of any type)   [1, 2, 3] Note - technically each element can be any base type, and they don't have to match. Be aware of casting failures.
+ Boolean               true, false
+ Decimal               1.31
+ Fragment              a fragment of HTML with a matching opening and closing tag
+ Integer               1
+ KeyValuePair          identifier : anydatatype - note the space before and after :
+ Nil                   Nothing, will be factored out by the evaluator
+ Object                An arrangement of KeyValuePair types, rather like JSON
+ String                "this is a string"

Coming soon: Fixing fragment type, and datetime type.

---

Syntax Types
------------

Syntax types are:

+ Identifier type - this is a placeholder type which resolves to either a user-defined function, or a variable of any data type. 
+ Keyword type - each list can contain up to 1 keyword. The evaluator will look up known keywords and, if one is found, pass the operands to the keyword to do something.
+ ReservedWord type - a reserved word is a word or something that is known to the evaluator but doesn't do anything special, and is used to make code read better.
Symbol type = each list can contain up to 1 symbol. The evaluator will look up known symbols and, if one is found, pass the operands to the symbol to do something.

Note - when looking up keywords, you do not need to type the entire keyword, but rather only enough letters so that the evaluator can work out which one you want. For example, the keyword "interpolate" could just be "inte".

Currently understood keywords are:

+ Function
Requires exactly 2 operands, the identifier for the new function, and a list containing what the function will do. The function keyword evaluates to nil.

Example:

    (function main
        (3 + 4)
    )

This registers a function called "main" in the global scope. To invoke main, you can simply do:

(main)

Which will then evaluate (3 + 4), returning 7.

+ If
Understands between 2 and 3 operands. The first operand is the list to evaluate which must return a boolean value. The second operand is a list to evaluate in the case of true. And the third optional operand is a list to evalute in the case of false. The if keyword evaluates to nil.

Example:

    (func main
        (if (true = true)
            (parameter1 * add3)
        )
    )


+ Interpolation
This is currently used to send parameters to a function. The first operand is of type object, and the second operand is an identifier for a function. There is a shortcut for this keyword which is => "goes into". The object operand and serialized into a local scope for the function. This keyword evaluates to the evaluation of the function. 

Exmaple:

    (var input is { parameter1 : "cheese", parameter2 : "potato" })

    (input => main)


+ Print and Printline
These functions can take any number of operands and print them out to screen in a friendly manner. Print doesn't add newlines at the end of each item, printline does.


+ Variable
This adds a variable to the appropriate scope i.e. if define outside of a function, to global scope, otherwise to local scope. It evaluates to nil. Example:

  (variable a is 6)


N.b. any nils coming from keyword evaluations are factored out on final evaluation. Keywords are added all the time, check the keyword folder for support keywords.


---

Symbols
-------

Symbols understood currently are:

+ \+ For numeric types, adds them together. Can operate on any number of operands. Use to concat strings and add elements to arrays.
+ \- For numeric types, subtracts them. Can operate on any number of operands.
+ \* For numeric types, multiplies them. Can operate on any number of operands. For string * integer, repeats the string that number of times.
+ \/ For numeric types, divides them. On any number of operands.
+ = or == tests equality
+ != tests inequality
+ and, or, xor, not are boolean operations

---

Scope
-----

All programs have a global scope. Functions will have an implicit local scope if parameters are passed in. When checking identifiers, local scope is checked before global scope. After a function is completely evaluated, local scope is destroyed.

---

REPL
----

There is a basic REPL where you can enter a single list to be evaluated. Or you can type "run" then the filename you want to evaluate.

---

Examples
--------

Here are some examples:

---

(
    (variable a is 6)

    (function add3
        (a + 3)
    )

    (function main
        (add3)
    )

    (main)
) 

---

; example with keyword short forms
(
    (var a is 6)

    (fun add3
        (a + 3)
    )

    (func main
        (add3)
    )

    (main)
)

---

; run "Examples/example5.txt"
(
    (var a is 6)

    (fun add3
        (a + 3)
    )

    (func main
        (parameter1 * add3)
    )

    (var input is { parameter1 : "cheese", parameter2 : "potato" })

    (input => main)
) 

---

; run "Examples/example6.txt"
(
    (var a is 6)

    (fun add3
        (a + 3)
    )

    (func main
        (if (true = true)
            (parameter1 * add3)
        )
    )

    (var input is { parameter1 : "cheese", parameter2 : "potato" })

    (input => main)
) 

---

Final Words
-----------

There is a "todo" list in Examples.
