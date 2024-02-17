using System.Reflection.Emit;
using Console = System.Console;

namespace Parser;

public class AbstractTree
{
    public String Label { get; set; }
    public List<AbstractTree> Childrens { get; set;}
    private ParseTree? parseTree;
    public bool IsEmpty { get; private set; }
    public bool IsModify;

    public AbstractTree Parent { get; private set; }
    public bool MustGiveChildsToParent { get; set; } = false;

    public AbstractTree(ParseTree tree)
    {
        this.parseTree = tree;
        this.Label = parseTree.GetSymbol().GetLabel();
        this.Childrens = new List<AbstractTree>();
        this.IsModify = false;
    }

    public AbstractTree(string label)
    {
        this.Label = label;
        this.Childrens = new List<AbstractTree>();
        this.IsModify = false;
    }

    public string GetLabel()
    {
        return Label;
    }

    public List<AbstractTree> GetChildren()
    {
        return Childrens;
    }

    public AbstractTree(ParseTree tree, AbstractTree parent)
    {
        this.parseTree = tree;
        this.Label = parseTree.GetSymbol().GetLabel();
        this.Childrens = new List<AbstractTree>();
        this.Parent = parent;
    }


    public void SetChildrens(List<ParseTree> childs)
    {
        foreach (var child in childs)
        {
            if(!child.Ast.IsEmpty)
                this.Childrens.Add(child.Ast);
            if (child.Ast.MustGiveChildsToParent)
            {
                this.Childrens.AddRange(child.Ast.Childrens);
                this.Childrens.Remove(child.Ast);
            }
        }
    }

    public void Remove()
    {
        string s = "sorcière";
        switch (Label)
        {
            case "Expr":
                s = "E1";
                break;
            case "E2":
                s = "E3";
                break;
            case "E5":
                s = "E6";
                break;
            case "E7":
                s = "E8";
                break;
            case "E9":
                s = "E10";
                break;
            case "E11":
                s = "E12";
                break;
            case "E14":
                s = "E15";
                break;
            case "E14b":
                s = "E15";
                break;
            case "E15+":
                s = "E15";
                break;
        }

        if (s != "sorcière")
        {
            for (int i = 0; i < Childrens.Count; i++)
            {
                AbstractTree child = Childrens[i];
                if (child.GetLabel() == s)
                {
                    Childrens.Remove(Childrens[i]);
                }
            } 
        }

        for (int i = 0; i < Childrens.Count; i++)
        {
            AbstractTree child = Childrens[i];
            child.Remove();


        }
    }

    public void Egalite()
    {
        string s = "turboMan";
        switch (Label)
        {
            case "Expr":
                s = "E1";
                break;
            case "E2":
                s = "E3";
                break;
            case "E5":
                s = "E6";
                break;
            case "E7":
                s = "E8";
                break;
            case "E9":
                s = "E10";
                break;
            case "E11":
                s = "E12";
                break;
            case "E14":
                s = "E15";
                break;
            case "E14b":
                s = "E15";
                break;
            case "E15+":
                s = "E15";
                break;
        }

        if (s != "TurboMan")
        {
            IsModify = true;
            for (int i = 0; i < Childrens.Count; i++)
            {
                AbstractTree child = Childrens[i];
                if (child.GetLabel() == s)
                {
                    for (int j = 0; j < child.GetChildren().Count; j++)
                    {
                        Childrens.Add(child.Childrens[j]);
                    }
                }
            }
        }


        for (int i = 0; i < Childrens.Count; i++)
        {
            AbstractTree child = Childrens[i];
            if (!child.IsModify)
            {
                child.Egalite();    
            } 
        }
    }
    
