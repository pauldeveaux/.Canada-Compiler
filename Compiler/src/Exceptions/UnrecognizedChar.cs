using Lexer;

namespace Exceptions;

public class UnrecognizedChar : ExceptionsLexicales
{
    public int unrecognizedChar { get; set; }
    public Lexer.Lexer Lexer {get;}

    public UnrecognizedChar(string message, int Line) : base(" caractère non reconnu : '" + Tag.GetTagDescription(message[0]) + "' n'est pas un caractère valide à la ligne : " + Line)
    {
        unrecognizedChar = (int)(message[0]);
    }
    
    public UnrecognizedChar(string message, int Line, Lexer.Lexer lexer) : base(" caractère non reconnu : '" + Tag.GetTagDescription(message[0]) + "' n'est pas un caractère valide à la ligne : " + Line)
    {
        unrecognizedChar = (int)(message[0]);
        this.Lexer = lexer;
    }
}