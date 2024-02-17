using Lexer;
using Parser;
using Graphique;
using Lex = Lexer.Lexer;
using System.IO;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Exceptions;



public static class Program
{
    
    public static void Main(String[] args)
    {
        string? inputFile;
        
        if (args.Length < 1)
        {
            do
            {
                Console.WriteLine("-----------------------------------------------------------\n" +
                                  "Veuillez entrer le numéro du programme à analyser");

                inputFile = Console.ReadLine();
            } while (inputFile == "help");
        }
        else
        {
            inputFile = args[0];
        }



        if (inputFile.Equals("all"))
        {
            for (int i = 1; i <= 14; i++)
            {
                LaunchCompiler("" + i);
                
            }
        }
        else
        {
            LaunchCompiler(inputFile);
        }
    }


    private static void LaunchCompiler(String inputFile)
    {
        string programm = System.IO.File.ReadAllText($@"canada_files/input{inputFile}.txt");
        
        
        
        //string programm = System.IO.File.ReadAllText("../../../canada_files/input5.txt");
        StringReader sr = new StringReader(programm);
        Console.SetIn(sr);


        
        Lex lexer = new Lex();
        
        Parser.Parser parser = new Parser.Parser(lexer);

        try
        {
            ParseTree tree = parser.Scan();

            Console.WriteLine("\n");
            Console.WriteLine("Tree :\n");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(tree);
            Console.ResetColor();
            
            Console.WriteLine("\n");

            //Graphique.Graphique.DisplayParseTree(tree.Ast);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("AST finale");
            tree.Ast.AssoGauche();
            Console.WriteLine(tree.Ast.ToString());
            Console.ResetColor();

            //Graphique.Graphique.DisplayParseTree(tree);
            Console.WriteLine("Input"+inputFile);
            
            //Graphique.Graphique.DisplayParseTree(tree.Ast);
        }
        catch (UnrecognizedChar e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\nLEXICAL ERRORS");
            Console.WriteLine(string.Join("\n", e.Lexer.GetErrors()));
        }
        catch (SyntaxicException e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\nSYNTAXIC ERRORS");
            Console.WriteLine(e.ToString());
        }
    }
}