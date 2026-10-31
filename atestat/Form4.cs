using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace atestat
{
    public partial class Form4 : Form
    {
        public class GraphNode
        {
            public int Id { get; set; }
            public Point Location { get; set; }
            public string Label { get; set; }
            public Color Color { get; set; }
            public int Radius { get; set; } = 20;

            public GraphNode(int id, Point location, string label = null)
            {
                Id = id;
                Location = location;
                Label = label ?? id.ToString();
                Color = Color.LightBlue;
            }

            public bool Contains(Point point)
            {
                return Math.Pow(point.X - Location.X, 2) + Math.Pow(point.Y - Location.Y, 2) <= Radius * Radius;
            }
        }

        public class GraphEdge
        {
            public GraphNode From { get; set; }
            public GraphNode To { get; set; }
            public Color Color { get; set; }
            public int Weight { get; set; }
            public bool IsDirected { get; set; }

            public GraphEdge(GraphNode from, GraphNode to, bool isDirected = false)
            {
                From = from;
                To = to;
                Color = Color.Black;
                Weight = 1;
                IsDirected = isDirected;
            }
        }

       
        private List<GraphNode> nodes = new List<GraphNode>();
        private List<GraphEdge> edges = new List<GraphEdge>();
        private GraphNode selectedNode = null;
        private GraphNode draggedNode = null;
        private Point dragOffset;
        private bool isAddingEdge = false;
        private bool isDirectedGraph = false;
        private int nextNodeId = 1;

     
        private readonly Color defaultNodeColor = Color.LightBlue;
        private readonly Color selectedNodeColor = Color.Orange;
        private readonly Color defaultEdgeColor = Color.Black;

        public Form4()
        {
            InitializeComponent();
            CreateMenu();
        }

        private void CreateMenu()
        {
            MenuStrip menuStrip = new MenuStrip();

            ToolStripMenuItem graphMenu = new ToolStripMenuItem("Tip de graf");
            ToolStripMenuItem directedItem = new ToolStripMenuItem("Orientat", null, (s, e) =>
            {
                isDirectedGraph = !isDirectedGraph;
                ((ToolStripMenuItem)s).Checked = isDirectedGraph;
            });
            directedItem.CheckOnClick = true;
            graphMenu.DropDownItems.Add(directedItem);

            ToolStripMenuItem modeMenu = new ToolStripMenuItem("Setari");
            ToolStripMenuItem addNodeItem = new ToolStripMenuItem("Adauga nod", null, (s, e) => SetMode("AddNodes"));
            ToolStripMenuItem addEdgeItem = new ToolStripMenuItem("Adauga muchie", null, (s, e) => SetMode("AddEdges"));
            ToolStripMenuItem moveItem = new ToolStripMenuItem("Muta noduri", null, (s, e) => SetMode("Move"));
            ToolStripMenuItem newGraphItem = new ToolStripMenuItem("Sterge graf", null, (s, e) => ClearGraph());
            modeMenu.DropDownItems.Add(addNodeItem);
            modeMenu.DropDownItems.Add(addEdgeItem);
            modeMenu.DropDownItems.Add(moveItem);
            modeMenu.DropDownItems.Add(newGraphItem);

            menuStrip.Items.Add(graphMenu);
            menuStrip.Items.Add(modeMenu);

            this.Controls.Add(menuStrip);
        }

        private void SetMode(string mode)
        {
            isAddingEdge = (mode == "AddEdges");
            selectedNode = null;
            this.Cursor = mode == "AddNodes" ? Cursors.Cross : Cursors.Default;
            this.Invalidate();
        }

        private void ClearGraph()
        {
            nodes.Clear();
            edges.Clear();
            nextNodeId = 1;
            selectedNode = null;
            this.Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left)
            {
                if (this.Cursor == Cursors.Cross) 
                {
                    GraphNode newNode = new GraphNode(nextNodeId++, e.Location);
                    nodes.Add(newNode);
                    this.Invalidate();
                }
                else 
                {
                    foreach (GraphNode node in nodes)
                    {
                        if (node.Contains(e.Location))
                        {
                            if (isAddingEdge)
                            {
                                if (selectedNode == null)
                                {
                
                                    selectedNode = node;
                                    node.Color = selectedNodeColor;
                                }
                                else if (selectedNode != node)
                                {
                                    
                                    edges.Add(new GraphEdge(selectedNode, node, isDirectedGraph));
                                    selectedNode.Color = defaultNodeColor;
                                    selectedNode = null;
                                }
                                this.Invalidate();
                                return;
                            }
                            else
                            {
                               
                                draggedNode = node;
                                dragOffset = new Point(e.Location.X - node.Location.X, e.Location.Y - node.Location.Y);
                                node.Color = selectedNodeColor;
                                this.Invalidate();
                                return;
                            }
                        }
                    }

                    
                    if (selectedNode != null && !isAddingEdge)
                    {
                        selectedNode.Color = defaultNodeColor;
                        selectedNode = null;
                        this.Invalidate();
                    }
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                
                foreach (GraphNode node in nodes.ToArray())
                {
                    if (node.Contains(e.Location))
                    {
                        
                        edges.RemoveAll(edge => edge.From == node || edge.To == node);
                        nodes.Remove(node);

                        if (selectedNode == node)
                            selectedNode = null;

                        this.Invalidate();
                        return;
                    }
                }

                
                foreach (GraphEdge edge in edges.ToArray())
                {
                    if (IsPointOnEdge(e.Location, edge))
                    {
                        edges.Remove(edge);
                        this.Invalidate();
                        return;
                    }
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (draggedNode != null && e.Button == MouseButtons.Left)
            {
                draggedNode.Location = new Point(e.Location.X - dragOffset.X, e.Location.Y - dragOffset.Y);
                this.Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            draggedNode = null;
        }

        private bool IsPointOnEdge(Point point, GraphEdge edge)
        {
           
            Point from = edge.From.Location;
            Point to = edge.To.Location;

           
            double a = point.X - from.X;
            double b = point.Y - from.Y;
            double c = to.X - from.X;
            double d = to.Y - from.Y;

            double dot = a * c + b * d;
            double lenSq = c * c + d * d;
            double param = (lenSq != 0) ? dot / lenSq : -1;

            double xx, yy;

            if (param < 0)
            {
                xx = from.X;
                yy = from.Y;
            }
            else if (param > 1)
            {
                xx = to.X;
                yy = to.Y;
            }
            else
            {
                xx = from.X + param * c;
                yy = from.Y + param * d;
            }

            double dx = point.X - xx;
            double dy = point.Y - yy;
            double distance = Math.Sqrt(dx * dx + dy * dy);

            return distance < 5; 
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            
            foreach (GraphEdge edge in edges)
            {
                using (Pen pen = new Pen(edge.Color, 2))
                {
                    Point from = edge.From.Location;
                    Point to = edge.To.Location;

                    
                    g.DrawLine(pen, from, to);

                   
                    if (edge.IsDirected)
                    {
                        DrawArrow(g, pen, from, to);
                    }

                   
                    if (edge.Weight != 1)
                    {
                        PointF midPoint = new PointF((from.X + to.X) / 2f, (from.Y + to.Y) / 2f);
                        g.DrawString(edge.Weight.ToString(), this.Font, Brushes.Black, midPoint);
                    }
                }
            }

          
            foreach (GraphNode node in nodes)
            {
                using (Brush brush = new SolidBrush(node.Color))
                {
                  
                    g.FillEllipse(brush,
                        node.Location.X - node.Radius,
                        node.Location.Y - node.Radius,
                        node.Radius * 2,
                        node.Radius * 2);

                    
                    g.DrawEllipse(Pens.Black,
                        node.Location.X - node.Radius,
                        node.Location.Y - node.Radius,
                        node.Radius * 2,
                        node.Radius * 2);

                  
                    SizeF labelSize = g.MeasureString(node.Label, this.Font);
                    g.DrawString(node.Label, this.Font, Brushes.Black,
                        node.Location.X - labelSize.Width / 2,
                        node.Location.Y - labelSize.Height / 2);
                }
            }

           
            if (isAddingEdge && selectedNode != null)
            {
                using (Pen pen = new Pen(Color.Gray, 1))
                {
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    g.DrawLine(pen, selectedNode.Location, this.PointToClient(Cursor.Position));
                }
            }
        }

        private void DrawArrow(Graphics g, Pen pen, Point from, Point to)
        {
            
            float angle = (float)Math.Atan2(to.Y - from.Y, to.X - from.X);

            
            int arrowLength = 15;

           
            float radius = 20f; 

            
            GraphNode toNode = nodes.Find(n => n.Location == to);
            if (toNode != null)
            {
                radius = toNode.Radius;
            }

           
            Point adjustedTo = new Point(
                (int)(to.X - radius * Math.Cos(angle)),
                (int)(to.Y - radius * Math.Sin(angle)));

            
            PointF arrowPoint1 = new PointF(
                adjustedTo.X - arrowLength * (float)Math.Cos(angle - Math.PI / 6),
                adjustedTo.Y - arrowLength * (float)Math.Sin(angle - Math.PI / 6));
            PointF arrowPoint2 = new PointF(
                adjustedTo.X - arrowLength * (float)Math.Cos(angle + Math.PI / 6),
                adjustedTo.Y - arrowLength * (float)Math.Sin(angle + Math.PI / 6));

           
            g.DrawLine(pen, adjustedTo, arrowPoint1);
            g.DrawLine(pen, adjustedTo, arrowPoint2);
        }

     
    }
}
