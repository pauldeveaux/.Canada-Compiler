using Microsoft.Msagl.Drawing;
using Parser;
using Color = Microsoft.Msagl.Drawing.Color;

namespace Graphique;

public class Graphique
{
    
    private static int count = 0; 
    
    public static void DisplayParseTree(AbstractTree ast)
    {
        Graph graph = new Graph("AST");
        TreeNode root = new TreeNode(ast.Label + " ("+count++ +")");
        foreach (AbstractTree a in ast.Childrens)
        {
            TreeNode node = ExploreNode(a);
            root.AddChild(node);
        }
        root.ReverseChilds();
        graph = TreeNode.treeToGraph(root, graph);
        TreeVisualizer.DrawTree(graph);
    }
    
    private static TreeNode ExploreNode(AbstractTree ast)
    {
        TreeNode node = new TreeNode(ast.Label + " ("+count++ +")");
        foreach (AbstractTree a in ast.Childrens)
        {
            TreeNode child = ExploreNode(a);
            node.AddChild(child);
        }

        node.ReverseChilds(); 
        return node;
    }
}
