namespace Lexer;
    
public static class Tag
{
    // Double Characters Operators
    public const int 
        ASSIGN = 256,       // :=
        INF_OR_EQUAL = 257, // <= 
        SUP_OR_EQUAL = 258, // >=
        NOT_EQUAL = 259; // /=
        
        
    // Constants, Identifiers and Keywords
    public const int NUM = 300, ID = 301, ACCESS = 302, AND = 303, BEGIN = 304, ELSE = 305, ELSIF = 306, END = 307,
        FALSE = 308, FOR = 309, FUNCTION = 310, IF = 311, IN = 312, IS = 313, LOOP = 314, NEW = 315, NOT = 316,
        NULL = 317, OR = 318, OUT = 319, PROCEDURE = 320, RECORD = 321, REM = 322, RETURN = 323, REVERSE = 324,
        THEN = 325, TRUE = 326, TYPE = 327, USE = 328, WHILE = 329, WITH = 330, ADATEXT_IO = 331, CHARACTER = 332, 
        VAL = 333, CHAR = 334, DOT_DOT = 335, EOF = 65535;


    /// <summary>
    /// Table containing the correspondance between the integer tag and their description
    /// </summary>
    private static Dictionary<int, string> _stringTagTable = new()
    {
        {0, "NUL"},
        {1, "SOH"},
        {2, "STX"},
        {3, "ETX"},
        {4, "EOT"},
        {5, "ENQ"},
        {6, "ACK"},
        {7, "BEL"},
        {8, "BS"},
        {9, "HT"},
        {10, "LF"},
        {11, "VT"},
        {12, "FF"},
        {13, "CR"},
        {14, "SO"},
        {15, "SI"},
        {16, "DLE"},
        {17, "DC1"},
        {18, "DC2"},
        {19, "DC3"},
        {20, "DC4"},
        {21, "NAK"},
        {22, "SYN"},
        {23, "ETB"},
        {24, "CAN"},
        {25, "EM"},
        {26, "SUB"},
        {27, "ESC"},
        {28, "FS"},
        {29, "GS"},
        {30, "RS"},
        {31, "US"},
        {32, "SP"},
        {65535, "EOF"},
        
        // Double Characters Operators
        {ASSIGN, ":="},
        {INF_OR_EQUAL, "<="},
        {SUP_OR_EQUAL, ">="},
        {NOT_EQUAL, "/=" },
            
        // Constants and identifiers
        {CHAR, "CHAR"},
        {NUM, "NUM"}, 
        {ID, "ID"},
            
        // KEYWORDS
        {ADATEXT_IO, "Ada.Text_IO"},
        {ACCESS, "ACCESS"},
        {AND, "AND"},
        {BEGIN, "BEGIN"},
        {CHARACTER, "CHARACTER"},
        {ELSE, "ELSE"},
        {ELSIF, "ELSIF"},
        {END, "END"},
        {FALSE, "FALSE"},
        {FOR, "FOR"},
        {FUNCTION, "FUNCTION"},
        {IF, "IF"},
        {IN, "IN"},
        {IS, "IS"},
        {LOOP, "LOOP"},
        {NEW, "NEW"},
        {NOT, "NOT"},
        {NULL, "NULL"},
        {OR, "OR"},
        {OUT, "OUT"},
        {PROCEDURE, "PROCEDURE"},
        {RECORD, "RECORD"},
        {REM, "REM"},
        {RETURN, "RETURN"},
        {REVERSE, "REVERSE"},
        {THEN, "THEN"},
        {TRUE, "TRUE"},
        {TYPE, "TYPE"},
        {USE, "USE"},
        {VAL, "val"},
        {WHILE, "WHILE"},
        {WITH, "WITH"},
        {DOT_DOT, "DOT_DOT"}
    };
    
    
    /// <summary>
    /// Get the textual description of a tag
    /// </summary>
    /// <param name="tag">The integer representing the tag</param>
    /// <returns>A description of a tag</returns>
    public static string GetTagDescription(int tag)
    {
        if (tag >= 33 && tag <= 255)
        {
            return ((char)tag).ToString();
        }
        else
        {
            if (_stringTagTable.TryGetValue(tag, out var desc))
                return desc;
            return "UNKNOWN";
        }
    }

    /// <summary>
    /// Get the tag of a description string
    /// Multiple calls of this method can slow down the compiler
    /// </summary>
    /// <param name="description"></param>
    /// <returns>The tag corresponding of the description (int), or -1 if the description does not exist</returns>
    public static int GetTagFromDescription(string description)
    {
        if (description.Length == 1)
            return description[0];
        int tag = _stringTagTable.FirstOrDefault(x => x.Value == description).Key;
        if (tag == 0)
            tag = -1;
        return tag;
    }

    public static bool IsSingleChar(int tagId)
    {
        return tagId >= 48 && tagId <= 90 || tagId >= 97 && tagId <= 122;
    }
}
