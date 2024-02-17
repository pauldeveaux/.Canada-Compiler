using Lexer;
using System;

namespace Parser;

public class Grammar
{
    /// <summary>
    /// Dictionnary containing as key the names of the NonTerminal, and as value the NonTerminal
    /// </summary>
    private Dictionary<string, NonTerminal> _nonTerminals;
    
    private Dictionary<int, Terminal> terminals { get; }
    
    //(Rappel : P_eps est l'ensemble des non terminaux qui se dérive en mot vide)
    private Dictionary<string, NonTerminal> p_eps;
    
    /// <summary>
    /// The axiom of the grammar
    /// </summary>
    public NonTerminal Axiom { get; }

    private Dictionary<string, Token> _words = new();

    /// <summary>
    /// Constructor of the grammar
    /// </summary>
    public Grammar()
    {
        Reserve(new Word(Tag.ADATEXT_IO , "Ada.Text_IO"));
        Reserve(new Word(Tag.ACCESS , "access"));
        Reserve(new Word(Tag.AND , "and"));
        Reserve(new Word(Tag.BEGIN , "begin"));
        Reserve(new Word(Tag.CHARACTER, "character"));
        Reserve(new Word(Tag.CHAR, "caractere"));
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
        Reserve(new Word(Tag.WITH , "with"));
        Reserve(new Word(Tag.VAL, "val"));
        Reserve(new Word(Tag.WHILE , "while"));
        Reserve(new Word(Tag.ID , "Ident"));
        Reserve(new Word(';' , ";"));
        Reserve(new Word('(' , "("));
        Reserve(new Word(Tag.NUM , "entier"));
        Reserve(new Word(')' , ")"));
        Reserve(new Word('+' , "+"));
        Reserve(new Word('-' , "-"));
        Reserve(new Word('*' , "*"));
        Reserve(new Word('/' , "/"));
        Reserve(new Word(Tag.NOT_EQUAL , "/="));
        Reserve(new Word('=' , "="));
        Reserve(new Word(Tag.SUP_OR_EQUAL , ">="));
        Reserve(new Word('>' , ">"));
        Reserve(new Word(Tag.INF_OR_EQUAL , "<="));
        Reserve(new Word('<' , "<"));
        Reserve(new Word(',' , ","));
        Reserve(new Word('.' , "."));
        Reserve(new Word(Tag.ASSIGN , ":="));
        Reserve(new Word(Tag.EOF, "$"));
        Reserve(new Word(':', ":"));
        Reserve(new Word(Tag.DOT_DOT,"Dot_Dot"));
        
        _nonTerminals = new Dictionary<string, NonTerminal>();
        
        // List used to create the NonTerminal. 
        // Enter the labels of the NonTerminal here
        List<String> l = new List<String>
        {
          "S", "Start", "Fichier", "Decl_star", "Decl", "Decl_b", "Decl_a", "Champs", "Champs_plus", "Champs_star", "Type", "Params", "Param", "Param_plus", "Param_star", 
          "Mode", "Mode_a", "Instr_plus", "Instr_star", "Expr_virgule_plus", "Expr_virgule_star",  "Expr_egal_interog", "Expr_interog", "Elsif_star", "Else_interog", 
          "Else_i", "Then_i", "Instr", "Instr1", "Instr2", "Expr", "E1", "E2", "E3", "E4", "E5", "E6", "E7", "E8", "E9", "E10", "E11", "E12", "E13", "E14", "E14b",
          "E15+", "E15", "Reverse_interog", "Ident_virgule_plus","Ident_virgule_star", "Ident_interog",
        };
        //Start est l'axiome de la grammaire
        
        // Create the NonTerminal Dictionnary from the list
        foreach (var labelNT in l)
        {
            _nonTerminals.Add(labelNT, new NonTerminal(labelNT));
        }
        
        terminals = new Dictionary<int, Terminal>();
        terminals.Add(Tag.WITH,new Terminal(Tag.WITH));
        terminals.Add(Tag.ADATEXT_IO,new Terminal(Tag.ADATEXT_IO));
        terminals.Add(Tag.USE,new Terminal(Tag.USE));
        terminals.Add(Tag.PROCEDURE,new Terminal(Tag.PROCEDURE));
        terminals.Add(Tag.ID,new Terminal(Tag.ID));
        terminals.Add(Tag.BEGIN,new Terminal(Tag.BEGIN));
        terminals.Add(Tag.END,new Terminal(Tag.END));
        terminals.Add(';',new Terminal(';'));
        terminals.Add(Tag.EOF,new Terminal(Tag.EOF));
        terminals.Add(Tag.TYPE,new Terminal(Tag.TYPE));
        terminals.Add(':',new Terminal(':'));
        terminals.Add(Tag.FUNCTION,new Terminal(Tag.FUNCTION));
        terminals.Add(Tag.RETURN,new Terminal(Tag.RETURN));
        terminals.Add(Tag.ACCESS,new Terminal(Tag.ACCESS));
        terminals.Add(Tag.RECORD,new Terminal(Tag.RECORD));
        terminals.Add('(',new Terminal('('));
        terminals.Add(')',new Terminal(')'));
        terminals.Add(Tag.IN,new Terminal(Tag.IN));
        terminals.Add(Tag.OUT,new Terminal(Tag.OUT));
        terminals.Add(',',new Terminal(','));
        terminals.Add(Tag.ASSIGN,new Terminal(Tag.ASSIGN));
        terminals.Add(Tag.ELSIF,new Terminal(Tag.ELSIF));
        terminals.Add(Tag.THEN,new Terminal(Tag.THEN));
        terminals.Add(Tag.ELSE,new Terminal(Tag.ELSE));
        terminals.Add(Tag.NUM,new Terminal(Tag.NUM));
        terminals.Add(Tag.CHARACTER,new Terminal(Tag.CHARACTER));
        terminals.Add('\'',new Terminal('\''));
        terminals.Add(Tag.VAL,new Terminal(Tag.VAL));
        terminals.Add(Tag.FOR,new Terminal(Tag.FOR));
        terminals.Add(Tag.IF,new Terminal(Tag.IF));
        terminals.Add('.',new Terminal('.'));
        terminals.Add(Tag.LOOP,new Terminal(Tag.LOOP));
        terminals.Add(Tag.WHILE,new Terminal(Tag.WHILE));
        terminals.Add(Tag.OR,new Terminal(Tag.OR));
        terminals.Add(Tag.AND,new Terminal(Tag.AND));
        terminals.Add(Tag.NOT,new Terminal(Tag.NOT));
        terminals.Add('=',new Terminal('='));
        terminals.Add(Tag.NOT_EQUAL,new Terminal(Tag.NOT_EQUAL));
        terminals.Add('>',new Terminal('>'));
        terminals.Add(Tag.SUP_OR_EQUAL,new Terminal(Tag.SUP_OR_EQUAL));
        terminals.Add('<',new Terminal('<'));
        terminals.Add(Tag.INF_OR_EQUAL,new Terminal(Tag.INF_OR_EQUAL));
        terminals.Add('+',new Terminal('+'));
        terminals.Add('-',new Terminal('-'));
        terminals.Add('*',new Terminal('*'));
        terminals.Add('/',new Terminal('/'));
        terminals.Add(Tag.REM,new Terminal(Tag.REM));
        terminals.Add(Tag.REVERSE,new Terminal(Tag.REVERSE));
        terminals.Add(Tag.CHAR,new Terminal(Tag.CHAR));
        terminals.Add(Tag.DOT_DOT, new Terminal(Tag.DOT_DOT));

        
        
        
        

        // Augmentation de la grammaire
        Axiom = _nonTerminals["S"];

        // AXIOM
        _nonTerminals["S"].AddRule(_nonTerminals["Fichier"]);

        
        // TODO     POUR CREER AST, REGARDER EXEMPLE SUR LE NT FICHIER
        // TODO     METHODES DISPONIBLES EN BAS DE ABSTRACTTREE, APRES LE COMMENTAIRE SUR UNE LIGNE COMPLETE
        // TODO     LES FONCTIONS SEMANTIQUES ReplaceByChildIfOnlyOne ET  DeleteIfEmpty SONT APPELES AUTOMATIQUEMENT
        // TODO     LES METHODES DISPOS SONT DONC : KeepChildrens, RemoveChildrens ET RenameNode
        
        _nonTerminals["Fichier"]
            .AddRule(
            n => { 
                    n.RenameNode("PROGRAM"); n.KeepChildrens(11, 13,15,17); 
                    n.RenameChildren(15,"BLOCK");
                },
                new Terminal(Tag.WITH), new Terminal("Ada"), new Terminal('.'), new Terminal("Text_IO"), new Terminal(';'), new Terminal(Tag.USE),
                new Terminal("Ada"), new Terminal('.'), new Terminal("Text_IO"), new Terminal(';'), new Terminal(Tag.PROCEDURE),
                new Terminal(Tag.ID), new Terminal(Tag.IS), _nonTerminals["Decl_star"], new Terminal(Tag.BEGIN),
                _nonTerminals["Instr_plus"], new Terminal(Tag.END),
                _nonTerminals["Ident_interog"], new Terminal(';'), new Terminal(Tag.EOF));
        
        
        _nonTerminals["Decl_star"]
            .AddRule( //Decl_star -> Decl Decl_star
                (n) => { n.RenameNode("DECLS"); n.GiveChildsToParents();},
                _nonTerminals["Decl"], _nonTerminals["Decl_star"]
            )
            .AddRule( //Decl_star -> ''
                
            );


        _nonTerminals["Decl"]
            .AddRule( //Decl -> type ident Decl_b
                (n) =>
                { 
                    n.RemoveChildrens(0);
                },
                new Terminal(Tag.TYPE), new Terminal(Tag.ID), _nonTerminals["Decl_b"]
            )
            .AddRule( //Decl -> Ident_virgule_plus : Type Expr_egal_interog ;
                (n) => { n.RenameNode("DECL"); n.RemoveChildrens(1, 4); },
                _nonTerminals["Ident_virgule_plus"], new Terminal(':'), _nonTerminals["Type"],
                _nonTerminals["Expr_egal_interog"], new Terminal(';')
            )
            .AddRule( //Decl -> procedure ident Params is Decl_star begin Instr_plus end Ident_interog ;
                (n) => { n.RenameNode("PROCEDURE"); n.KeepChildrens(1,2,4,6,8);},
                new Terminal(Tag.PROCEDURE), new Terminal(Tag.ID), _nonTerminals["Params"], new Terminal(Tag.IS), _nonTerminals["Decl_star"], new Terminal(Tag.BEGIN),
                _nonTerminals["Instr_plus"], new Terminal(Tag.END), _nonTerminals["Ident_interog"], new Terminal(';')
            )
            .AddRule( //Decl -> function ident Params return Type is Decl_star begin Instr_plus end Ident_interog ;
                (n) =>  { n.RenameNode("FUNCTION"); n.KeepChildrens(1,2,4,6,8,10); n.RenameChildren(8,"FUNCTION_BLOCK");},
                new Terminal(Tag.FUNCTION), new Terminal(Tag.ID), _nonTerminals["Params"], new Terminal(Tag.RETURN), _nonTerminals["Type"], new Terminal(Tag.IS),
                _nonTerminals["Decl_star"], new Terminal(Tag.BEGIN), _nonTerminals["Instr_plus"], new Terminal(Tag.END), _nonTerminals["Ident_interog"], new Terminal(';')
            );
        
        
        _nonTerminals["Decl_b"]
            .AddRule( //Decl_b -> ;
                (n) =>  {n.RemoveChildrens(0);},
                new Terminal(';')
            )
            .AddRule( //Decl_b -> is Decl_a
                (n) => { n.RemoveChildrens(0); },
                new Terminal(Tag.IS), _nonTerminals["Decl_a"]
            );
        
        
        _nonTerminals["Decl_a"]
            .AddRule( //Decl_a -> access ident ;
                (n) => { n.RenameNode("ACCESS"); n.KeepChildrens(1);},
                new Terminal(Tag.ACCESS), new Terminal(Tag.ID), new Terminal(';')
            )
            .AddRule( //Decl_a -> record Champs_plus end record ;
                (n) =>  { n.KeepChildrens(1); }, 
                new Terminal(Tag.RECORD),_nonTerminals["Champs_plus"], new Terminal(Tag.END), new Terminal(Tag.RECORD), new Terminal(';')
            );
        
        
        _nonTerminals["Champs"]
            .AddRule( //Champs -> Ident_virgule_plus : Type ;
                (n) => {n.RenameNode("CHAMP"); n.KeepChildrens(0,2);},
                _nonTerminals["Ident_virgule_plus"], new Terminal(':'), _nonTerminals["Type"], new Terminal(';')
            );
        
        
        _nonTerminals["Champs_plus"]
            .AddRule( //Champs_plus -> Champs Champs_star
                (n) => {n.RenameNode("CHAMPS");},
                _nonTerminals["Champs"], _nonTerminals["Champs_star"]
            );
        
        
        _nonTerminals["Champs_star"]
            .AddRule( //Champs_star -> Champs Champs_star
                (n) => { n.GiveChildsToParents();},
                _nonTerminals["Champs"], _nonTerminals["Champs_star"]
            )
            .AddRule( //Champs_star -> ''
                
            );
        
        
        _nonTerminals["Type"]
            .AddRule( //Type -> ident
                new Terminal(Tag.ID)
            )
            .AddRule( //Type -> access ident
                (n) => { n.RemoveChildrens(0); n.RenameNode("ACCESS");}, true,
                new Terminal(Tag.ACCESS), new Terminal(Tag.ID)
            );
        
        
        _nonTerminals["Params"]
            .AddRule( //Params -> ( Param_plus ) 
                (n) => { n.RemoveChildrens(0,2); },
                new Terminal('('), _nonTerminals["Param_plus"], new Terminal(')')
            )
            .AddRule( //Params -> ''

            );
        
        
        _nonTerminals["Param"]
            .AddRule( //Param -> Ident_virgule_plus : Mode Type
                (n) => {n.RenameNode("PARAM"); n.RemoveChildrens(1);},
                _nonTerminals["Ident_virgule_plus"], new Terminal(':'), _nonTerminals["Mode"],_nonTerminals["Type"]
            );


        _nonTerminals["Param_plus"]
            .AddRule( //Param_plus -> Param Param_star
                (n) =>
                { n.RenameNode("PARAMS"); },
        _nonTerminals["Param"], _nonTerminals["Param_star"]
            );
        
        
        _nonTerminals["Param_star"]
            .AddRule( //Param_star -> ; Param Param_star
                (n) => { n.RemoveChildrens(0); n.GiveChildsToParents();},
                new Terminal(';'), _nonTerminals["Param"], _nonTerminals["Param_star"]
            )
            .AddRule( //Param_star -> ''
                
            );
        
        
        _nonTerminals["Mode"]
            .AddRule( //Mode -> in Mode_a
                (n) => { n.GiveChildsToParents();},
        new Terminal(Tag.IN), _nonTerminals["Mode_a"]
            )
            .AddRule( //Mode -> ''
                
            );
        
        
        _nonTerminals["Mode_a"]
            .AddRule( //Mode_a -> out 
                (n) => {  },
                new Terminal(Tag.OUT)
            )
            .AddRule( //Mode_a -> ''
                
            );


        _nonTerminals["Instr_plus"]
            .AddRule( //Instr_plus -> Instr Instr_star
                (n) => { n.GiveChildsToParents();},true,
                _nonTerminals["Instr"], _nonTerminals["Instr_star"]
            );
        
        
        _nonTerminals["Instr_star"]
            .AddRule( //Instr_star -> Instr Instr_star
                (n) => {n.GiveChildsToParents();},
                _nonTerminals["Instr"], _nonTerminals["Instr_star"]
            )
            .AddRule( //Instr_star -> ''
            );
        
        
        _nonTerminals["Expr_virgule_plus"]
            .AddRule( //Expr_virgule_plus -> Expr Expr_virgule_star
                (n) => { },true,
                _nonTerminals["Expr"], _nonTerminals["Expr_virgule_star"]
            );

        
        _nonTerminals["Expr_virgule_star"]
            .AddRule( //Expr_virgule_star -> , Expr Expr_virgule_star 
                (n) => { n.RemoveChildrens(0); n.GiveChildsToParents();},
                new Terminal(','), _nonTerminals["Expr"], _nonTerminals["Expr_virgule_star"]
            )
            .AddRule( //Expr_virgule_star -> ''
            );

        
        _nonTerminals["Expr_egal_interog"]
            .AddRule( //Expr_egal_interog -> := Expr
                (n) => { n.RemoveChildrens(0); n.RenameNode("ASSIGN");}, true,
                new Terminal(Tag.ASSIGN), _nonTerminals["Expr"] //TODO ASSIGN comme le NOT
            )
            .AddRule( //Expr_egal_interog -> ''
            );

        
        _nonTerminals["Expr_interog"]
            .AddRule( //Expr_interog -> Expr
                _nonTerminals["Expr"]
            )
            .AddRule( //Expr_interog -> ''
            );
        
        
        _nonTerminals["Elsif_star"]
            .AddRule( //Elsif_star -> elsif Expr then Instr_plus Elsif_star
                (n) => { 
                    n.RenameNode("ELSIF"); n.RemoveChildrens(0); 
                },
                new Terminal(Tag.ELSIF), _nonTerminals["Expr"], new Terminal(Tag.THEN), _nonTerminals["Instr_plus"], _nonTerminals["Elsif_star"]
            )
            .AddRule( //Elsif_star -> ''
                
            );
        
        
        _nonTerminals["Else_interog"]
            .AddRule( //Else_interog -> else Instr_plus
                (n) => { n.RemoveChildrens(0); n.RenameNode("ELSE");}, true,
                new Terminal(Tag.ELSE), _nonTerminals["Instr_plus"]
            )
            .AddRule( //Else_interog -> ''
                
            );
        
        
        _nonTerminals["Else_i"]
            .AddRule( //Else_i -> else
                new Terminal(Tag.ELSE)
            )
            .AddRule( //Else_i -> ''
                
            );
        
        
        _nonTerminals["Then_i"]
            .AddRule( //Then_i -> else Instr_plus
                new Terminal(Tag.THEN)
            )
            .AddRule( //Then_i -> ''
                
            );
        
        
        _nonTerminals["Instr"]                                                    //TODO Instr, 1 et 2
            .AddRule( //Instr -> ident Instr1
                (n) =>  { n.RenameNodeByChild(1);},
                new Terminal(Tag.ID), _nonTerminals["Instr1"]
            )
            .AddRule( //Instr -> entier E15+ := Expr ;
                (n) => { n.RenameNode("ASSIGN");}, true,
                new Terminal(Tag.NUM), _nonTerminals["E15+"], new Terminal(Tag.ASSIGN), _nonTerminals["Expr"], new Terminal(';')
            )
            .AddRule( //Instr -> char E15+ := Expr ;
                (n) => { n.RenameNode("ASSIGN");}, true,
                new Terminal(Tag.CHAR), _nonTerminals["E15+"], new Terminal(":="), _nonTerminals["Expr"], new Terminal(';')
            )
            .AddRule( //Instr -> true E15+ := Expr ;
                (n) => { n.RenameNode("ASSIGN");}, true,
                new Terminal(Tag.TRUE), _nonTerminals["E15+"], new Terminal(Tag.ASSIGN), _nonTerminals["Expr"],new Terminal(';')
            )
            .AddRule( //Instr -> false E15+ := Expr ;
                (n) => { n.RenameNode("ASSIGN");}, true,
                new Terminal(Tag.FALSE), _nonTerminals["E15+"], new Terminal(Tag.ASSIGN), _nonTerminals["Expr"],new Terminal(';')
            )
            .AddRule( //Instr -> null E15+ := Expr ;
                (n) => { n.RenameNode("ASSIGN");}, true,
                new Terminal(Tag.NULL), _nonTerminals["E15+"], new Terminal(Tag.ASSIGN), _nonTerminals["Expr"],new Terminal(';')
            )
            .AddRule( //Instr -> ( Expr ) E15+ := Expr ;
                (n) => { n.RenameNode("ASSIGN");}, 
                new Terminal('('), _nonTerminals["Expr"], new Terminal(')'), _nonTerminals["E15+"], new Terminal(Tag.ASSIGN), _nonTerminals["Expr"],new Terminal(';')
            )
            .AddRule( //Instr -> new Ident E15+ := Expr ;
                new Terminal(Tag.NEW), new Terminal(Tag.ID), _nonTerminals["E15+"], new Terminal(Tag.ASSIGN), _nonTerminals["Expr"],new Terminal(';')
            )
            .AddRule( //Instr -> character ' val ( Expr ) E15+ := Expr ;
                new Terminal(Tag.CHARACTER), new Terminal('\''), new Terminal("val"), new Terminal('('), _nonTerminals["Expr"] ,new Terminal(')'), 
                _nonTerminals["E15+"], new Terminal(Tag.ASSIGN), _nonTerminals["Expr"],new Terminal(';')
            )
            .AddRule( //Instr -> return Expr_interog ;
                (n) => {n.RenameNode("RETURN"); n.KeepChildrens(1);}, 
                new Terminal(Tag.RETURN), _nonTerminals["Expr_interog"],new Terminal(';')
            )
            .AddRule( //Instr -> begin Instr_plus end ;
                new Terminal(Tag.BEGIN), _nonTerminals["Instr_plus"], new Terminal(Tag.END),new Terminal(';')
            )
            .AddRule( //Instr -> if Expr then Instr_plus Elsif_star Else_interog end if ;
                (n) => { n.RenameNode("COND"); n.KeepChildrens(1,3,4,5); n.RenameChildren(3, "THEN");},
                new Terminal(Tag.IF), _nonTerminals["Expr"], new Terminal(Tag.THEN), _nonTerminals["Instr_plus"], _nonTerminals["Elsif_star"],
                _nonTerminals["Else_interog"], new Terminal(Tag.END), new Terminal(Tag.IF), new Terminal(';')
            )
            .AddRule( //Instr -> for Ident in Reverse_interog Expr . . Expr loop Instr_plus end loop ;
                (n) => { n.RenameNode("LOOP"); n.KeepChildrens(1,3,4,6,8); n.RenameChildren(8, "BLOCK");},
                new Terminal(Tag.FOR), new Terminal(Tag.ID), new Terminal(Tag.IN), _nonTerminals["Reverse_interog"], _nonTerminals["Expr"], 
                new Terminal(Tag.DOT_DOT), _nonTerminals["Expr"], new Terminal(Tag.LOOP), _nonTerminals["Instr_plus"],
                new Terminal(Tag.END), new Terminal(Tag.LOOP), new Terminal(';')
            ) // TODO C'était commenté
            .AddRule( //Instr -> while Expr loop Instr_plus end loop ;
                (n) => { n.RenameNode("WHILE"); n.KeepChildrens(1,3); n.RenameChildren(3, "BLOCK");},
                new Terminal(Tag.WHILE), _nonTerminals["Expr"], new Terminal(Tag.LOOP), _nonTerminals["Instr_plus"], new Terminal(Tag.END), 
                new Terminal(Tag.LOOP), new Terminal(';')
            );


        _nonTerminals["Instr1"]
            .AddRule( //Instr1 -> := Expr ;
                (n) => { n.RenameNode("ASSIGN"); n.RemoveChildrens(0,2);},true,
                new Terminal(Tag.ASSIGN), _nonTerminals["Expr"],new Terminal(';')
            )
            .AddRule( //Instr1 -> E15+ := Expr ;
                _nonTerminals["E15+"], new Terminal(Tag.ASSIGN), _nonTerminals["Expr"], new Terminal(';')
            )
            .AddRule( //Instr1 -> ( Expr_virgule_plus ) Instr2
                (n) => { n.RenameNode("CALL"); n.RemoveChildrens(0,2); n.RenameChildren(1,"PARAMS");}, true,
                new Terminal('('), _nonTerminals["Expr_virgule_plus"], new Terminal(')'), _nonTerminals["Instr2"]
            )
            .AddRule( //Instr1 -> ;
                new Terminal(';')
            );


        _nonTerminals["Instr2"]
            .AddRule( //Instr2 -> E15+ := Expr ;
                (n) =>
                { n.RenameNode("ASSIGN"); n.RemoveChildrens(1, 3); },
                _nonTerminals["E15+"], new Terminal(Tag.ASSIGN), _nonTerminals["Expr"], new Terminal(';')
            )
            .AddRule( //Instr2 -> ;
                (n) => {n.RemoveChildrens(0);},
                new Terminal(';')
            );
        
        
        _nonTerminals["Expr"]
            .AddRule( //Expr -> E2 E1
                _nonTerminals["E2"], _nonTerminals["E1"]
            );
        
        
        _nonTerminals["E1"]
            .AddRule( //E1 -> or Else_i E2 E1
                new Terminal(Tag.OR), _nonTerminals["Else_i"], _nonTerminals["E2"], _nonTerminals["E1"]
            )
            .AddRule( //E1 -> ''
                
            );


        _nonTerminals["E2"]
            .AddRule( //E2 -> E4 E3
                _nonTerminals["E4"], _nonTerminals["E3"]
            );
        
        
        
        _nonTerminals["E3"]
            .AddRule( //E3 -> and Then_i E4 E3
                new Terminal(Tag.AND), _nonTerminals["Then_i"], _nonTerminals["E4"], _nonTerminals["E3"]
            )
            .AddRule( //E3 -> ''
                
            );


        _nonTerminals["E4"]
            .AddRule( //E4 -> E5
                _nonTerminals["E5"]
            )
            .AddRule( //E4 -> not E4
                (n) => { n.RemoveChildrens(0); n.RenameNode("NOT");}, true,
                new Terminal(Tag.NOT), _nonTerminals["E4"]
            );
        
        
        _nonTerminals["E5"]
            .AddRule( //E5 -> E7 E6 
                _nonTerminals["E7"], _nonTerminals["E6"]
            );
        
        
        _nonTerminals["E6"]
            .AddRule( //E6 -> = E7 E6
                new Terminal('='), _nonTerminals["E7"], _nonTerminals["E6"]
            )
            .AddRule( //E6 -> /= E7 E6
                new Terminal(Tag.NOT_EQUAL), _nonTerminals["E7"], _nonTerminals["E6"]
            )
            .AddRule( //E6 -> ''
            
            );
        
        
        _nonTerminals["E7"]
            .AddRule( //E7 -> E9 E8 
                _nonTerminals["E9"], _nonTerminals["E8"]
            );
        
        
        _nonTerminals["E8"]
            .AddRule( //E8 -> > E9 E8
                new Terminal('>'), _nonTerminals["E9"], _nonTerminals["E8"]
            )
            .AddRule( //E8 -> >= E9 E8
                new Terminal(Tag.SUP_OR_EQUAL), _nonTerminals["E9"], _nonTerminals["E8"]
            )
            .AddRule( //E8 -> < E9 E8
                new Terminal('<'), _nonTerminals["E9"], _nonTerminals["E8"]
            )
            .AddRule( //E8 -> <= E9 E8
                new Terminal(Tag.INF_OR_EQUAL), _nonTerminals["E9"], _nonTerminals["E8"]
            )
            .AddRule( //E8 -> ''
                
            );

        
        _nonTerminals["E9"]
            .AddRule( //E9 -> E11 E10
                _nonTerminals["E11"], _nonTerminals["E10"]
            );
        
        
        _nonTerminals["E10"]
            .AddRule( //E10 -> + E11 E10
                new Terminal('+'), _nonTerminals["E11"], _nonTerminals["E10"]
            )
            .AddRule( //E10 -> - E11 E10
                new Terminal('-'), _nonTerminals["E7"], _nonTerminals["E6"]
            )
            .AddRule( //E10 -> ''
            
            );
        
        
        _nonTerminals["E11"]
            .AddRule( //E11 -> E13 E12
                _nonTerminals["E13"], _nonTerminals["E12"]
            );
        
        
        _nonTerminals["E12"]
            .AddRule( //E12 -> * E13 E12
                new Terminal('*'), _nonTerminals["E13"], _nonTerminals["E12"]
            )
            .AddRule( //E12 -> / E13 E12
                new Terminal('/'), _nonTerminals["E13"], _nonTerminals["E12"]
            )
            .AddRule( //E12 -> rem E13 E12
                new Terminal(Tag.REM), _nonTerminals["E13"], _nonTerminals["E12"]
            )
            .AddRule( //E12 -> ''
                
            );


        _nonTerminals["E13"]
            .AddRule( //E13 -> E14
                _nonTerminals["E14"]
            )
            .AddRule( //E13 -> - E13
                (n) => { n.RemoveChildrens(0); n.RenameNode("UNARY_MINUS");}, true,
                new Terminal('-'), _nonTerminals["E13"]
            );
        
        
        _nonTerminals["E14"]
            .AddRule( //E14 -> entier E15
                new Terminal(Tag.NUM), _nonTerminals["E15"]
            )
            .AddRule( //E14 -> char E15
                new Terminal(Tag.CHAR), _nonTerminals["E15"] // TODO A TESTER                 
            )
            .AddRule( //E14 -> true E15
                new Terminal(Tag.TRUE), _nonTerminals["E15"]
            )
            .AddRule( //E14 -> false E15
                new Terminal(Tag.FALSE), _nonTerminals["E15"]
            )
            .AddRule( //E14 -> null E15
                new Terminal(Tag.NULL), _nonTerminals["E15"]
            )
            .AddRule( //E14 -> ident E14b
                new Terminal(Tag.ID), _nonTerminals["E14b"]
            )
            .AddRule( //E14 -> ( Expr ) E15
                new Terminal('('), _nonTerminals["Expr"], new Terminal(')'), _nonTerminals["E15"]
            )
            .AddRule( //E14 -> new ident E15
                (n) => { n.RenameNode("CREATE"); n.RemoveChildrens(0);}, true,
                new Terminal(Tag.NEW), new Terminal(Tag.ID), _nonTerminals["E15"]
            )
            .AddRule( //E14 -> character ' val ( Expr ) E15
                new Terminal(Tag.CHARACTER), new Terminal('\''), new Terminal("val"), new Terminal('('), _nonTerminals["Expr"], new Terminal(')'), _nonTerminals["E15"]
            );                              //TODO fix this issue
        
        
        _nonTerminals["E14b"]
            .AddRule( //E14b -> E15
                _nonTerminals["E15"]
            )
            .AddRule( //E14b -> ( Expr_virgule_plus ) E15
                new Terminal('('), _nonTerminals["Expr_virgule_plus"], new Terminal(')'), _nonTerminals["E15"]
            );


        _nonTerminals["E15+"]                                                               //TODO node POINT
            .AddRule( //E15+ -> . ident E_ident_b
                new Terminal('.'), new Terminal(Tag.ID), _nonTerminals["E15"]
            );
        
        
        _nonTerminals["E15"]
            .AddRule( //E15 -> . ident E15
                new Terminal('.'), new Terminal(Tag.ID), _nonTerminals["E15"]
            )
            .AddRule( //E15 -> ''
                
            );
        
        
        
        _nonTerminals["Reverse_interog"]
            .AddRule( //Reverse_interog -> reverse
                new Terminal(Tag.REVERSE)
            )
            .AddRule( //Reverse_interog -> ''
                
            );
        
        
        _nonTerminals["Ident_virgule_plus"]                                            
            .AddRule( //Ident_virgule_plus -> ident Ident_virgule_star
                (n) => { n.RenameNode("IDENTS");},
                new Terminal(Tag.ID), _nonTerminals["Ident_virgule_star"]
            );
        
        
        _nonTerminals["Ident_virgule_star"]
            .AddRule( //Ident_virgule_star -> , ident Ident_virgule_star
                (n) =>
                {
                    n.RemoveChildrens(0); n.GiveChildsToParents();
                }, 
                new Terminal(','), new Terminal(Tag.ID), _nonTerminals["Ident_virgule_star"]
            )
            .AddRule( //Ident_virgule_star -> ''
                
            );
        
        
        _nonTerminals["Ident_interog"]
            .AddRule( //Ident_interog -> ident 
                new Terminal(Tag.ID)
            )
            .AddRule( //Ident_interog -> ''
                
            );
        
        
        // TODO Calculate automaticly the firsts and nexts here
        this.addPeps();
        this.AddFirst();
        this.AddNext();
        this.AddAllSd();
    }

