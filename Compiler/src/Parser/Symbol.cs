using Lexer;

namespace Parser;

public interface Symbol
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public abstract bool HasInFirst(Terminal other);

    public abstract List<Terminal> GetFirst();
    public abstract List<Terminal> GetNext();

    public abstract String GetLabel();

    public abstract List<Rule> GetRules();
    
    /// <summary>
    /// Research the rules that must be parsed if the terminal's token has been read
    /// </summary>
    /// <param name="terminal"></param>
    /// <returns></returns>
    public abstract List<Rule> GetRuleWith(Terminal terminal);

    /// <summary>
    /// Test if the token is matching with the symbol.
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public abstract bool IsTokenMatching(Token token);
}