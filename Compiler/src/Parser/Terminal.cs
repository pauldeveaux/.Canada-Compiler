using System.Formats.Asn1;
using Lexer;

namespace Parser;

public class Terminal : Symbol
{
    public Token Token { get; }


    public Terminal()
    {
        Token = new Token(-20);
    }

    /// <summary>
    /// Construct a Terminal from a token.
    /// </summary>
    /// <param name="token"></param>
    public Terminal(Token token)
    {
        Token = token;
    }

    /// <summary>
    /// Construct a Terminal from a lexem. The terminal's token will have an id equal Tag.ID, and a lexeme equals to lexeme
    /// </summary>
    /// <param name="lexeme"></param>
    public Terminal(string lexeme)
    {
        Token = new Word(Tag.ID, lexeme);
    }

    /// <summary>
    /// Construct a terminal from a tag. The terminal's token will have an id equal to the tag ID.
    /// </summary>
    /// <param name="tag"></param>
    public Terminal(int tag)
    {
        Token = new Token(tag);
    }

    /// <summary>
    ///  Construct a Terminal from a char. The terminal's token will have an id equal to the char ASCII.
    /// </summary>
    /// <param name="c"></param>
    public Terminal(char c)
    {
        Token = new Token(c);
    }

    public string GetLabel()
    {
        if (Token is Word)
            return ((Word)Token).Lexeme;

        if (Token is Num)
            return "" + ((Num)Token).Value;

        if (Token is CharacterToken)
            return "'" + ((CharacterToken)Token).charac + "'";

        return Tag.GetTagDescription(Token.Tag);
        
        
        return "aboubacar"; //Il faut ici juste renvoyer un label qui n'est pas un mot clé 
    }

    public List<Terminal> GetFirst()
    {
        List<Terminal> lt = new List<Terminal>();
        lt.Add(this);
        return lt;
    }
    
    public List<Terminal> GetNext()
    {
        return new List<Terminal>();
    }
    
    /// <summary>
    /// Return true if the other's token match with the terminal's token.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool HasInFirst(Terminal other)
    {
        var ret = IsTokenMatching(other.Token);
        Console.WriteLine(ret);
        return ret;
    }


    /// <summary>
    /// Return an empty list because a terminal does not have rules.
    /// </summary>
    /// <param name="terminal"></param>
    /// <returns></returns>
    public List<Rule> GetRuleWith(Terminal terminal)
    {
        return new List<Rule>();
    }
    
    
    /// <summary>
    /// Return true if the token match with the terminal's token.
    /// They match if they have the same Tag.
    /// If the terminal's token has a NUM tag, their value must be equal. If the terminal's token doesn't have a value,
    /// they match.
    /// If the terminal's token has a ID tag, their lexeme must be equal. If the terminal's token doesn't have a lexeme,
    /// they match.
    /// </summary>
    /// <param name="token"></param>
    /// <returns>True if the token match with the terminal's token, else false</returns>
    public virtual bool IsTokenMatching(Token token)
    {
        // Test if both tokens have the same tag
        if (Token.Tag != token.Tag)
            return false;

        // Test if both Num tokens have the same value
        if (Token.Tag == Tag.NUM)
        {
            // If the token has Tag.NUM is not a Num, we don't care about the value
            if (!(Token is Num))
                return true;
            return ((Num)Token).Value == ((Num)token).Value;
        }
        
        // Test if both Word token have the same lexeme
        if (Token.Tag == Tag.ID)
        {
            // If the token has Tag.ID but is not a word, we don't care about the lexeme
            if (!(Token is Word))
                return true;
            return ((Word)Token).Lexeme == ((Word)token).Lexeme;
        }

        return true;
    }

    /// <summary>
    /// Two Terminals are equal if their tokens match
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj)
    {
        if (!(obj is Terminal))
            return false;
        return this.IsTokenMatching(((Terminal)obj).Token);
    }
    
    public override string ToString()
    {
        return GetLabel();
        return Tag.GetTagDescription(this.Token.Tag);
        //return this._token.ToString();
    }

    public Token GetToken()
    {
        return Token;
    }
    
    public List<Rule> GetRules()
    {
        return new List<Rule>();
    }
}