using System.Collections;
using System.Text;
using Exceptions;

namespace Lexer;

public class Lexer
{

    public static int Line { get; private set; } = 1;
    private char _look = ' ';
    private char _peek = ' ';
    private bool isInString = false; 
    
    public static List<char> guillemets = new List<char> {'"', '\''};
    
    public List<char> _knownChars = new List<char>
    {
        '?', ' ', ':', '\t', '\n', '\r', '*', '/', '+', '-', '<', '>', '=', '!', '(', ')', '{', '}', ';', '.', '_', '"', '\'', ',', ((char)65535)
    };
    private bool _doubleRead = false;

    private Dictionary<string, Word> _words = new();

    private List<String> errors = new List<string>(); 

    public Lexer() {
        Reserve(new Word(Tag.ADATEXT_IO , "Ada.Text_IO"));
        Reserve(new Word(Tag.ACCESS , "access"));
        Reserve(new Word(Tag.AND , "and"));
        Reserve(new Word(Tag.BEGIN , "begin"));
        Reserve(new Word(Tag.CHARACTER, "character"));
        Reserve(new Word(Tag.ELSE , "else"));
        Reserve(new Word(Tag.ELSIF , "elsif"));
        Reserve(new Word(Tag.END , "end"));
        Reserve(new Word(Tag.FALSE , "false"));
        Reserve(new Word(Tag.FOR , "for"));
        Reserve(new Word(Tag.FUNCTION , "function"));
        Reserve(new Word(Tag.IF , "if"));
        Reserve(new Word(Tag.IN , "in"));
        Reserve(new Word(Tag.IS , "is"));
        Reserve(new Word(Tag.LOOP , "loop"));
        Reserve(new Word(Tag.NEW , "new"));
        Reserve(new Word(Tag.NOT , "not"));
        Reserve(new Word(Tag.NULL , "null"));
        Reserve(new Word(Tag.OR , "or"));
        Reserve(new Word(Tag.OUT , "out"));
        Reserve(new Word(Tag.PROCEDURE , "procedure"));
        Reserve(new Word(Tag.RECORD , "record"));
        Reserve(new Word(Tag.REM , "rem"));
        Reserve(new Word(Tag.RETURN , "return"));
        Reserve(new Word(Tag.REVERSE , "reverse"));
        Reserve(new Word(Tag.THEN , "then"));
        Reserve(new Word(Tag.TRUE , "true"));
        Reserve(new Word(Tag.TYPE , "type"));
        Reserve(new Word(Tag.USE , "use"));
        Reserve(new Word(Tag.VAL, "val"));
        Reserve(new Word(Tag.WHILE , "while"));
        Reserve(new Word(Tag.WITH , "with"));
    }

    /// <summary>
    /// Add keywords into the words table
    /// </summary>
    /// <param name="w">The word to add into the table</param>
    private void Reserve(Word w) {
        _words.Add(w.Lexeme, w);
    }

    /// <summary>
    /// Reads the next character, put it in the _look variable.
    /// Put the character after the next character in the _peek variable
    /// </summary>
    private void Readchar()
    {
        if (_doubleRead)
        {
            _look = _peek;
            _peek = ' ';
            _doubleRead = false;
        }
        else
        {
            _look = (char)Console.Read();

            //Console.Write(_look);
            if (!IsKnown(_look) && !isInString)
                throw new UnrecognizedChar(_look.ToString(), Line, this);

        }
    }
    
    /// <summary>
    /// Scan all the stdin and returns a list of tokens
    /// </summary>
    /// <returns>All the tokens in a list</returns>
    public (List<Token>, List<String>) ScanAll()
    {
        List<Token> tokens = new List<Token>();
        Token t = Scan();
        do
        {
            //Console.Write(_look);
            try
            {
                t = Scan();
                tokens.Add(t);
            } catch (UnrecognizedChar e)
            {
                if ((int)(_look) == 65535)
                    break; 
                errors.Add(e.Message);
                Readchar();
            }
        } while (t.Tag != 65535);
        return (tokens, errors);
    }
    
    /// <summary>
    /// Scan the next token in the stdin
    /// </summary>
    /// <returns>A token</returns>
    public Token Scan(){
        Token ret;
        try
        {
            RemoveWhiteSpace();

            ret = HandleNumbers();
            if (ret is not null)
                return ret;

            ret = HandleCharacters();
            if (ret is not null)
                return ret;

            ret = HandleIdentifiersAndKeywords();
            if (ret is not null)
                return ret;

            ret = HandleTwoCharactersToken();
            if (ret is not null)
                return ret;
        } catch (UnrecognizedChar e)
        {
            if (e.unrecognizedChar != 65535)
            {
                errors.Add(e.Message);
                throw;
            }
        }


        // Single characters
        Token tok = new Token(_look, Line);
        
        _look = ' ';
        return tok;
    }


