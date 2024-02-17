using Exceptions;
using Lexer;

namespace Parser;

public class Parser
{
    private Grammar _grammar;
    private Lexer.Lexer _lexer;
    private ParseTree _parseTree;

    
    public Parser(Lexer.Lexer lexer)
    {
        _grammar = new Grammar();
        _lexer = lexer;
        _parseTree = new ParseTree(_grammar.Axiom);
    }

    /// <summary>
    /// Scan the file in the STDIN and returns the AST
    /// </summary>
    /// <returns></returns>
    public ParseTree Scan()
    {
        Token token = _lexer.Scan();
        
        _parseTree.SetRule(_grammar.Axiom.Rules[0]);

        try
        {
            do
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("READING : " + token);
                Console.ResetColor();
                _parseTree = _parseTree.Move(token);
                token = _lexer.Scan();
            } while (_parseTree.Symbol != _grammar.Axiom);

            _parseTree.callSemanticFunction();
            
            
            while (_parseTree.Symbol != _grammar.Axiom)
            {
                _parseTree = _parseTree.Parent;
            }
        }
        catch (SyntaxicException e)
        {
            while (_parseTree.Symbol != _grammar.Axiom)
            {
                _parseTree = _parseTree.Parent;
            }
            
            // Priorité aux exception lexicales
            do
            {
                token = _lexer.Scan();
            } while (token.Tag != Tag.EOF);
            
            throw;
        }
        catch (UnrecognizedChar e2)
        {
            // On continue le scan des erreurs lexicales pour toutes les avoir
            do
            {
                token = _lexer.Scan();
            } while (token.Tag != Tag.EOF);

            throw;
        }
        

        

        return _parseTree;
    }
}