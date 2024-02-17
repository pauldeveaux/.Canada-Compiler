namespace Lexer;

public class CharacterToken : Token
{

    public readonly char charac;
    public CharacterToken(char charac) : base(global::Lexer.Tag.CHAR)
    {
        this.charac = charac;
    }
    
    public CharacterToken(char charac, int line) : base(global::Lexer.Tag.CHAR, line)
    {
        this.charac = charac;
    }
    
    
    public override bool Equals(object? obj)
    {
        if (obj == null || !(obj is CharacterToken))
            return false;
        return true;
    }
}