    /// <summary>
    /// Remove the Whitespaces, \n, comments, ... from the stdin
    /// </summary>
    private void RemoveWhiteSpace()
    {
        for( ; ; Readchar()){
            if (_look == ' ' || _look == 11 || _look == 13)
            { 
                continue;
            } // 11 = tab, 13 = Retour chariot (différent de saut de ligne)
            else if(_look == 10) Line = Line + 1; // 10 = Saut de ligne
            else if (_look == '-') // test si commentaire --
            {
                _peek = (char)Console.Read();
                _doubleRead = true;
                if (_peek == '-')
                {
                    while (_look != 10)
                    {
                        try
                        {
                            Readchar();
                        }
                        catch (UnrecognizedChar e)
                        {
                            // Nothing
                        }
                        
                    }
                    Line += 1;
                }
                else break;
            }
            else break;
        }
    }

    /// <summary>
    /// If there is a digit, build a number token with the value of the current and next digits
    /// </summary>
    /// <returns>Return a Num Token</returns>
    private Token? HandleNumbers()
    {
        if (char.IsDigit(_look)) {
            int v = 0;
            do {
                v = 10 * v + int.Parse(_look.ToString());
                _look = (char)System.Console.Read();
            } while (char.IsDigit(_look));
            return new Num(v, Line);
        }

        return null;
    }


    private Token? HandleCharacters()
    {
        // TODO Surement à revoir
        if (_look.Equals('\''))
        {
            char c;
            Readchar();
            c = _look;
            Readchar();
            if (_look != '\'')
                throw new Exception("Pas un char TODO"); // TODO
            Readchar();
            CharacterToken tok = new CharacterToken(c, Line);
            return tok;
        }

        return null;
    }


    /// <summary>
    /// Recognize identifiers and keywords and returns a corresponding token
    /// </summary>
    /// <returns>A Token corresponding to the keyword or a Word with an ID tag and the lexeme</returns>
    private Token? HandleIdentifiersAndKeywords()
    {
        if (char.IsLetter(_look)) {
            StringBuilder b = new StringBuilder();
            do {
                b.Append(_look);
                Readchar();
            } while (char.IsLetterOrDigit(_look) || _look=='_');
            string s = b.ToString();

            // TODO TESTER ADA.TEXT_IO
            
            if (_words.TryGetValue(s, out var wordToken))
            {
                return new Word(wordToken.Tag, wordToken.Lexeme, Line);
            }
            Word w = new Word(Tag.ID, s, Line);
            _words.Add(s, w);
            return w;
        }
     /*   if(guillemets.Contains(_look) && !isInString)
        {
            isInString = true;
            return new Token(_look);
        }*/
        return null;
    }


    /// <summary>
    /// Manage the two characters tokens with the lookaheads
    /// </summary>
    /// <returns>A token corresponding of the two characters word</returns>
    private Token? HandleTwoCharactersToken()
    {
        // Gestion de l'opérateur :=
        if (_look == ':')
        {
            Readchar();
            if (_look == '=')
            {
                Readchar();
                // _words.TryGetValue(":=", out var wordToken);
                return new Token(Tag.ASSIGN, Line);
            }
            else
            {
                Token tok_div = new Token(':', Line);
                return tok_div;
            }
        }

        // gestion de l'opérateur /=
        if (_look == '/')
        {

            Readchar();
            if (_look == '=')
            {
                Readchar();
                return new Token(Tag.NOT_EQUAL, Line);
            }
            else
            {
                Token tok_div = new Token('/', Line);
                return tok_div;
            }
        }
        // gestion de l'opérateur >=
        if (_look == '>')
        {

            Readchar();
            if (_look == '=')
            {
                Readchar();
                return new Token(Tag.SUP_OR_EQUAL, Line);
            }
            else
            {
                Token tok_div = new Token('>', Line);
                return tok_div;
            }
        }

        // gestion de l'opérateur <=
        if (_look == '<')
        {

            Readchar();
            if (_look == '=')
            {
                Readchar();
                _words.TryGetValue("<=", out var wordToken);
                return new Token(Tag.INF_OR_EQUAL, Line);
            }
            else
            {
                Token tok_div = new Token('<', Line);
                return tok_div;
            }
        }
        
        // Gestion de ..
        if (_look == '.')
        {

                Readchar();
            if (_look == '.')
            {
                Readchar();
                _words.TryGetValue("..", out var wordToken);
                return new Token(Tag.DOT_DOT, Line);
            }
            else
            {
                Token tok_div = new Token('.', Line);
                return tok_div;
            }
        }
        return null;
    }
    
    
    private bool IsKnown(char c)
    {
        return Char.IsLetterOrDigit(c) || _knownChars.Contains(c);    
    }

    public List<Token> scanAda(List<Token> tokens)
    {
        List<Token> tokensB = new List<Token>();
        int c = 0;
        foreach (var t in tokens)
        {
            if (t.GetTag() == 301 && ((Word)t).GetIDname() == "Ada")
            {
                Console.WriteLine("test if réussi");
                tokensB.Add(new Token(331));
                c = 2;
            }
            else if (c > 0)
            {
                c = c - 1;
            }
            else
            {
                tokensB.Add(t);
            }
        }

        return tokensB;
    }
    
    public List<String> GetErrors()
    {
        return errors;
    }
}

