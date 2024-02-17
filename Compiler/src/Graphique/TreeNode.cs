using Microsoft.Msagl.Drawing;

namespace Graphique;

public class TreeNode
{
    public string Value { get; set; }
    private List<TreeNode> Children { get; set; }

    public TreeNode(string value)
    {
        Value = value;
        Children = new List<TreeNode>();
    }
    
    public void AddChild(TreeNode child)
    {
        Children.Add(child);
    }
    
    
    
    public static Graph treeToGraph(TreeNode root, Graph graph)
    {
        graph.AddNode(root.Value);
        foreach (TreeNode child in root.Children)
        {
            //Console.WriteLine($"Adding edge {root.Value} -> {child.Value}");
            graph.AddEdge(root.Value, child.Value);
            treeToGraph(child, graph);
        }
        return graph;
    }

    public void ReverseChilds()
    {
        Children.Reverse();
    }
}