    public void addPeps()
    {
        p_eps = new Dictionary<string, NonTerminal>();
        StreamReader sr = new StreamReader("../../../src/grammaire_data/peps.txt");
        string line = sr.ReadLine();
        string[] words = line.Split(' ');
        foreach (var w in words)
        {
            p_eps.Add(w, new NonTerminal(w));
        }
        
    }
    
    public void PrintPeps()
    {
        Console.WriteLine("Les non terminaux se dérivant en le mot vide sont :");
        foreach (var nt in p_eps.Keys)
        {
            Console.Write(nt + " ");
        }
        Console.WriteLine();
    }
    
    
    // On rempli List<Terminal> _first de chaque nonTerminal de la grammaire
    public void AddFirst()
    {
        StreamReader sr = new StreamReader("../../../src/grammaire_data/first.txt");
        Token t;
        string line = sr.ReadLine();
        while (line != null)
        {   
            string[] words = line.Split(' ');
            if (this.GetNonTerminals().TryGetValue(words[0],out NonTerminal nt))
            {
                for (int i = 1; i < words.Length ; i++)
                {
                    _words.TryGetValue(words[i], out t);
                    if (t == null)
                    {
                        Console.WriteLine("erreur : " + words[i]);
                    }
                    else
                    {
                        nt.GetFirst().Add(new Terminal(t));
                    }
                }
            }
            else
            {
                Console.WriteLine("NonTerminal non trouvé pour l'étiquette");
            }
            line = sr.ReadLine();
        }
    }

