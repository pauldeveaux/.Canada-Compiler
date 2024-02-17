using System.Runtime.InteropServices;
using Lexer;

namespace Parser;

public class Rule
{
    public List<Symbol> Rights_members;
    public Action<AbstractTree> SemanticFunction { get; private set; }
    private String _leftElem;
    private List<Terminal> sd;
    
    
    public Rule(List<Symbol> rightMembers, String leftElem, [Optional] Action<AbstractTree>? lambda, bool d)
    {
        _leftElem = leftElem;
        Rights_members = rightMembers;
        if (lambda is null)
            lambda = (n) => {};

        SemanticFunction = (n) =>
        {
            lambda(n);
            n.DeleteIfEmpty();
            if (!d)
                n.ReplaceByChildIfOnlyOne();
        };
        sd = new List<Terminal>();
    }
    

    public List<Symbol> GetSymbol()
    {
        return this.Rights_members;
    }

    public void AddSd(Terminal t)
    {
        sd.Add(t);
    }

    public bool IsGoodRule(Token t)
    {
        foreach (var terminal in sd)
        {
            if (t.GetTag() == terminal.GetToken().GetTag())
            {
                return true;
            } 
        }
        
        return false;
    }

    public Symbol Get(int i)
    {
        return Rights_members[i];
    }

    /// <summary>
    /// Print the rule : A -> A a B ...
    /// </summary>
    public void PrintRule()
    {
        Console.WriteLine("Rule :");
    
        Console.Write(_leftElem + " -> ");
        foreach (var symbol in Rights_members)
        {
            Console.Write(symbol + " ");
        }
        Console.WriteLine("\n");
    }
    
    public void PrintRule(int pointer)
    {
        Console.WriteLine("Rule :");
    
        Console.Write(_leftElem + " -> ");
        
        int i = 0;
        foreach (var symbol in Rights_members)
        {
            if (i == pointer)
                Console.ForegroundColor = ConsoleColor.Red;
            
            Console.Write(symbol + " ");
            
            if (i == pointer)
                Console.ResetColor();
            i++;
        }
        Console.WriteLine("\n");
    }
    
    
    
}