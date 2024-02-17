namespace Lexer;
    
using TagClass = Tag;

public class Word : Token {

    public readonly string Lexeme; 

    public Word(int tag, string lexeme) : base(tag)
    {
        Lexeme = lexeme;
    }

    
    public Word(int tag, string lexeme, int line) : base(tag, line)
    {
        Lexeme = lexeme;
    }
    
    public override string ToString()
    {
        return $"Token<{Tag}, {Lexeme}>";
    }

    public string GetIDname()
    {
        return Lexeme;
    }
    
    public override void PrintToken()
    {
        string tagStr = TagClass.GetTagDescription(Tag);

        Console.Write($"Token<{Tag}(");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write(tagStr);
        Console.ResetColor();
        Console.Write($"), {Lexeme}>\n");
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || !(obj is Word))
            return false;
        Word otherWord = (Word)obj;
        return Tag == otherWord.Tag && Lexeme == otherWord.Lexeme;
    }
}
