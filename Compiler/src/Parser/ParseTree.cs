using System.Diagnostics;
using Lexer;

namespace Parser;

/// <summary>
/// Represents a node of the AST    
/// </summary>
public class ParseTree
{
    /// <summary>
    /// Childrens of the node
    /// </summary>
    private List<ParseTree> _childrens;
    
    
    /// <summary>
    /// Symbol of the node (Terminal/NonTerminal)
    /// </summary>
    public Symbol Symbol { get; set; }

    /// <summary>
    /// An int pointer to know which childrens have already been parsed
    /// </summary>
    private int _pointer;
    
    /// <summary>
    /// The parent of the node
    /// </summary>
    public ParseTree Parent { get; }

    private bool isAxiom; 
    
    public AbstractTree Ast { get; private set; }

    public Rule Rule { get; private set; }

    /// <summary>
    /// Construct a ParseTree with a symbol, no childrens and no parent?=.
    /// </summary>
    /// <param name="symbol">Symbol of the node</param>
    public ParseTree(Symbol symbol)
    {
        Symbol = symbol;
        _childrens = new List<ParseTree>();
        _pointer = 0;
        Parent = null;
        isAxiom = true;
        Ast = new AbstractTree(this);
    }
    
    /// <summary>
    /// Construct a ParseTree with a symbol, a parent and no childrens
    /// </summary>
    /// <param name="symbol">Symbol of the node</param>
    /// <param name="parent">Parent of the node</param>
    public ParseTree(Symbol symbol, ParseTree parent) : this(symbol)
    {
        Parent = parent;
        isAxiom = false;
        Ast = new AbstractTree(this, parent.Ast);
    }

    
    /// <summary>
    /// Move into the leftest branch not already analysed to look after a Terminal containing the token
    /// </summary>
    /// <param name="token">Token research in the tree</param>
    /// <returns>The node from which to search</returns>
    /// <exception cref="Exception"></exception>
    public ParseTree Move(Token token)
    {
        this.PrintParseTree();
        
        if (_pointer >= _childrens.Count)
        {
            callSemanticFunction();
            Parent._pointer += 1;
            return Parent.Move(token);
        }
        
        if (_childrens.Count == 0)
        {
            callSemanticFunction();
            Parent._pointer += 1;
            return Parent;
        }
        
        Terminal terminal = new Terminal(token);

        ParseTree children = _childrens[_pointer];

        // If the children's symbol matchs the token
        if (children.Symbol.Equals(terminal))
        {
            children.Symbol = terminal;
            children.Ast.Label = terminal.GetLabel();
            
            _pointer += 1;
            // If the childrens of the node have been completely explored, we return the parent of the node and  
            // increment its pointer
            if (_pointer >= _childrens.Count)
            {
                callSemanticFunction();
                Parent._pointer += 1;
                return Parent;
            }
            return this;
        }
        
        // Research of the rules 
        var rules = FindRules(token);
        
        
        if (rules.Count == 0)
            throw new SyntaxicException($"Syntaxic error, reading token {token} line {token.Line}");
        if (rules.Count > 1)
            throw new Exception("Erreur non LL(1), plusieurs règles");
        
        
        Rule currentRule = rules[0];

        // We give the rules to the children so it can create its childrens
        children.SetRule(currentRule);
        // We research recursivly the terminal
        return children.Move(terminal.Token);
    }

    
    /// <summary>
    /// Create the childrens of the node from a List of symbols representing the rule
    /// </summary>
    /// <param name="rule">List of symbol representing the rule</param>
    public void SetRule(Rule rule)
    {
        foreach (Symbol symbol in rule.Rights_members)
        {
            _childrens.Add(new ParseTree(symbol, this));
        }

        Rule = rule;
    }
    
    public List<ParseTree> GetChildrens()
    {
        return _childrens;
    }

    public ParseTree GetChild(int i)
    {
        return _childrens[i-1];
    }

    public ParseTree GetParent()
    {
        return Parent;
    }

    public List<Rule> FindRules(Token t)
    {
        List<Rule> rules = new List<Rule>();
        //foreach (var rule in this.GetSymbol().GetRules())
        foreach (var rule in  CurrentChildren().GetRules())
        {
            if (rule.IsGoodRule(t))
            {
               rules.Add(rule);
            }
        }

        return rules;
    }

    public Symbol CurrentChildren()
    {
        return _childrens[_pointer].GetSymbol();
    }

    public Symbol GetSymbol()
    {
        return Symbol;
    }

    
    public override string ToString()
    {
        if (_childrens.Count == 0)
            return $"{Symbol}";
        
        string childrensStr = "";
        
        for (int i = 0; i < _childrens.Count; i++)
        {
            childrensStr += _childrens[i];
            if (i < _childrens.Count -1)
                childrensStr += ", ";
        }
        return $"{Symbol}({childrensStr})";
    }

    public int GetPointer()
    {
        return _pointer;
    }

    public void SetPointer(int p)
    {
        this._pointer = p;
    }

    public bool GetAxiom()
    {
        return isAxiom;
    }


    public void PrintParseTree()
    {
        if (_childrens.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{Symbol}" + "({");
            Console.ResetColor();
        }
        else
        {
            Console.Write($"{Symbol}" + "({");
        }
        
        
        for (int i = 0; i < _childrens.Count; i++)
        {
            if (_pointer == i)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(_childrens[i] + " ");
                Console.ResetColor();
            }
            else
                Console.Write(_childrens[i] + " ");

            if (i < _childrens.Count -1)
                Console.Write(", ");
        }
        Console.Write("})\n\n");
    }

    public void callSemanticFunction()
    {
        // Create the childrens of the ast
        Ast.SetChildrens(_childrens);
        
        
        Rule.SemanticFunction(Ast);
    }
    
}