namespace Graphique;
using System.Windows.Forms;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;

public class TreeVisualizer : Form
{
    public static void DrawTree(Graph graph)
    {
        // Créer un formulaire
        Form form = new Form();

        // Créer un objet viewer
        GViewer viewer = new GViewer();

        
        viewer.Graph = graph;

        // Associer le viewer au formulaire
        form.SuspendLayout();
        viewer.Dock = System.Windows.Forms.DockStyle.Fill;
        form.Controls.Add(viewer);
        form.ResumeLayout();

        // Afficher le formulaire
        form.ShowDialog();

    }
}