    public void Gauchiste()
    {
        if (Label == "Expr" || Label == "E2" || Label == "E5" || Label == "E7" || Label == "E9" || Label == "E11" || Label == "E14" || Label == "E14b" || Label == "E15+")
        {
            if (Childrens.Count == 0)
            {
            }
            else if (Childrens.Count == 1)
            {
                Label = Childrens[0].Label;
                //Childrens = new List<AbstractTree>();
                List<AbstractTree> cpChild = new List<AbstractTree>();
                cpChild.Add(Childrens[0]);
                Childrens = cpChild;
            }
            else
            {
                AbstractTree newAbstractTree = new AbstractTree(Label);
                for (int i = 0; i < Childrens.Count - 2; i++)
                {
                    newAbstractTree.Childrens.Add(Childrens[i]);
                }

                Label = Childrens[Childrens.Count - 2].Label;
                List<AbstractTree> newChildren = new List<AbstractTree>();
                newChildren.Add(newAbstractTree);
                newChildren.Add(Childrens[Childrens.Count - 1]);
                Childrens = newChildren;
            }
        }

        for (int i = 0; i < Childrens.Count; i++){
            AbstractTree child = Childrens[i];
            child.Gauchiste();    
        }
        
        
    }

    public void BugDeMesFesses()
    {
        if (Label == "." && Childrens.Count > 0 && Childrens[0].Label == "E15+")
        {
            Childrens.Remove(Childrens[0]);
        }
        if (Childrens.Count == 1 && Label == Childrens[0].Label )
        {
            Childrens = Childrens[0].Childrens;
        }
        if (Label == "Expr" || Label == "E2" || Label == "E5" || Label == "E7" || Label == "E9" || Label == "E11" || Label == "E14" || Label == "E14b" || Label == "E15+")
        {
            if (Childrens.Count == 0)
            {
            }
            else
            {
                Label = Childrens[0].Label;
                Childrens = Childrens[0].Childrens;    
            }
        }

        for (int i = 0; i < Childrens.Count; i++){
            AbstractTree child = Childrens[i];
            child.BugDeMesFesses();    
        }
    }

    public void AssoGauche()
    {
        Egalite();
        Remove();
        Gauchiste();
        BugDeMesFesses();
    }
    
    public override string ToString()
    {
        if (Childrens.Count == 0)
            return Label;
        
        
        String s = $"{Label}(";
        for(int i = 0; i < Childrens.Count; i++)
        {
            AbstractTree child = Childrens[i];
            s += child;
                
            if (i < Childrens.Count -1)
                s += ", ";
        }

        s += ")";
        return s;
    }
    
    
    private void DeleteChildWith(ParseTree p)
    {
        foreach (var i in Childrens)
        {
            if (i.parseTree == p)
            {
                Childrens.Remove(i);
                return;
            }
        }
    }

    private void Copy(AbstractTree other)
    {
        Childrens = new List<AbstractTree>(other.Childrens);
        Label = other.Label;
        MustGiveChildsToParent = false;
        
    }

    private AbstractTree GetChildFromParseTree(int i)
    {
        if(parseTree.GetChildrens().Count > i)
            return parseTree.GetChildrens()[i].Ast;
        return null;
    }
    
    
    // -------------------------------------------- FUNCTIONS TO CALL IN LAMBDA ---------------------------------------
    
    public void RemoveChildrens(params int[] ids)
    {
        List<ParseTree> parseTreeChilds = parseTree.GetChildrens();
        
        foreach (var i in ids)
        {
            DeleteChildWith(parseTreeChilds[i]);
        }
    }

    public void KeepChildrens(params int[] ids)
    {
        List<ParseTree> parseTreeChilds = parseTree.GetChildrens();
        List<AbstractTree> newAbstractTrees = new List<AbstractTree>();

        for (int i = 0; i < parseTreeChilds.Count; i++)
        {
            if(ids.Contains(i))
                if(!parseTreeChilds[i].Ast.IsEmpty)
                    newAbstractTrees.Add(parseTreeChilds[i].Ast);
        }

        Childrens = newAbstractTrees;
    }

    public void ReplaceByChildIfOnlyOne()
    {
        if (Childrens.Count == 1)
            this.Copy(Childrens[0]);
    }

    public void DeleteIfEmpty()
    {
        if (Childrens.Count == 0)
            IsEmpty = true;
    }

    public void RenameNode(String s)
    {
        Label = s;
    }


    public void RenameNodeByChild(int i)
    {
        if (Childrens.Count >i)
        {
            Label = Childrens[i].Label;
            Childrens[i].ReplaceByChildIfOnlyOne();
        }
    }

    public void RenameChildren(int i, String s)
    {
        GetChildFromParseTree(i).Label = s;
    }

    public void GiveChildsToParents()
    {
        MustGiveChildsToParent = true;
    }
}