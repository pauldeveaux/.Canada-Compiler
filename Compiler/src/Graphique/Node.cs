namespace Graphique;

using System.Collections.Generic;
public class Node
{
    public int Value { get; set; }
    public List<Node> Children { get; set; }

    public Node(int value)
    {
        Value = value;
        Children = new List<Node>();
    }
}

    