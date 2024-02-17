# .Canada Compiler Project

## Description
This is compiler was made as a school project. It that implements a lexer and parser for the .Canada language, a fictional language inspired by Ada.
Only the lexer and parser are implemented. The program generates a syntax tree and an abstract syntax tree (AST), and reports the errors with their location.

## Usage
To run the compiler, execute Compiler/src/Program.cs. The program can be run with or without arguments.

### Running with Arguments
If run with an integer argument, it specifies which file from the `canada_files` directory should be given to the compiler for processing. For example:
```bash
csc ./Program 1
```

### Running without Arguments
If run without any arguments, the program will prompt you to enter the file number you want to compile. Follow the on-screen instructions to proceed.


## File Structure
- Compiler/src/Program: Contains the main program file.
- Compiler/canada_files: Directory containing input files for testing. It includes 14 files named inputX where X is a number. 
- Additional files can be manually created by the user in Compier/canada_files for testing purposes.
- The original subject can be found in the root of the repository : sujet-Projet-2023-24.pdf

## Grammar
The compiler uses the following grammar:
```
Fichier -> with Ada.Text_I0; use Ada.Text_I0; procedure Ident is Decl_star begin Instr_plus end Ident_interog ; EOF
Decl_star -> Decl Decl_star
Decl_star ->
Decl -> type Ident Decl_b
Decl -> Ident_virgule_plus : Type Expr_egal_interog ;
Decl -> procedure Ident Params is Decl_star begin Instr_plus end Ident_interog ;
Decl -> function Ident Params return Type is Decl_star begin Instr_plus end Ident_interog ;
Decl_b -> ;
Decl_b -> is Decl_a
Decl_a -> acces Ident ;
Decl_a -> record Champs_plus end record ;
Champs -> Ident_virgule_plus : Type ;
Champs_plus -> Champs Champs_star
Champs_star -> Champs Champs_star
Champs_star ->
Type -> Ident
Type -> access Ident
Params -> ( Param_plus ) 
Params ->
Param -> Ident_virgule_plus : Mode Type
Param_plus -> Param Param_star
Param_star -> ; Param Param_star
Param_star -> 
Mode -> in Mode_a
Mode -> 
Mode_a -> out 
Mode_a -> 
Instr_plus -> Instr Instr_star
Instr_star -> Instr Instr_star
Instr_star ->
Expr_virgule_plus -> Expr Expr_virgule_star
Expr_virgule_star ->
Expr_virgule_star -> , Expr Expr_virgule_star 
Expr_egal_interog -> := Expr
Expr_egal_interog ->
Expr_interog -> Expr
Expr_interog ->
Elsif_star -> elsif Expr then Instr_plus Elsif_star
Elsif_star ->
Else_interog -> else Instr_plus
Else_interog ->
Else_i -> else 
Else_i ->
Then_i -> then
Then_i ->
Instr -> Ident Instr1
Instr -> entier E15+ := Expr ;
Instr -> caractere E15+ := Expr ;
Instr -> true E15+ := Expr ;
Instr -> false E15+ := Expr ;
Instr -> null E15+ := Expr ;
Instr -> ( Expr ) E15+ := Expr ;
Instr -> new Ident E15+ := Expr ;
Instr -> character ' val ( Expr ) E15+ := Expr ;
Instr -> return Expr_interog ;
Instr -> begin Instr_plus end ;
Instr -> if Expr then Instr_plus Elsif_star Else_interog end if ;
Instr -> for Ident in Reverse_interog Expr . . Expr loop Instr_plus end loop ;
Instr -> while Expr loop Instr_plus end loop ;
Instr1 -> := Expr ;
Instr1 -> E15+ := Expr ;
Instr1 -> ( Expr_virgule_plus ) Instr2
Instr1 -> ;
Instr2 -> E15+ := Expr ;
Instr2 -> ;
Expr -> E2 E1
E1 -> or Else_i E2 E1 
E1 ->
E2 -> E4 E3 
E3 -> and Then_i E4 E3
E3 ->
E4 -> E5 
E4 -> not E4
E5 -> E7 E6
E6 -> = E7 E6
E6 -> /= E7 E6
E6 ->
E7 -> E9 E8 
E8 -> > E9 E8
E8 -> >= E9 E8
E8 -> < E9 E8
E8 -> <= E9 E8
E8 ->
E9 -> E11 E10
E10 -> + E11 E10
E10 -> - E11 E10
E10 ->
E11 -> E13 E12
E12 -> * E13 E12
E12 -> / E13 E12
E12 -> rem E13 E12
E12 ->
E13 -> E14
E13 -> - E13
E14 -> entier E15 
E14 -> caractere E15
E14 -> true E15
E14 -> false E15
E14 -> null E15
E14 -> Ident E14b
E14 -> ( Expr ) E15
E14 -> new Ident E15
E14 -> character ' val ( Expr ) E15
E14b -> E15
E14b -> ( Expr_virgule_plus ) E15
E15+ -> . Ident E15
E15 -> . Ident E15
E15 ->
Reverse_interog -> reverse
Reverse_interog ->
Ident_virgule_plus -> Ident Ident_virgule_star
Ident_virgule_star ->
Ident_virgule_star -> , Ident Ident_virgule_star
Ident_interog -> Ident
Ident_interog ->

```
