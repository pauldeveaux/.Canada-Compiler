namespace Lexer;

using TagClass = Tag;

public class Token
{ 
    public readonly int Tag;
    public int Line { get;  }

    public Token(int t)
    {
        Tag = t;
    }

    public Token(int t, int line)
    {
        Tag = t;
        Line = line;
    }

    public override string ToString()
    {
        string tagStr = TagClass.GetTagDescription(Tag);
        
        
        return $"Token<{Tag}:{TagClass.GetTagDescription(Tag)}>";
    }

    public virtual void PrintToken()
    {
        string tagStr = TagClass.GetTagDescription(Tag);

        Console.Write($"Token<{Tag}(");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write(tagStr);
        Console.ResetColor();
        Console.Write(")>\n");
    }

    /// <summary>
    /// Test if two Token objects are equals. 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>True if their tag are the same, else return false</returns>
    public override bool Equals(object? obj)
    {
        if (obj == null || !(obj is Token))
            return false;
        Token otherToken = (Token)obj;
        return Tag == otherToken.Tag;
    }

    public int GetTag()
    {
        return Tag;
    }
    
}
