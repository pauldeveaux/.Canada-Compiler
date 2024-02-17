using System.Diagnostics;
using System.Runtime.InteropServices;
using Lexer;

namespace Parser;

public class NonTerminal : Symbol
{

    /// <summary>
    /// Label of the Non Terminal
    /// </summary>
    public string Label { get; }
    
    /// <summary>
    /// Rules of the Non Terminal
    /// </summary>
    public List<Rule> Rules { get; }
    
    
    /// <summary>
    /// First Non Terminal that can be found first in a string created by this Non Terminal.
    /// </summary>
    public List<Terminal> _firsts { get; }
    private List<Terminal> _nexts { get;  }

    
    /// <summary>
    /// Create a Non Terminal with a label
    /// </summary>
    /// <param name="label"></param>
    public NonTerminal(string label)
    {
        Label = label;
        Rules = new List<Rule>();
        _firsts = new List<Terminal>();
        _nexts = new List<Terminal>();
    }

    public List<Rule> GetRules()
    {
        return Rules;
    }

    public String GetLabel()
    {
        return Label;
    }


    /// <summary>
    /// Add a rule to the Non Terminal. Returns this Non Terminal to facilitate the syntax.
    /// By example :
    /// NonTerminal nt = new NonTerminal("label")
    ///     .AddRule(new List<Symbol/>{
    ///         ...
    ///     }
    ///     .AddRule(new List<Symbol/>{
    ///         ...
    ///     }; 
    /// </summary>
    /// <param name="lambda"></param>
    /// <param name="rightMembers"></param>
    /// <returns></returns>
    public NonTerminal AddRule(Action<AbstractTree> lambda, params Symbol[] rightMembers)
    {
        Rules.Add(new Rule( new List<Symbol>(rightMembers), Label, lambda:lambda, false));
        return this;
    }
    
    public NonTerminal AddRule(Action<AbstractTree> lambda, bool d, params Symbol[] rightMembers)
    {
        Rules.Add(new Rule( new List<Symbol>(rightMembers), Label, lambda:lambda, d));
        return this;
    }
    
    /// <summary>
    /// Add a rule to the Non Terminal. Returns this Non Terminal to facilitate the syntax.
    /// By example :
    /// NonTerminal nt = new NonTerminal("label")
    ///     .AddRule(new List<Symbol/>{
    ///         ...
    ///     }
    ///     .AddRule(new List<Symbol/>{
    ///         ...
    ///     };
    /// </summary>
    /// <param name="right_members"></param>
    /// <returns>The Non Terminal to simplify syntax</returns>
    public NonTerminal AddRule(params Symbol[] rightMembers)
    {
        return AddRule((n) => { }, rightMembers);
    }


    /// <summary>
    ///  Research the rules that must be parsed if the terminal's token has been read
    /// </summary>
    /// <param name="terminal"></param>
    /// <returns>A list of rules. There must be only one rule if the grammar is LL(1)</returns>
    public List<Rule> GetRuleWith(Terminal terminal)
    {
        List<Rule> possibleRules = new List<Rule>();
        //Grammar.PrintRules(Rules);
       
        foreach (Rule rule in Rules)
        {
            rule.PrintRule();
            if (rule.Get(0).HasInFirst(terminal))
            {
                possibleRules.Add(rule);
            }
        }
        
        return possibleRules;
    }

    /// <summary>
    /// Test if the token is matching with the symbol. Returns false because a token cannot match a NonTerminal
    /// </summary>
    /// <param name="token"></param>
    /// <returns>false</returns>
    public bool IsTokenMatching(Token token)
    {
        return false;
    }

    /// <summary>
    /// Return true if the other's token is in the firsts of the Non Terminal
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool HasInFirst(Terminal other)
    {
        // Tester si liste vide, et regarder suivant si c'est le cas
        return _firsts.Contains(other);
    }


    public override string ToString()
    {
        return this.Label;
    }

    public List<Terminal> GetFirst()
    {
        return _firsts;
    }

    public List<Terminal> GetNext()
    {
        return _nexts;
    }

    public void PrintFirst()
    {
        Console.WriteLine("Les premiers de " + Label + " sont :");
        foreach (var f in _firsts)
        {
            Token t = f.GetToken();
            Console.Write(Tag.GetTagDescription(f.GetToken().GetTag()) + " ");
        }
        Console.WriteLine();
    }
    
    public void PrintNext()
    {
        Console.WriteLine("Les suivants de " + Label + " sont :");
        foreach (var f in _nexts)
        {
            Token t = f.GetToken();
            Console.Write(Tag.GetTagDescription(f.GetToken().GetTag()) + " ");
        }
        Console.WriteLine();
    }
}