namespace Exceptions;

public class ExceptionsLexicales : Exception{
    public ExceptionsLexicales(string message) : base("Erreur lexicale : " + message)
    {
        
    }
}