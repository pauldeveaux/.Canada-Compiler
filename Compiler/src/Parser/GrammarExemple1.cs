using System.Runtime.InteropServices.JavaScript;
using Lexer;

namespace Parser;

public class GrammarExemple1
{
    private Dictionary<string, NonTerminal> nonTerminals { get; }

    private Dictionary<int, Terminal> terminals { get; }
    
    //(Rappel : P_eps est l'ensemble des non terminaux qui se dérive en mot vide)
    private Dictionary<string, NonTerminal> p_eps { get; }  
    private NonTerminal Axiom { get; }

    public GrammarExemple1()
    {
        //Initialisation de P_eps
        p_eps = new Dictionary<string, NonTerminal>();
        
        // Création des non terminaux
        nonTerminals = new Dictionary<string, NonTerminal>();
        
        List<String> ln = new List<string>
        {
            "S", "A", "B", "C", "D",
        };
        foreach (var labelNonTerminal in ln)
        {
            nonTerminals.Add(labelNonTerminal, new NonTerminal(labelNonTerminal));
        }
        
        // Création des terminaux
        terminals = new Dictionary<int, Terminal>();
        List<string > ltc = new List<string>
        {
            "x","y","z","t","NULL"
        };
        foreach (var labelTerminal in ltc)
        {
            terminals.Add(Tag.GetTagFromDescription(labelTerminal), new Terminal(Tag.GetTagFromDescription(labelTerminal)));
        }
        
        // On récupère l'axiome
        Axiom = nonTerminals["S"];
        
        // On crée nos règles de la grammaire
        nonTerminals["S"]
            .AddRule(nonTerminals["A"], nonTerminals["B"],nonTerminals["C"])
            .AddRule(nonTerminals["D"], nonTerminals["A"], nonTerminals["D"]);
        
        nonTerminals["A"]
            .AddRule(terminals[Tag.GetTagFromDescription("x")], nonTerminals["A"])
            .AddRule();
        
        nonTerminals["B"]
            .AddRule(terminals[Tag.GetTagFromDescription("y")], nonTerminals["B"])
            .AddRule();
        
        nonTerminals["C"]
            .AddRule(terminals[Tag.GetTagFromDescription("z")], nonTerminals["D"])
            .AddRule();
        
        nonTerminals["D"]
            .AddRule(terminals[Tag.GetTagFromDescription("t")], nonTerminals["D"])
            .AddRule(terminals[Tag.GetTagFromDescription("NULL")]);
    }
    
    // On rempli List<Terminal> _first de chaque nonTerminal de la grammaire
    public void AddFirst()
    {
        StreamReader sr = new StreamReader("../../../src/grammaire_data/first_exemple1.txt");
        
        string line = sr.ReadLine();
        while (line != null)
        {   
            string[] words = line.Split(' ');
            if (this.GetNonTerminals().TryGetValue(words[0],out NonTerminal nt))
            {
                for (int i = 1; i < words.Length ; i++)
                {
                    nt.GetFirst().Add(new Terminal(Tag.GetTagFromDescription(words[i])));
                }
            }
            else
            {
                Console.WriteLine("NonTerminal non trouvé pour l'étiquette");
            }
            line = sr.ReadLine();
        }
    }

    public void AddNext()
    {
        StreamReader sr = new StreamReader("../../../src/grammaire_data/next_exemple1.txt");
        
        string line = sr.ReadLine();
        while (line != null)
        {   
            string[] words = line.Split(' ');
            if (this.GetNonTerminals().TryGetValue(words[0],out NonTerminal nt))
            {
                for (int i = 1; i < words.Length ; i++)
                {
                    nt.GetNext().Add(new Terminal(Tag.GetTagFromDescription(words[i])));
                }
            }
            else
            {
                Console.WriteLine("NonTerminal non trouvé pour l'étiquette");
            }
            line = sr.ReadLine();
        }
    }

    public void AddPeps()
    {
        StreamReader sr = new StreamReader("../../../src/grammaire_data/p_eps_exemple1.txt");
        string line = sr.ReadLine();
        string[] words = line.Split(' ');
        foreach (var w in words)
        {
            p_eps.Add(w, new NonTerminal(w));
        }
        
    }

    public void PrintPeps()
    {
        Console.WriteLine("Les non terminaux se dérivant en le mot vide sont :");
        foreach (var nt in p_eps.Keys)
        {
            Console.Write(nt + " ");
        }
        Console.WriteLine();
    }

    public void PrintNonTerminals()
    {
        foreach (var nt in nonTerminals.Values)
        {
            Console.WriteLine(nt.Label);
        }
    }
    
    public void PrintTerminals()
    {
        foreach (var t in terminals.Values)
        {
            t.Token.PrintToken();
        }
    }

    public void PrintAllRules()
    {
        foreach (var nt in nonTerminals.Values)
        {
            PrintRules(nt.Rules);
        }

    }
    
    // Test affichant les régles sur le terminal
    public static void PrintRules(List<Rule> rules)
    {
        foreach (var rule in rules)
        {
            rule.PrintRule();
        }
    }
    
    public Dictionary<string, NonTerminal> GetNonTerminals()
    {
        return nonTerminals;
    }

}