    public void PrintAllFirst()
    {
        foreach (var nt in _nonTerminals.Values)
        {
            nt.PrintFirst();
        }
    }
    
    public void AddNext()
    {
        StreamReader sr = new StreamReader("../../../src/grammaire_data/next.txt");
        Token t;
        string line = sr.ReadLine();
        while (line != null)
        {   
            string[] words = line.Split(' ');
            if (this.GetNonTerminals().TryGetValue(words[0],out NonTerminal nt))
            {
                for (int i = 1; i < words.Length ; i++)
                {
                    _words.TryGetValue(words[i], out t);
                    if (t == null)
                    {
                        Console.WriteLine("erreur : " + words[i]);
                    }
                    else
                    {
                        nt.GetNext().Add(new Terminal(t));
                    }
                }
            }
            else
            {
                Console.WriteLine("NonTerminal non trouvé pour l'étiquette");
            }
            line = sr.ReadLine();
        }
    }
    
    public void PrintAllNext()
    {
        foreach (var nt in _nonTerminals.Values)
        {
            nt.PrintNext();
        }
    }

    public void AddAllSd()
    {
        foreach (var nt in _nonTerminals.Values)
        {
            foreach (var r in nt.GetRules())
            {
                this.AddSd(r,nt);
            }
        }
    }

    public void AddSd(Rule r,NonTerminal nt)
    {
        bool b = true;
        foreach (var s in r.GetSymbol())
        {
            foreach (var t in s.GetFirst())
            {
                r.AddSd(t);
            }

            if (!s.GetNext().Any()) 
            {   
                b = false;
                break;
            }
            else if (!p_eps.ContainsKey(s.GetLabel()))
            {
                b = false;
                break;
            }
        }

        if (b)
        {
            foreach (var t in nt.GetNext())
            {
                r.AddSd(t);
            }
        }
    }

    /// <summary>
    /// Print a List of Rules
    /// </summary>
    /// <param name="rules"></param>

    public void PrintAllRules()
    {
        foreach (var nt in _nonTerminals.Values)
        {
            PrintRules(nt.Rules);    
        }
    }
    public static void PrintRules(List<Rule> rules)
    {
        foreach (var rule in rules)
        {
            rule.PrintRule();
        }
    }

    public Dictionary<String,NonTerminal> GetNonTerminals()
    {
        return _nonTerminals;
    }
    
    private void Reserve(Word w) {
        _words.Add(w.Lexeme, w);
    }

    public NonTerminal GetAxiom()
    {
        return Axiom;
    }
}