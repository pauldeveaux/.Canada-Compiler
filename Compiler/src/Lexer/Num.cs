namespace Lexer;

using TagClass = Tag;

public class Num : Token
{
    public int Value { get; }

    public Num(int v) : base(global::Lexer.Tag.NUM)
    {
        Value = v;
    }
    
    public Num(int v, int line) : base(global::Lexer.Tag.NUM, line)
    {
        Value = v;
    }

    public override string ToString()
    {
        return $"Token<{Tag}, {Value}>";
    }

    public override void PrintToken()
    {
        string tagStr = TagClass.GetTagDescription(Tag);

        Console.Write($"Token<{Tag}(");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write(tagStr);
        Console.ResetColor();
        Console.Write($"), {Value}>\n");
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || !(obj is Num))
            return false;
        Num otherNum = (Num)obj;
        return Tag == otherNum.Tag && Value == otherNum.Value;
    }
}
